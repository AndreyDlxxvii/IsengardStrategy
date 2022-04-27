using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{
    public enum EquipType 
    {
        Head,
        Shoulderpads,
        Armour,
        Arms,
        Legs,
        WorkerWeapon=20,
        Bags,
        Resurses,
        Quiver,
        LeftHandMeleeWeapon,
        RightHandMeleeWeapon,
        LeftHandShield,
        Bow,
        MageRobe,
        Shoulderpads2,
        Spear,
        MageStaff
    }   
    public class EquipSystemBehaviour : MonoBehaviour 
    {
        [Header("Slot to equip item")]
        [SerializeField]
        private EquipableItemBase _equipableItem;

        [Header("Slot to Unequip item")]
        [SerializeField]
        private EquipableItemBase _unEquipableItem;
        
        
        private ArmourEquipHolder[] _armourEquipHolders;
        private IWeaponEquipped[] _weaponEquipHolders;
        private WorkerEquipHolder[] _workerEquipHolder;

        private bool isIsWorker = false;
        

        public void Awake()
        {
            GetEquipHolders();
        }
        private void GetEquipHolders()
        {
            _armourEquipHolders = gameObject.GetComponentsInChildren<ArmourEquipHolder>();
            _weaponEquipHolders = gameObject.GetComponentsInChildren<IWeaponEquipped>();
            _workerEquipHolder= gameObject.GetComponentsInChildren<WorkerEquipHolder>();
        }

        public void GetEquipArmour(IEquippable item)
        {
            UnEquipWorkerAll();
            if (item.ItemType==EquipType.Armour)
            {
                foreach (ArmourEquipHolder holder in _armourEquipHolders)
                {
                    if (holder.HolderType==EquipType.MageRobe)
                    {
                        holder.DesableArmour();
                    }                    
                    if (item.ItemType == holder.HolderType)
                    {
                        holder.GetEquipped(item);                       
                    }
                }
            }
            else
                if (item.ItemType==EquipType.Shoulderpads)
                {
                    foreach (ArmourEquipHolder holder in _armourEquipHolders)
                    { 
                        if (holder.HolderType == EquipType.Shoulderpads2)
                        {
                            holder.DesableArmour();
                        }
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.GetEquipped(item);
                        }
                    }
                }
            else
                foreach (ArmourEquipHolder holder in _armourEquipHolders)
                {
                    if (item.ItemType == holder.HolderType)
                    {
                        holder.GetEquipped(item);
                        break;
                    }
                }
        }
        public void GetEquipedMagikRobe(IEquippable item)
        {
            UnEquipWorkerAll();
            foreach (ArmourEquipHolder holder in _armourEquipHolders)
            {
                if (holder.HolderType==EquipType.Armour)
                {
                    holder.DesableArmour();
                }
                if (item.ItemType == holder.HolderType)
                {                    
                    holder.GetEquipped(item);                    
                }
            }
        }        
        public void GetEquipedShoulderpads2(IEquippable item)
        {
            UnEquipWorkerAll();
            foreach (ArmourEquipHolder holder in _armourEquipHolders)
            {
                if (holder.HolderType == EquipType.Shoulderpads)
                {
                    holder.DesableArmour();
                }
                if (item.ItemType == holder.HolderType)
                {                    
                    holder.GetEquipped(item);
                }
            }
        }

        public void GetEquipped(IEquippable item)
        {            
            switch(item.ItemType)
            {
                case  EquipType.Arms:
                    GetEquipArmour(item);
                    break;
                case EquipType.Armour:
                    GetEquipArmour(item);
                    break;
                case EquipType.Legs:
                    GetEquipArmour(item);
                    break;
                case EquipType.Shoulderpads:
                    GetEquipArmour(item);
                    break;
                case EquipType.Head:
                    GetEquipArmour(item);
                    break;
                case EquipType.MageRobe:
                    GetEquipedMagikRobe(item);
                    break;
                case EquipType.Shoulderpads2:
                    GetEquipedShoulderpads2(item);
                    break;


                case EquipType.LeftHandMeleeWeapon :
                    UnEquipWorkerAll();
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    {
                        if (holder.HolderType==EquipType.LeftHandShield | holder.HolderType == EquipType.Bow  | holder.HolderType == EquipType.Quiver)
                        {
                            holder.UnEquiped();
                        }
                        if (item.ItemType==holder.HolderType)
                        {
                            holder.GetEquipped(item);
                        }                       
                    }break;

                case EquipType.RightHandMeleeWeapon:
                    UnEquipWorkerAll();
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    {
                        if (holder.HolderType == EquipType.Bow || holder.HolderType == EquipType.Quiver)
                        {
                            holder.UnEquiped();
                        }
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.GetEquipped(item);
                        }
                    }                    
                    break;

                case EquipType.LeftHandShield:
                    UnEquipWorkerAll();
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    {
                        if (holder.HolderType == EquipType.Bow || holder.HolderType == EquipType.Quiver || holder.HolderType == EquipType.LeftHandMeleeWeapon)
                        {
                            holder.UnEquiped();
                        }
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.GetEquipped(item);
                        }
                    }                    
                    break;                
                case EquipType.Bow :
                    UnEquipWorkerAll();
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    {
                        if (holder.HolderType == EquipType.LeftHandMeleeWeapon | holder.HolderType == EquipType.RightHandMeleeWeapon | holder.HolderType == EquipType.LeftHandShield)
                        {
                            holder.UnEquiped();
                        }
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.GetEquipped(item);
                        }
                    }
                    break;
                case (EquipType.WorkerWeapon):                    
                    GetWorker(item);                    
                    break;
                case (EquipType.Bags):
                    GetWorker(item);
                    break;
                case (EquipType.Resurses):
                    GetWorker(item);
                    break;            
            }
        }
        public void UnEquiped(IEquippable item)
        {
            switch (item.ItemType)
            {
                case EquipType.Arms:
                    UnEquipArmour(item);
                    break;
                case EquipType.Armour:
                    UnEquipArmour(item);
                    break;
                case EquipType.Legs:
                    UnEquipArmour(item);
                    break;
                case EquipType.Shoulderpads:
                    UnEquipArmour(item);
                    break;
                case EquipType.Head:
                    UnEquipArmour(item);
                    break;
                case EquipType.MageRobe:
                    UnEquipArmour(item);
                    break;
                case EquipType.Shoulderpads2:
                    UnEquipArmour(item);
                    break;
                case EquipType.LeftHandMeleeWeapon:                    
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    {                        
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.UnEquiped(item);
                        }
                    }
                    break;

                case EquipType.RightHandMeleeWeapon:                    
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    {                        
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.UnEquiped(item);
                        }
                    }
                    break;

                case EquipType.LeftHandShield:                   
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    {                        
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.UnEquiped(item);
                        }
                    }
                    break;
                case EquipType.Bow:                                       
                    foreach (IWeaponEquipped holder in _weaponEquipHolders)
                    { 
                        if (item.ItemType == holder.HolderType)
                        {
                            holder.UnEquiped(item);
                        }
                    }
                    break;
                case (EquipType.WorkerWeapon):
                    UnEquipWorker(item);                    
                    break;
                case (EquipType.Resurses):
                    UnEquipWorker(item);
                    break;
                case (EquipType.Bags):
                    UnEquipWorker(item);
                    break;
            }
        }
        public void UnEquipArmour(IEquippable item)
        {
            if (item.ItemType==EquipType.MageRobe)
            {
                foreach (ArmourEquipHolder holder in _armourEquipHolders)
                {
                    if (holder.HolderType==EquipType.Armour)
                    {
                        holder.OnEnableArmour();
                    }
                    if (item.ItemType == holder.HolderType)
                    {
                        holder.UnEquiped(item);                        
                    }
                }
            }
            else
            foreach (ArmourEquipHolder holder in _armourEquipHolders)
            {                
                if (item.ItemType == holder.HolderType)
                {
                    holder.UnEquiped(item);
                    break;
                }
            }
        }
        public void GetWorker(IEquippable item)
        {
            if (!isIsWorker)
            {
                UnEquipAllWeapons();
                UnEquipAllArmour();
            }
            foreach (WorkerEquipHolder holder in _workerEquipHolder)
            {
               if (item.ItemType==EquipType.Bags | item.ItemType == EquipType.Resurses)
                {
                    if (holder.HolderType!=EquipType.WorkerWeapon & holder.HolderType!=item.ItemType)
                    {
                        holder.UnEquiped();
                    }
                    else
                    {
                        if (holder.HolderType==item.ItemType)
                        { 
                            holder.GetEquipped(item);
                        }
                    }
                }
               else
                {
                    if (holder.HolderType==item.ItemType)
                    { 
                    holder.GetEquipped(item);
                        break;
                    }
                }
                
            }
            isIsWorker = true;
            

        }
        public void UnEquipAllArmour()
        {
            foreach(ArmourEquipHolder holder in _armourEquipHolders)
            {
                if(holder.HolderType==EquipType.Armour)
                {
                    holder.OnEnableArmour();
                }
                holder.UnEquiped();
                
            }
        }
        public void UnEquipAllWeapons()
        {
            foreach (IWeaponEquipped holder in _weaponEquipHolders)
            {
                holder.UnEquiped();
            }
        }
        public void UnEquipWorker( IEquippable item)
        {
            if (isIsWorker)
            {
                foreach (WorkerEquipHolder holder in _workerEquipHolder)
                {
                    holder.UnEquiped(item);
                }
            }
        }
        public void UnEquipWorkerAll()
        {  
            if (isIsWorker)
            { 
                foreach (WorkerEquipHolder holder in _workerEquipHolder)
                {
                    holder.UnEquiped();
                }
            }
            isIsWorker = false;
        }

        public void CheckScabbard(IEquippable inItem,IWeaponEquipped holder)
        {
            switch(inItem.ItemType)
            {
                case EquipType.Bow:
                    if(holder.HolderType==EquipType.Bow)
                    {
                        break;
                    }
                    else
                        if(holder.HolderType==EquipType.LeftHandMeleeWeapon| holder.HolderType == EquipType.LeftHandShield)
                        {
                            holder.EmptyTheScabbard();
                        }
                    else
                    {
                        holder.PutWeaponInScabbard(holder.EquippableItem, out IEquippable outItem);
                    }
                    break;       
            }
        }
        public void PutAllWeaponsInScabbards ()
        {
            foreach(IWeaponEquipped holder in _weaponEquipHolders)
            {
                if (holder.EquippableItem!=null)
                    { 
                    holder.PutWeaponInScabbard(holder.EquippableItem, out IEquippable outItem);
                    holder.UnEquiped();
                    if (outItem!=null)
                    { 
                        GetEquipped(outItem);
                        break;
                    }
                }
            }
        }
        public void GetAllWeaponOutScabbards()
        {
            foreach (IWeaponEquipped holder in _weaponEquipHolders)
            {
                holder.GetWeaponOutScabbard( out IEquippable outItem);
                if (holder.EquippableItem!=null)
                { 
                    holder.PutWeaponInScabbard(holder.EquippableItem, out IEquippable outItemNew);
                }
                if (outItem!=null)
                { 
                    GetEquipped(outItem);
                    break;
                }
            }
        }
        public void UnEquipAllScabbards()
        {
            foreach (IWeaponEquipped holder in _weaponEquipHolders)
            {
                holder.GetWeaponOutScabbard(out IEquippable outItem);
            }
        }
        public EquipType CheckPlayerState()
        {
            foreach (IWeaponEquipped holder in _weaponEquipHolders)
            {
                if (holder.EquippableItem != null)
                {
                    if (holder.EquippableItem.MageItem)
                    {
                        return EquipType.MageStaff;
                    }
                    else
                        if (holder.EquippableItem.SpearItem)
                        {
                        return EquipType.Spear;
                        }
                    return holder.EquippableItem.ItemType;                    
                }
            }
            return EquipType.WorkerWeapon;
        }
        public bool CheckResurseHolders()
        {
            foreach (WorkerEquipHolder holder in _workerEquipHolder)
            {
                if (holder.HolderType == EquipType.Resurses & holder.EquippableItem != null)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckBagHolders()
        {
            foreach (WorkerEquipHolder holder in _workerEquipHolder)
            {
                if (holder.HolderType==EquipType.Bags&holder.EquippableItem!=null)
                {
                    return true; 
                }
            }
            return false;
        }
        public void OnValidate()
        {
            if (_equipableItem!=null)
            { 
            GetEquipped(_equipableItem);
                _equipableItem = null;
            }
            if (_unEquipableItem!=null)
            {
                UnEquiped(_unEquipableItem);
                _unEquipableItem = null;
            }
        }



    }
}
