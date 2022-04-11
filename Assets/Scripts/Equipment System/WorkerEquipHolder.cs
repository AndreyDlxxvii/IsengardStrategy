using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{ 
    
    public class WorkerEquipHolder : MonoBehaviour, IEquipped
    {

        public EquipType HolderType => _holderType;

        public GameObject EquipHolderObject => gameObject;

        public IEquippable EquippableItem => _equippableItem;

        [SerializeField]
        private EquipType _holderType;

        private MeshFilter equipHolderMesh;
        private Mesh equipHolderMeshBase;
        private IEquippable _equippableItem;


        private void Awake()
        {
            equipHolderMesh = gameObject.GetComponent<MeshFilter>();
            equipHolderMeshBase = equipHolderMesh.sharedMesh;
        }

        public void GetEquipped(IEquippable item)
        {
            _equippableItem = item;
            if (equipHolderMesh.mesh!=item.ItemMesh && _holderType==item.ItemType)
            {
                equipHolderMesh.sharedMesh = item.ItemMesh;
            }
        }

        public void UnEquiped(IEquippable item)
        {
            _equippableItem = null;
            if (item.ItemMesh == equipHolderMesh.sharedMesh && _holderType == item.ItemType)
            {
                equipHolderMesh.sharedMesh = equipHolderMeshBase;                
            }
        }
        public void UnEquiped()
        {
            _equippableItem = null;
            equipHolderMesh.sharedMesh = equipHolderMeshBase;
        }
    }
}
