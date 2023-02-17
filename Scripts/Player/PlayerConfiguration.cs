using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [CreateAssetMenu(menuName = "Create player configuration", fileName = "PlayerConfiguration", order = 0)]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] Vector2 _speed;
        [SerializeField] Vector3 _position;
        [SerializeField] Vector3 _rotation;
        [SerializeField] float _rotationSpeed;
        [SerializeField] PlayerMediator _prefabBoy;
        [SerializeField] PlayerMediator _prefabGirl;

        public Vector2 Speed => _speed;
        public Vector3 Position => _position;
        public void SetXPosition(float x)
        {
            _position = new Vector3(x,_position.y,_position.z);
        }
        public Vector3 Rotation => _rotation;
        public float RotationSpeed => _rotationSpeed;
        public PlayerMediator Prefab(int player)
        {
            if (player == 0)
            {
                return _prefabBoy;
            }
            else
            {
                return _prefabGirl;
            }
        }
    }
}
