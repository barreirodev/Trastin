using Landfill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Box;
using Core;

namespace Player
{
    public class PlayerPark : MonoBehaviour, IPlayer
    {
        PlayerMediator _player;

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

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out IBoxBehaviour boxBehaviour))
            {
                boxBehaviour.Taken();
            }
        }
    }
}
