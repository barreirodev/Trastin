using UnityEngine;
using Player;

namespace Input
{
    public class KeyboardAdapter : IInput
    {
        public Vector3 GetDirection() => Input.mousePosition;

    }
}
