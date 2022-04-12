using UnityEngine;

namespace EquipSystem
{
    public interface IWeaponEquipped:IEquipped
    {
        public GameObject ScabbardHolder { get; }
        public IEquippable ItemInScabbard { get; }

        public void PutWeaponInScabbard(IEquippable inItem,out IEquippable outItem);
        public void GetWeaponOutScabbard(out IEquippable outItem);
        public void EmptyTheScabbard();


    }
}
