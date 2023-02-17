using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landfill
{
    [CreateAssetMenu(menuName = "Create landfill config", fileName = "LandfillConfiguration", order = 2)]
    public class LandfillConfiguration : ScriptableObject
    {
        [SerializeField] ContainerConfiguration _container;

        public ContainerConfiguration GetContainer => _container;

    }
}