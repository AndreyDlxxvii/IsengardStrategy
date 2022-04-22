using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{ 
    
    public class ArmourEquipHolder : MonoBehaviour,IEquipped
    {
        public EquipType HolderType => _holderType;

        public GameObject EquipHolderObject => gameObject;

        public IEquippable EquippableItem => _equippableItem;

        [SerializeField]
        private EquipType _holderType;

        private IEquippable _equippableItem;

        private SkinnedMeshRenderer ArmourEquipHolderMesh;
        private Mesh ArmourEquipHolderMeshBase;
        private bool _activeFlag=true;


        private void Awake()
        {
            ArmourEquipHolderMesh = gameObject.GetComponent<SkinnedMeshRenderer>();
            ArmourEquipHolderMeshBase = ArmourEquipHolderMesh.sharedMesh;
        }

        public void GetEquipped(IEquippable item)
        {   
            if (ArmourEquipHolderMesh.sharedMesh != item.ItemMesh && _holderType == item.ItemType)
            {
                OnEnableArmour();
                _equippableItem = item;
                ArmourEquipHolderMesh.sharedMesh = item.ItemMesh;                 
            }
        }

        public void UnEquiped(IEquippable item)
        {
            _equippableItem = null;
            if (item.ItemMesh == ArmourEquipHolderMesh.sharedMesh && _holderType == item.ItemType)
                ArmourEquipHolderMesh.sharedMesh = ArmourEquipHolderMeshBase;
        }

        public void UnEquiped()
        {
            _equippableItem = null;
            ArmourEquipHolderMesh.sharedMesh = ArmourEquipHolderMeshBase;
        }
        public void DesableArmour()
        {
            _activeFlag = false;
            ArmourEquipHolderMesh.sharedMesh = ArmourEquipHolderMeshBase;
            gameObject.SetActive(_activeFlag);
        }
        public void OnEnableArmour()
        {
            if (!_activeFlag)
            {                
                _activeFlag = true;
                gameObject.SetActive(_activeFlag);
            }            
        }



    }
}
