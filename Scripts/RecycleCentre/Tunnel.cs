using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recycle
{
    public class Tunnel : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Box"))
                AudioEffectsManager.Instance.BoxThroughtTunel();
        }
    }
}
