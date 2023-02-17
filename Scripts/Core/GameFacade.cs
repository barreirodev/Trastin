using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Box;
using Landfill;

namespace Core
{
    public class GameFacade : MonoBehaviour
    {
        [SerializeField] PlayerInstaller _playerInstaller;
        [SerializeField] BoxInstaller _boxInstaller;
        [SerializeField] LandfillController _landfillController;

        public void ShowLandfills()
        {
            _landfillController.ShuffleLandfill();
        }

        public void ShowPlayer()
        {
            _playerInstaller.SpawnPlayer();
        }

        public void ShowBox(bool neededNonBiodegradable = false)
        {
            _boxInstaller.SpawnBox(neededNonBiodegradable);
            AudioEffectsManager.Instance.ShowBox();
        }

        public void InteractWithLandfills()
        {
            _landfillController.InteractWithLandfills();
        }
    }
}
