using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class WiiFitAdapter : MonoBehaviour, IInput
    {
        public Vector3 GetDirection() => new Vector3(0,0,0);

    }
}
