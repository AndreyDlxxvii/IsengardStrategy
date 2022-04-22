using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace EquipSystem
{

    public class BowWeaponEquipHolder : MonoBehaviour, IWeaponEquipped
    {

        public EquipType HolderType => _holderType;

        public GameObject EquipHolderObject => gameObject;

        public IEquippable EquippableItem => _equippableItem;

        public GameObject ScabbardHolder => _scabbardHolder;
        public IEquippable ItemInScabbard => _itemInScabbard;

        [SerializeField]
        private EquipType _holderType=EquipType.Bow;

        [SerializeField]
        private EquipableItemBase _quiverItem;

        [SerializeField]
        private GameObject _quiverHolder;
        

        [SerializeField]
        private GameObject _scabbardHolder;
        private IEquippable _itemInScabbard;
        private MeshFilter _scabbardMesh;

        private MeshFilter equipHolderMesh;
        private Mesh equipHolderMeshBase;
        private IEquippable _equippableItem;


        private void Awake()
        {
            equipHolderMesh = gameObject.GetComponent<MeshFilter>();
            equipHolderMeshBase = equipHolderMesh.sharedMesh;
            _quiverHolder.GetComponent<MeshFilter>().sharedMesh = _quiverItem.ItemMesh;
            _quiverHolder.SetActive(false);
            _scabbardMesh = _scabbardHolder.GetComponent<MeshFilter>();
        }

        public void GetEquipped(IEquippable item)
        {
            _equippableItem = item;
            if (equipHolderMesh.mesh != item.ItemMesh && _holderType == item.ItemType)
            {
                equipHolderMesh.mesh = item.ItemMesh;
                _quiverHolder.SetActive(true);
            }
        }

        public void UnEquiped(IEquippable item)
        {
            _equippableItem = null;
            if (item.ItemMesh == equipHolderMesh.sharedMesh && _holderType == item.ItemType)
            {
                equipHolderMesh.sharedMesh = equipHolderMeshBase;
                _quiverHolder.SetActive(false);
            }
        }
        public void UnEquiped()
        {
            _equippableItem = null;
            equipHolderMesh.sharedMesh = equipHolderMeshBase;
            _quiverHolder.SetActive(false);
        }

        public void PutWeaponInScabbard(IEquippable inItem, out IEquippable outItem)
        {
            outItem = _itemInScabbard;
            if (inItem!=null)
            {
                _scabbardMesh.sharedMesh = inItem.ItemMesh;
                _itemInScabbard = inItem;            
            }
            else
            {
                _scabbardMesh.sharedMesh = null;
                _itemInScabbard = null;
            }
        }

        public void GetWeaponOutScabbard(out IEquippable outItem)
        {
            outItem = _itemInScabbard;
            if (ItemInScabbard != null)
            {                
                _itemInScabbard = null;
                _scabbardMesh.sharedMesh = null;
            }
        }

        public void EmptyTheScabbard()
        {
            _itemInScabbard = null;
            _scabbardMesh.sharedMesh = null;
        }
    }
}

