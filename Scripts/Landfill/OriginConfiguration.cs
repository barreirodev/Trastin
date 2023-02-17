using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Landfill
{
    [CreateAssetMenu(menuName = "Create Origin config", fileName = "OriginConfiguration", order = 6)]
    public class OriginConfiguration : ScriptableObject
    {
        public enum Origin
        {
            None,
            Vegetal,
            Animal,
            Mineral,
            Fosil,
            Universal,
            Sintetico
        }
        [SerializeField] Origin _id;

        public Origin GetID => _id;
    }
}
