using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Box;
using System.Linq;

namespace Park
{
    public class BoxSpawner : MonoBehaviour
    {
        [SerializeField] Transform[] _spawnPositions;
        [SerializeField] GameFacade _gameFacade;
        [SerializeField] BoxConfiguration _boxConfig;
        int _maxNumberBoxes = 4;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(InvokeBoxes());
        }
        IEnumerator InvokeBoxes()
        {
            while (GameCycle.Instance._gameState == GameCycle.GameState.Menu) { yield return new WaitForEndOfFrame(); }
            while (GameCycle.Instance._gameState == GameCycle.GameState.Playing)
            {
                Spawn();
                yield return new WaitForSeconds(GameConfiguration.Instance.TimeToAppearMoreBox);
            }
        }
        void Spawn()
        {
            Random.InitState(Random.Range(0,10000));
            List<int> spawnBoxAtThisPosition = new List<int>();
            List<int> positions = new List<int>();
            bool nonBiodegradableSpawned = false;
            int counter = 0;
            for (int i = 0; i < _maxNumberBoxes; ++i)
            {
                if (Random.Range(0, 2) == 1 && GameConfiguration.Instance.NumberBox != counter)
                {
                    spawnBoxAtThisPosition.Add(1);
                    counter++;
                }
                else
                {
                    spawnBoxAtThisPosition.Add(0);
                }
               
                positions.Add(i);
            }

            if (!spawnBoxAtThisPosition.Contains(0))
            {
                int currentPosition = Random.Range(0, positions.Count);
                _boxConfig.Position(_spawnPositions[positions[currentPosition]].position);
                positions.Remove(currentPosition);
                _gameFacade.ShowBox(true);
                nonBiodegradableSpawned = true;
                for (int i = 0; i < positions.Count; ++i)
                {                      
                    _boxConfig.Position(_spawnPositions[positions[i]].position);
                    _gameFacade.ShowBox();
                }

            }
            else
            {
                for (int i = 0; i < spawnBoxAtThisPosition.Count; ++i)
                {
                    int index = Random.Range(i, spawnBoxAtThisPosition.Count);
                    int oldValue = spawnBoxAtThisPosition[i];
                    spawnBoxAtThisPosition[i] = spawnBoxAtThisPosition[index];
                    spawnBoxAtThisPosition[index] = oldValue; 
                }
                for (int i = 0; i < spawnBoxAtThisPosition.Count; ++i)
                {
                    if (spawnBoxAtThisPosition[i] == 1)
                    {
                        _boxConfig.Position(_spawnPositions[i].position);
                        if (Random.Range(0,2) == 1 && !nonBiodegradableSpawned)
                        {                    
                            _gameFacade.ShowBox(true);
                            nonBiodegradableSpawned = true;
                        }
                        else
                        {
                            _gameFacade.ShowBox();
                        }
                    }
                }
            }
        }
    }
}