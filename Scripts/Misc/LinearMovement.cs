using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Misc
{
    public class LinearMovement : MonoBehaviour
    {
       [SerializeField]  Vector3 _movement;
        public void SetMovement(Vector3 movement)
        {
            _movement = movement;
        }

        void Update()
        {
            if (GameCycle.Instance._gameState == GameCycle.GameState.Playing)
            {
                transform.position += new Vector3(GameConfiguration.Instance.SpeedBox * _movement.x, 0, GameConfiguration.Instance.SpeedBox * -_movement.z) * Time.deltaTime;
            }
        }
    }
}