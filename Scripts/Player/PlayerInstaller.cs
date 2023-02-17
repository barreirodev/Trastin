using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
namespace Player
{
    public class PlayerInstaller : MonoBehaviour
    {
        PlayerBuilder _playerBuilder;
        [SerializeField] bool _keyboardInput;
        [SerializeField] PlayerConfiguration _playerConfiguration;
        public enum Behaviour
        {
            Landfill,
            Park,
            Recycle
        }
        [SerializeField] Behaviour _behaviour;

        public void SelectPlayer(int player)
        {
            _playerBuilder = new PlayerBuilder()
                .FromPrefab(_playerConfiguration.Prefab(player))
                .WithPosition(_playerConfiguration.Position)
                .WithRotation(_playerConfiguration.Rotation)
                .WithSpeed(_playerConfiguration.Speed)
                .WithRotationSpeed(_playerConfiguration.RotationSpeed);
            SetInput(_playerBuilder);
            SetBehaviour(_playerBuilder);
        }

        void SetInput(PlayerBuilder playerBuilder)
        {
            if (_keyboardInput)
            {
                playerBuilder.WithInputMode(PlayerBuilder.InputMode.Keyboard);
            }
            else
            {
                playerBuilder.WithInputMode(PlayerBuilder.InputMode.WiiFit);
            }
        }

        void SetBehaviour(PlayerBuilder playerBuilder)
        {
            switch (_behaviour)
            {
                case Behaviour.Landfill:
                    playerBuilder.WithBehaviourMode(PlayerBuilder.Behaviour.Landfill);
                    break;
                case Behaviour.Park:
                    playerBuilder.WithBehaviourMode(PlayerBuilder.Behaviour.Park);
                    break;
                case Behaviour.Recycle:
                    playerBuilder.WithBehaviourMode(PlayerBuilder.Behaviour.Centre);
                    break;
            }
        }

        public void SpawnPlayer()
        {
            _playerBuilder.Build();
        }
    }
}
