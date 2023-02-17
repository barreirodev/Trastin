using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Box
{
    public class Sticker : MonoBehaviour
    {

        void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
