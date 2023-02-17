using UnityEngine;
using Core;

namespace Box {
    public class BoxDisappears : MonoBehaviour, IBoxBehaviour
    {
        public void Taken()
        {
            if (gameObject.GetComponent<BoxMediator>().GetNature == BoxConfigurationDetails.Nature.No_biodegradable)
            {
                GameCycle.Instance.AddScore(1);
                AudioEffectsManager.Instance.Correct();

            }
            else
            {
                GameCycle.Instance.AddScore(-1);
                AudioEffectsManager.Instance.Error();
            }
            Destroy(gameObject);
        }
    }
}