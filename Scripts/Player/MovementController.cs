using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        Vector2 _speed;
        float _rotationSpeed;

        public void Configure( Vector2 speed, float rotationSpeed)
        {
            _speed = speed;
            _rotationSpeed = rotationSpeed;
        }

        public void Move(Vector3 direction)
        {

            transform.position = new Vector3(direction.x, 0, direction.z);
        }
    }

}