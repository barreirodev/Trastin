using UnityEngine;
using UnityEngine.UI;
using Input;
using Box;
using Core;
using Landfill;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace Player
{
    public class PlayerMediator : MonoBehaviour
    {
        public enum PlayerState
        {
            Walking,
            BoxTaking,
            InLandfill,
        };
        public PlayerState _playerState;
        LayerMask _ground;
        [SerializeField] Transform _groundTransform;
        [SerializeField] MovementController _movementController;
        public Animator _playerAnimator;
        [SerializeField] ParticleSystem _dust;

        IInput _input;

        IPlayer _behaviour;

        Vector3 _lastGoodDirection;
        Vector3 _limitMovements;

        start_connect wiiBalance;
        float _maxPositionX;
        float _minPositionX;
        float _maxPositionZ;
        float _minPositionZ;

        public float wiiX;
        public float wiiY;


        #region filtro
        private List<float> _windowX;
        private List<float> _windowY;
        #endregion
        private void Start()
        {
            _playerState = PlayerState.Walking;
            AudioEffectsManager.Instance.ShowPlayer();
            _ground = 1 << 8;
            wiiBalance = MasterSceneManager.Instance._startConnect;
            _groundTransform = GameObject.FindGameObjectWithTag("Ground").transform;

            if(!MasterSceneManager.Instance.withMouse)
                StartCoroutine(StartFilter());
            StartCoroutine(Movement());

            transform.DOMoveY(0,0.5f).SetEase(Ease.Linear).OnComplete(()=>StartCoroutine(FallOnGround()));
        }
        public IInput GetInput => _input;
        public ParticleSystem GetDust => _dust;

        public void Configuration(IInput input, Vector2 speed, float rotationSpeed, PlayerBuilder.Behaviour behaviour)
        {
            _input = input;
            _movementController.Configure(speed, rotationSpeed);
            switch (behaviour)
            {
                case PlayerBuilder.Behaviour.Landfill:
                    _behaviour = gameObject.AddComponent<PlayerLandfill>();
                    _behaviour.AddPlayerMediator(this);
                    _limitMovements = new Vector3(1,0,1);
                    _maxPositionX =  6;
                    _minPositionX =  -4.7f;
                    _maxPositionZ =  9;
                    _minPositionZ =  -9;
                    break;
                case PlayerBuilder.Behaviour.Park:
                    _behaviour = gameObject.AddComponent<PlayerPark>();
                    _behaviour.AddPlayerMediator(this);
                    _limitMovements = new Vector3(1, 0, 0);
                    _maxPositionX =  0;
                    _minPositionX =  0;
                    _maxPositionZ =  5.15f;
                    _minPositionZ =  -5.3f;
                    break;
                case PlayerBuilder.Behaviour.Centre:
                    _behaviour = gameObject.AddComponent<PlayerRecycle>();
                    _behaviour.AddPlayerMediator(this);
                    _limitMovements = new Vector3(0, 0, 1);
                    _maxPositionX =  4.5f;
                    _minPositionX =  -5.2f;
                    _maxPositionZ =  0;
                    _minPositionZ =  0;
                    break;
                default:
                    throw new System.Exception("Behaviour not implemented");

            }
        }

        IEnumerator Movement()
        {
            if (MasterSceneManager.Instance.withMouse)
            {
                while (GameCycle.Instance._gameState != GameCycle.GameState.Playing)
                {
                    yield return null;
                }
                while (GameCycle.Instance._gameState != GameCycle.GameState.Score)
                {
                    Ray ray = Camera.main.ScreenPointToRay(_input.GetDirection());
                    if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _ground))
                    {
                        _movementController.Move(new Vector3(raycastHit.point.x * _limitMovements.z, raycastHit.point.y, raycastHit.point.z * _limitMovements.x));
                        _lastGoodDirection = new Vector3(raycastHit.point.x * _limitMovements.z, raycastHit.point.y, raycastHit.point.z * _limitMovements.x);
                    }
                    else
                    {
                        _movementController.Move(_lastGoodDirection);
                    }
                    yield return null;
                }
                if (GameCycle.Instance._gameState == GameCycle.GameState.Score && transform.position.y > 0)
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                }
            }
            else
            {
                while (GameCycle.Instance._gameState != GameCycle.GameState.Playing)
                {
                    yield return null;
                }
                while (GameCycle.Instance._gameState != GameCycle.GameState.Score)
                {
                    _movementController.Move(
                        new Vector3(Mathf.Clamp(wiiY * (_groundTransform.localScale.z * GameConfiguration.Instance.multMovX),_minPositionX,_maxPositionX),
                        0,
                        Mathf.Clamp(-wiiX * (_groundTransform.localScale.x * GameConfiguration.Instance.multMovZ),_minPositionZ,_maxPositionZ)));
                    yield return null;
                }
            }
          
        }

        private IEnumerator FallOnGround()
        {
            _dust.Play();
            yield return new WaitForSeconds(0.5f);
            _dust.Stop();
        }
        #region filtro
        IEnumerator UpdateFilter()
        {
            bool bucle = true;
            while (bucle)
            {
                wiiX = 0;
                wiiY = 0;
                for (int i = 0; i < GameConfiguration.Instance.filterWindow; ++i)
                {
                    if (i < GameConfiguration.Instance.filterWindow - 1)
                    {
                        _windowX[i] = _windowX[i + 1];
                        _windowY[i] = _windowY[i + 1];
                    }
                    else
                    {
                        _windowX[i] = wiiBalance.GetMovement().x;
                        _windowY[i] = wiiBalance.GetMovement().y;
                    }
                    wiiX += _windowX[i];
                    wiiY += _windowY[i];
                }
                wiiX /= GameConfiguration.Instance.filterWindow;
                wiiY /= GameConfiguration.Instance.filterWindow;
                yield return null;
            }
        }

        IEnumerator StartFilter()
        {
            _windowX = new List<float>();
            _windowY = new List<float>();
            for (int i = 0; i < GameConfiguration.Instance.filterWindow; ++i)
            {
                yield return null;
                _windowX.Add(wiiBalance.GetMovement().x);
                _windowY.Add(wiiBalance.GetMovement().y);
            }          
            StartCoroutine(UpdateFilter());
        }
        #endregion
    }
}
