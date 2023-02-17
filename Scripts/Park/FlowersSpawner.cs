using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Park
{
    public class FlowersSpawner : MonoBehaviour
    {
        public Transform flowersSpawner;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Destroyer"))
            {
                transform.position = flowersSpawner.position;
            }
        }
    }
}