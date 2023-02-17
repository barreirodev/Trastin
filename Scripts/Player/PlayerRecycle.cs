using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Box;
using Recycle;
using Misc;

namespace Player
{
    public class PlayerRecycle : MonoBehaviour, IPlayer
    {
        public GameObject _box;
        float _timePassedInLandfill;

        GameObject _container;
        float _startTimeToPickUp;
        PlayerMediator _player;
        public bool _inBox;
        bool _inLandfill;

        Animator playerAnimator;

        public void AddPlayerMediator(PlayerMediator player)
        {
            _player = player;
        }

        private void Awake()
        {
            playerAnimator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            _player.GetDust.Stop();
        }

        private void Update()
        {
            if (GameCycle.Instance._gameState == GameCycle.GameState.Playing)
            {
                if (!_player._playerAnimator.GetBool("Playing"))
                {
                    _player._playerAnimator.SetBool("Playing", true);
                    AudioEffectsManager.Instance.SetAudioSourcePlayer();
                    _player.GetDust.Play();
                }

                Vector2 direction = _player.GetInput.GetDirection();
                if (_player._playerState == PlayerMediator.PlayerState.BoxTaking)
                {
                    _player._playerState = BoxTaked(_box, _startTimeToPickUp);
                }
                else if (_player._playerState == PlayerMediator.PlayerState.InLandfill)
                {
                    _player._playerState = BoxPlaced(_container.GetComponentInParent<RecycleMediator>(), _startTimeToPickUp);
                }

                if (_box != null)
                {

                    _box.GetComponent<BoxMediator>()._positionPlayer = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
                }
            }
            else
            {
                if (_player._playerAnimator.GetBool("Playing"))
                {
                    _player.GetDust.Stop();
                    _player._playerAnimator.SetBool("Playing", false);
                }               
            }
        }

        public PlayerMediator.PlayerState BoxPlaced(RecycleMediator bell, float startTimeToOpen)
        {
            if (_box != null)
            {
                if (_inLandfill && GameCycle.Instance._gameState == GameCycle.GameState.Playing)
                {
                    bell.SetStartTimeToOpenLandfill(startTimeToOpen);
                    _timePassedInLandfill += Time.deltaTime;
                    if (Time.realtimeSinceStartup >= startTimeToOpen + GameConfiguration.Instance.timeLeavingBox)
                    {
                        if (bell.GetOrigin.GetID == _box.GetComponent<BoxMediator>().GetOrigin.GetID)
                        {
                            GameCycle.Instance.AddScore(1);
                            AudioEffectsManager.Instance.Correct();                        
                        }
                        else
                        {
                            GameCycle.Instance.AddScore(-1);
                            AudioEffectsManager.Instance.Error();
                        }
                        playerAnimator.SetBool("WithBox", false);
                        _box.GetComponent<BoxMediator>().Notify();
                        _box.GetComponent<BoxMediator>()._inPlayer = false;
                        _box.AddComponent<LinearMovement>().SetMovement(new Vector3(0,0,1));
                        _box.AddComponent<Destroyer>();
                        bell.SetStartTimeToCloseLandfill(_timePassedInLandfill);                       
                        _timePassedInLandfill = 0;
                        _box = null;
                        return PlayerMediator.PlayerState.Walking;
                    }
                }
                else
                {
                    bell.SetStartTimeToCloseLandfill(_timePassedInLandfill);
                    _timePassedInLandfill = 0;
                    return PlayerMediator.PlayerState.Walking;
                }
            }
            else
            {
                return PlayerMediator.PlayerState.Walking;
            }
            return PlayerMediator.PlayerState.InLandfill;
        }

        public PlayerMediator.PlayerState BoxTaked(GameObject box, float startTimeToPickUp)
        {
            if (_inBox)
            {
                box.GetComponent<BoxMediator>().SetStartTimeToPick(startTimeToPickUp);
                if (Time.realtimeSinceStartup >= startTimeToPickUp + GameConfiguration.Instance.timeHoldingBox)
                {
                    playerAnimator.SetBool("WithBox", true);
                    box.transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
                    _box = box;
                    _box.GetComponent<BoxMediator>()._imageFillProgress.gameObject.SetActive(false);
                    _box.GetComponent<BoxMediator>()._inPlayer = true;
                    _box.GetComponent<BoxCollider>().isTrigger = false;
                    return PlayerMediator.PlayerState.Walking;
                }
            }
            else
            {
                _box = null;
                playerAnimator.SetBool("WithBox", false);
                _box.GetComponent<BoxMediator>()._imageFillProgress.gameObject.SetActive(false);
                return PlayerMediator.PlayerState.Walking;
            }
            return PlayerMediator.PlayerState.BoxTaking;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Box" && _box == null)
            {
                _box = collision.gameObject;
                _player._playerState = PlayerMediator.PlayerState.BoxTaking;
                _startTimeToPickUp = Time.realtimeSinceStartup;
                _inBox = true;
            }

            else if (collision.gameObject.tag == "Bell")
            {
                _container = collision.gameObject;
                _player._playerState = PlayerMediator.PlayerState.InLandfill;
                _startTimeToPickUp = Time.realtimeSinceStartup;
                _inLandfill = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.tag == "Box" && _box.GetComponent<BoxMediator>()._inPlayer != true)
            {
                _box = null;
                _inBox = false;
                playerAnimator.SetBool("WithBox", false);
                _player._playerState = PlayerMediator.PlayerState.Walking;
            }
            else if (collision.gameObject.tag == "Bell")
            {
                collision.gameObject.GetComponentInParent<RecycleMediator>()._inLandfill = false;
                _container = null;
                _inLandfill = false;
                _player._playerState = PlayerMediator.PlayerState.Walking;
            }
        }
    }
}