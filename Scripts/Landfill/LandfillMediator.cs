using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace Landfill {
    public class LandfillMediator : MonoBehaviour
    {
        [SerializeField] LandfillConfiguration _landfillConfiguration;
        [SerializeField] GameObject _parentCap;
        [SerializeField] Image _fillImage;
        [SerializeField] ParticleSystem _smokeFX;
        public bool _inLandfill;
        float _startTimeInClose;
        float _startTimeInOpen;
        float _timePassed;
        public ContainerConfiguration GetContainer => _landfillConfiguration.GetContainer;

       
        private void Update()
        {
            if (_inLandfill && _parentCap.transform.localRotation.x != 0 && GameCycle.Instance._gameState == GameCycle.GameState.Score)
            {
                _inLandfill = false;
            }
            if (!_inLandfill && _parentCap.transform.localRotation.x != 0 && GameCycle.Instance._gameState != GameCycle.GameState.Menu)
            {
                CloseLandfill(Time.realtimeSinceStartup);
            }
            else if (_inLandfill)
            {
                OpenLandfill(Time.realtimeSinceStartup);
                if (!_fillImage.gameObject.activeSelf)
                {
                    _fillImage.gameObject.SetActive(true);
                }
            }
            if (_fillImage.gameObject.activeSelf && _parentCap.transform.localRotation.x == 0)
            {
                _fillImage.gameObject.SetActive(false);
            }
        }

        public void SetStartTimeToCloseLandfill( float timePassed)
        {
            _startTimeInClose = Time.realtimeSinceStartup;
            _timePassed = timePassed;
            _inLandfill = false;
        }

        public void SetStartTimeToOpenLandfill(float startTime)
        {
            _startTimeInOpen = startTime;
            _inLandfill = true;
        }

        void OpenLandfill(float currentTime)
        {
            float _timePassedLandfill = ((currentTime - _startTimeInOpen) / GameConfiguration.Instance.timeHoldingLandfill) * 100;
            _parentCap.transform.localRotation = Quaternion.Euler(-_timePassedLandfill, 0, 0);
            _fillImage.fillAmount = _timePassedLandfill / 100;

        }
        
        void CloseLandfill(float currentTime)
        {
            float _timePassedLandfill = ((currentTime - _startTimeInClose) / (_timePassed + 0.1f));
            _parentCap.transform.localRotation = Quaternion.Euler(_parentCap.transform.localRotation.x +((_timePassed + 0.1f) * Time.deltaTime / 100), 0, 0);
            _fillImage.fillAmount = _parentCap.transform.localRotation.x / 100;
            if (_parentCap.transform.localRotation.x > 0)
            {
                _parentCap.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _fillImage.fillAmount = 0;
                _smokeFX.Play();
            }
        }
    }
}