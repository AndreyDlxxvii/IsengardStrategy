using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{
    [System.Serializable]
    public class ItemÑarrierHolder : IItemÑarrierHolder
    {
        public IEquippable Item => _item;

        public int CurrentValue => _currentValue;

        public int MaxItemValue => _maxItemValue;

        [SerializeField]
        private EquipableItemBase _item;
        [SerializeField]
        private int _currentValue;
        [SerializeField]
        private int _maxItemValue;

        public ItemÑarrierHolder(ItemÑarrierHolder itholder)
        {
            _item = (EquipableItemBase)itholder.Item;
            _currentValue = itholder.CurrentValue;
            _maxItemValue = itholder.MaxItemValue;
        }
        public ItemÑarrierHolder(IEquippable item,int currentValue, int maxItemValue)
        {
            _item = (EquipableItemBase)item;
            _currentValue = currentValue;
            _maxItemValue = maxItemValue;
        }
        public void ChangeItemHolder(ItemÑarrierHolder holder)
        {
            _item = (EquipableItemBase)holder.Item;
            _currentValue = holder.CurrentValue;
            _maxItemValue = holder.MaxItemValue;
        }
        public int AddItemValue(IEquippable item, int value)
        {
            var tempValue = 0;
            if (item==Item)
            {
                if ((value+CurrentValue)<=MaxItemValue)
                {
                    _currentValue += value;
                }
                else
                {
                    tempValue = CurrentValue + value - MaxItemValue;
                    _currentValue = MaxItemValue;
                    Debug.Log("Holder is full");
                }
            }
            return tempValue;
        }
        public IItemÑarrierHolder GetItemInHolder(int value)
        {
            int tempvalue = value;
            if (value>CurrentValue)
            {
                tempvalue = CurrentValue;
                _currentValue = 0;
                Debug.Log("Not enough Item in Holder");
            }
            else
            {
                _currentValue -= tempvalue;
            }
            ItemÑarrierHolder tempHolder = new ItemÑarrierHolder(Item, tempvalue, tempvalue);
            return tempHolder;
        }


    }
}
