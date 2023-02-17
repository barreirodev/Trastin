using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Landfill;
using Core;

namespace Box
{
    public class BoxMediator : MonoBehaviour, ISubject
    {
        [SerializeField] IBoxBehaviour boxBehaviour;
        [SerializeField] OriginConfiguration _origin;
        [SerializeField] ContainerConfiguration _container;
        [SerializeField] Image _image;
        [SerializeField] Image _outline;
        [SerializeField] GameObject _mesh;
        [SerializeField] ParticleSystem _smoke;
        public Image _imageFillProgress;
        public BoxConfigurationDetails.Nature _nature;
        private readonly List<IObserver> _observers;

        public bool _pickingUp;
        float _startTimePick;
        public bool _inPlayer;
        public Vector3 _positionPlayer;
        public BoxMediator()
        {
            _observers = new List<IObserver>();
        }
        public void Configuration(BoxConfigurationDetails.Nature nature, OriginConfiguration origin, ContainerConfiguration container, Sprite sprite)
        {
            _nature = nature;
            _origin = origin;
            _container = container;
            _image.sprite = sprite;
            _outline.sprite = sprite;
        }
        public ContainerConfiguration GetContainer => _container;
        public OriginConfiguration GetOrigin => _origin;
        public BoxConfigurationDetails.Nature GetNature => _nature;
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Updated(this);
            }
        }
        private void Update()
        {
            if (!_pickingUp && _imageFillProgress.fillAmount > 0 && GameCycle.Instance._gameState == GameCycle.GameState.Playing)
            {
                _imageFillProgress.fillAmount = 0;
            }
            else if (_pickingUp && GameCycle.Instance._gameState == GameCycle.GameState.Playing)
            {
                PickUpBox();
                if (!_imageFillProgress.gameObject.activeInHierarchy)
                {
                    _imageFillProgress.gameObject.SetActive(true);
                }
            }
            if (_imageFillProgress.gameObject.activeSelf && (_imageFillProgress.fillAmount == 0 || _imageFillProgress.fillAmount == 1 ||  GameCycle.Instance._gameState != GameCycle.GameState.Playing))
            {
                _imageFillProgress.gameObject.SetActive(false);
            }
            if (_inPlayer)
            {
                transform.position = new Vector3(_positionPlayer.x,transform.position.y,_positionPlayer.z);
            }
        }
        public void SetStartTimeToPick(float startTime)
        {
            _startTimePick = startTime;
            _pickingUp = true;
        }
        void PickUpBox()
        {
            float _timePassedLandfill = ((Time.realtimeSinceStartup - _startTimePick) / GameConfiguration.Instance.timeHoldingBox) * 100;
            _imageFillProgress.fillAmount = _timePassedLandfill / 100;
            if (_imageFillProgress.fillAmount == 1)
            {
                _pickingUp = false;
            }
        }
        public void HideBox()
        {
            _smoke.Play();
            _mesh.SetActive(false);
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _pickingUp = false;
            }
        }
    }
}