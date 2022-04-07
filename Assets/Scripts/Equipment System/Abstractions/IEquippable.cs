using UnityEngine.UI;
using UnityEngine;

namespace EquipSystem
{ 
    public interface IEquippable : ISelectable
    {
        public EquipType ItemType { get; }
        public Mesh ItemMesh { get; }
        public float AttackModificator { get; }
        public float DeffendsModificator { get; }
        public string NameOfItem { get; }
        
        public string Description { get; }
    }
}