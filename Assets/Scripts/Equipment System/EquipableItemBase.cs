using UnityEngine.UI;
using UnityEngine;

namespace EquipSystem
{ 
    [CreateAssetMenu(fileName = "Equipable Item", menuName = "Interactable Item/Equipable Item", order = 1)]
    public class EquipableItemBase : ScriptableObject, IEquippable
    {
        public EquipType ItemType => _itemType;

        public Mesh ItemMesh => _itemMesh;

        public float AttackModificator => _attackModificator;
        public float SpeedModificator => _speedModificator;

        public float DeffendsModificator => _deffendsModificator;

        public Sprite Icon => _icon;

        public string NameOfItem => _nameOfItem;

        public string Description => _description;

        public bool MageItem => _mageItemFlag;

        public bool SpearItem => _spearItemFlag;

        [SerializeField]
        private EquipType _itemType;

        [SerializeField]
        private Mesh _itemMesh;

        [SerializeField]
        private float _attackModificator;

        [SerializeField]
        private float _deffendsModificator;

        [SerializeField]
        private float _speedModificator;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]        
        private string _nameOfItem;

        [SerializeField]
        [TextArea(5,5)]
        private string _description;

        [SerializeField]
        private bool _mageItemFlag=false;
        [SerializeField]
        private bool _spearItemFlag=false;
    }
}
