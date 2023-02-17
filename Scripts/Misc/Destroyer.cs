using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public class Destroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Destroyer"))
            {
                Destroy(gameObject);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Destroyer"))
            {
                Destroy(gameObject);
            }
        }
    }
}