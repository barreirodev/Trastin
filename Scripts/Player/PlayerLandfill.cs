using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Landfill;
using Box;

namespace Player
{
    public class PlayerLandfill : MonoBehaviour, IPlayer
    {
        public GameObject _box;
        float _timePassedInLandfill;

        GameObject _container;
        float _startTimeToPickUp;
        PlayerMediator _player;
        public bool _inBox;
        bool _inLandfill;

        public void AddPlayerMediator(PlayerMediator player)
        {
            _player = player;
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
                    _player._playerState = BoxPlaced(_container.GetComponentInParent<LandfillMediator>(), _startTimeToPickUp);
                }

                if (_box != null)
                {
                    _box.GetComponent<BoxMediator>()._positionPlayer = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z); 
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

        public PlayerMediator.PlayerState BoxPlaced(LandfillMediator landfill,  float startTimeToOpen)
        {
            if (_box != null)
            {
                if (_inLandfill && GameCycle.Instance._gameState == GameCycle.GameState.Playing)
                {
                    landfill.SetStartTimeToOpenLandfill(startTimeToOpen);
                    _timePassedInLandfill += Time.deltaTime;
                    if (Time.realtimeSinceStartup >= startTimeToOpen + GameConfiguration.Instance.timeHoldingLandfill)
                    {
                        if (landfill.GetContainer.GetID == _box.GetComponent<BoxMediator>().GetContainer.GetID)
                        {
                            GameCycle.Instance.AddScore(1);
                            AudioEffectsManager.Instance.Correct();
                            _box.GetComponent<BoxMediator>().Notify();
                            Destroy(_box);
                            _box = null;
                            landfill.SetStartTimeToCloseLandfill( _timePassedInLandfill);
                            _timePassedInLandfill = 0;
                            AudioEffectsManager.Instance.CloseLandfill();
                            _player._playerAnimator.SetBool("WithBox", false);
                            return PlayerMediator.PlayerState.Walking;
                        }
                        else
                        {
                            GameCycle.Instance.AddScore(-1);
                            AudioEffectsManager.Instance.Error();
                            _box.GetComponent<BoxMediator>().Notify();

                            Destroy(_box);
                            _box = null;
                            landfill.SetStartTimeToCloseLandfill(_timePassedInLandfill);
                            _timePassedInLandfill = 0;
                            AudioEffectsManager.Instance.CloseLandfill();
                            _player._playerAnimator.SetBool("WithBox", false);
                            return PlayerMediator.PlayerState.Walking;
                        }

                    }
                }
                else
                {
                    landfill.SetStartTimeToCloseLandfill( _timePassedInLandfill);
                    _timePassedInLandfill = 0;
                    AudioEffectsManager.Instance.CloseLandfill();
                    return PlayerMediator.PlayerState.Walking;
                }
            }
            else{
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
                    box.transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
                    _box = box;
                    _box.GetComponent<BoxMediator>()._imageFillProgress.gameObject.SetActive(false);
                    _box.GetComponent<BoxMediator>()._inPlayer = true;
                    _box.GetComponent<BoxCollider>().enabled = false;
                    _player._playerAnimator.SetBool("WithBox",true);
                    return PlayerMediator.PlayerState.Walking;
                }
            }
            else
            {
                _box = null;

                _box.GetComponent<BoxMediator>()._imageFillProgress.gameObject.SetActive(false);
                return PlayerMediator.PlayerState.Walking;
            }
            return PlayerMediator.PlayerState.BoxTaking;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Box")
            {
                _box = collision.gameObject;
                _player._playerState = PlayerMediator.PlayerState.BoxTaking;
                _startTimeToPickUp = Time.realtimeSinceStartup;
                _inBox = true;
            }

            else if (collision.gameObject.tag == "Landfill")
            {
                _container = collision.gameObject;
                _player._playerState = PlayerMediator.PlayerState.InLandfill;
                _startTimeToPickUp = Time.realtimeSinceStartup;
                _inLandfill = true;
                if (_box != null)
                {
                    AudioEffectsManager.Instance.OpenLandfill();
                }
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.tag == "Box" && _box.GetComponent<BoxMediator>()._inPlayer != true)
            {
                _box = null;
                _inBox = false;
                _player._playerState = PlayerMediator.PlayerState.Walking;
            }
            else if (collision.gameObject.tag == "Landfill")
            {
                collision.gameObject.GetComponentInParent<LandfillMediator>()._inLandfill = false;
                AudioEffectsManager.Instance.IsOpenLandfillSound();
                _container = null;
                _inLandfill = false;
                _player._playerState = PlayerMediator.PlayerState.Walking;
                
            }
        }

    }
}

