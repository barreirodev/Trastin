using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Landfill;
using Core;

namespace Recycle
{
    public class RecycleMediator : MonoBehaviour
    {
        public BellConfiguration _bellConfiguration;
        [SerializeField] GameObject _incorrectExclamation;
        [SerializeField] Image _fillImage;
        public Text originText;
        [SerializeField] ParticleSystem _correctFX;
        [SerializeField] ParticleSystem _smoke;
        public bool _inLandfill;

        float _startTimeInOpen;

        public OriginConfiguration GetOrigin => _bellConfiguration.GetOrigin;
        public ParticleSystem GetSmoke => _smoke;

        private void Update()
        {           
            if (_inLandfill && _fillImage.fillAmount > 0 && GameCycle.Instance._gameState == GameCycle.GameState.Score)
            {
                _inLandfill = false;
                _fillImage.gameObject.SetActive(false);
             }

            else if (_inLandfill)
            {
                OpenLandfill(Time.realtimeSinceStartup);
                if (!_fillImage.gameObject.activeSelf)
                {
                    _fillImage.gameObject.SetActive(true);
                }
            }
            if (!_inLandfill && _fillImage.fillAmount > 0 && GameCycle.Instance._gameState != GameCycle.GameState.Menu)
            {
                if (_fillImage.fillAmount > 0)
                {
                    _fillImage.fillAmount = 0;
                    _fillImage.gameObject.SetActive(false);
                }
            }
        }

        public void SetStartTimeToCloseLandfill(float timePassed)
        {
            _inLandfill = false;
        }

        public void SetStartTimeToOpenLandfill(float startTime)
        {
            _startTimeInOpen = startTime;
            _inLandfill = true;
        }

        void OpenLandfill(float currentTime)
        {
            float _timePassedBell = ((currentTime - _startTimeInOpen) / GameConfiguration.Instance.timeLeavingBox) * 100;
            _fillImage.fillAmount = _timePassedBell / 100;

        }

    }
}