using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;
using Core;

namespace Park {
    public class DecorationSpawner : MonoBehaviour
    {
        public List<Transform> decoSpawnerRight;
        public List<Transform> decoSpawnerLeft;
        public List<GameObject> objectsToSpanwnHighProbable;
        public List<GameObject> objectsToSpanwnLessProbable;

        void Start()
        {
            StartCoroutine(GenerateDeco());
        }

        IEnumerator GenerateDeco()
        {
            while (GameCycle.Instance._gameState != GameCycle.GameState.Playing)
            {
                yield return null;
            }
            while (GameCycle.Instance._gameState == GameCycle.GameState.Playing)
            {
                for (int i = 0; i < decoSpawnerLeft.Count; ++i)
                {
                    if (Random.Range(0,101) <= 30)
                    {
                        GameObject newItem;
                        GameObject objectChoosen;
                        if (Random.Range(0, 101) <= 70)
                        {
                            objectChoosen = objectsToSpanwnHighProbable[Random.Range(0, objectsToSpanwnHighProbable.Count)];
                        }
                        else
                        {
                            objectChoosen = objectsToSpanwnLessProbable[Random.Range(0, objectsToSpanwnLessProbable.Count)];
                        }
                        newItem = Instantiate(objectChoosen, new Vector3(decoSpawnerLeft[i].position.x, objectChoosen.transform.localPosition.y, decoSpawnerLeft[i].position.z), Quaternion.Euler(0f, 180f, 0f));
                        newItem.AddComponent<Destroyer>();
                        LinearMovement linear = newItem.AddComponent<LinearMovement>();
                        linear.SetMovement(new Vector3(-1, 0, 0));

                    }

                }

                for (int i = 0; i < decoSpawnerRight.Count; ++i)
                {
                    if (Random.Range(0, 101) <= 30)
                    {
                        GameObject newItem;
                        GameObject objectChoosen;
                        if (Random.Range(0, 101) <= 70)
                        {
                            objectChoosen = objectsToSpanwnHighProbable[Random.Range(0, objectsToSpanwnHighProbable.Count)];
                        }
                        else
                        {
                            objectChoosen = objectsToSpanwnLessProbable[Random.Range(0, objectsToSpanwnLessProbable.Count)];
                        }
                        newItem = Instantiate(objectChoosen, new Vector3(decoSpawnerRight[i].position.x, objectChoosen.transform.localPosition.y, decoSpawnerRight[i].position.z), Quaternion.identity);
                        newItem.AddComponent<Destroyer>();
                        LinearMovement linear = newItem.AddComponent<LinearMovement>();
                        linear.SetMovement(new Vector3(-1, 0, 0));

                    }

                }
                yield return new WaitForSeconds(2.0f);
            }
        }

    }
}