using UnityEngine.UI;
using UnityEngine;

namespace EquipSystem
{ 
    [SerializeField]
    public interface IEquippable : ISelectable
    {
        public EquipType ItemType { get; }
        public Mesh ItemMesh { get; }
        public float AttackModificator { get; }
        public float DeffendsModificator { get; }
        public string NameOfItem { get; }
        
        public string Description { get; }

        public bool MageItem { get; }
        public bool SpearItem { get; }
    }
}