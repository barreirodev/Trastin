using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Box {
    public class BoxInstaller : MonoBehaviour,IObserver
    {
        private BoxBuilder _boxBuilder;
        [SerializeField] private BoxConfiguration _boxConfiguration;
        [SerializeField] private EventBus _newBoxCreated;

        private Dictionary<BoxConfigurationDetails, List<int>> _numberOfImagesUsed = new Dictionary<BoxConfigurationDetails, List<int>>();

        private void Awake()
        {
            _boxConfiguration.InitializeNumberOfImagesUsed(_numberOfImagesUsed);
        }

        public void SpawnBox(bool neededNonBiodegradable = false)
        {
            BoxConfigurationDetails details;

            details = _boxConfiguration.GetBox(neededNonBiodegradable, Random.Range(0, 101) <= GameConfiguration.Instance.hardBoxPercentaje);

            _boxBuilder = new BoxBuilder(_newBoxCreated)
            .FromPrefab(_boxConfiguration.Prefab)
            .WithPosition(_boxConfiguration.Position())
            .WithNature(details.GetNature)
            .WithOrigin(details.GetOrigin)
            .WithContainer(details.GetContainer)
            .WithSprite(details.Image(_numberOfImagesUsed,details))
            .WithMovement((BoxBuilder.Movement)_boxConfiguration.TypeMovement);
            SetFunctionability(_boxBuilder);

            _boxBuilder.Build(this);
        }
        void SetFunctionability(BoxBuilder boxBuilder)
        {
            if (_boxConfiguration.DisappearOnPickUp)
            {
                boxBuilder.WithBehaviour(BoxBuilder.Behaviour.Disappear);
            }
            else
            {
                boxBuilder.WithBehaviour(BoxBuilder.Behaviour.OverHead);
            }
        }

        public void Updated(ISubject subject)
        {
            if (subject is BoxMediator)
            {
                if (Random.Range(0, 2) == 1)
                    SpawnBox(true);
                else
                    SpawnBox();
            }
        }
    }
}

