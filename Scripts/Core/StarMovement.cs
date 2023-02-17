using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class StarMovement : MonoBehaviour
    {
        [SerializeField] GameObject _score;

        void Update()
        {
            transform.position = Camera.main.ScreenToWorldPoint(_score.transform.position);
        }
    }
}