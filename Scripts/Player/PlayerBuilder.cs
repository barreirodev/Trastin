using Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerBuilder
    {
        PlayerMediator _prefab;
        Vector3 _position;
        Vector3 _rotation;
        Vector2 _speed;
        float _rotationSpeed;
        IInput _input;
        public enum InputMode
        {
            Keyboard,
            WiiFit
        }
        InputMode _inputMode;
        public enum Behaviour
        {
            Landfill,
            Park,
            Centre
        }
        Behaviour _behaviour;
        public PlayerBuilder FromPrefab(PlayerMediator prefab)
        {
            _prefab = prefab;
            return this;
        }
        public PlayerBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        public PlayerBuilder WithRotation(Vector3 rotation)
        {
            _rotation = rotation;
            return this;
        }
        public PlayerBuilder WithSpeed(Vector2 speed)
        {
            _speed = speed;
            return this;
        }
        public PlayerBuilder WithRotationSpeed(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
            return this;
        }
        public PlayerBuilder WithInput(IInput input)
        {
            _input = input;
            return this;
        }
        public PlayerBuilder WithInputMode(InputMode inputMode)
        {
            _inputMode = inputMode;
            return this;
        }
        public PlayerBuilder WithBehaviourMode(Behaviour behaviour)
        {
            _behaviour = behaviour;
            return this;
        }

        private IInput GetInput()
        {
            if (_input != null) return _input;

            switch (_inputMode)
            {
                case InputMode.Keyboard:
                    return new KeyboardAdapter();
                case InputMode.WiiFit:
                    return new WiiFitAdapter();
                default:
                    throw new System.Exception("Input not implemented");
            }
        }

        public PlayerMediator Build()
        {
            PlayerMediator player = Object.Instantiate(_prefab, _position, Quaternion.Euler(_rotation));
            MasterSceneManager.Instance.SelectSceneToInstantiate(player.gameObject);
            player.Configuration(GetInput(),_speed,_rotationSpeed,_behaviour);
            return player;
        }
    }
}