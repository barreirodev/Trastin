using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landfill
{
    [CreateAssetMenu(menuName = "Create Container config", fileName = "ContainerConfiguration", order = 5)]
    public class ContainerConfiguration : ScriptableObject
{
        public enum Container
        {
            Organico,
            Amarillo,
            Verde,
            Azul,
            Gris
        }
        [SerializeField] Container _id;

        public Container GetID => _id;
    }
}
