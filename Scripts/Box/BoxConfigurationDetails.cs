using System.Collections.Generic;
using UnityEngine;
using Landfill;

namespace Box
{
    [CreateAssetMenu(menuName = "Create box details", fileName = "BoxDetails", order = 2)]
    public class BoxConfigurationDetails : ScriptableObject
    {
        public enum Nature
        {
            None,
            Biodegradable,
            No_biodegradable
        }
        [SerializeField] private Nature _nature;
       
        [SerializeField] private OriginConfiguration _origin;

        [SerializeField] private ContainerConfiguration _container;

        [SerializeField] private Sprite[] _sprite;
        public Nature GetNature => _nature;
        public OriginConfiguration GetOrigin => _origin;
        public ContainerConfiguration GetContainer => _container;

        public Sprite Image(Dictionary<BoxConfigurationDetails, List<int>> usedNumbers, BoxConfigurationDetails details)
        {
            int newImage = Random.Range(0, _sprite.Length);
            int attemps = _sprite.Length;
            while (usedNumbers[details].Contains(newImage) && attemps > 0)
            {
                attemps--;
                newImage = Random.Range(0, _sprite.Length);
            }
            usedNumbers[details].Add(newImage);
            if (attemps <= 0)
            {
                usedNumbers[details].Clear();
            }
            return _sprite[newImage];
        }
    }
}