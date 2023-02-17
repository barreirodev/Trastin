using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Park
{
    public class MaterialMovement : MonoBehaviour
    {
        Material _material;
        // Start is called before the first frame update
        void Start()
        {
            _material = GetComponent<Renderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameCycle.Instance._gameState == GameCycle.GameState.Playing)
            {
                _material.mainTextureOffset += new Vector2(0, Time.deltaTime * GameConfiguration.Instance.SpeedBox * 0.07f);
            }
        }
    }
}