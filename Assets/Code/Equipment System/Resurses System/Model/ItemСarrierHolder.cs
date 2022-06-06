using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{
    [System.Serializable]
    public class Item�arrierHolder : IItem�arrierHolder
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

        public Item�arrierHolder(Item�arrierHolder itholder)
        {
            _item = (EquipableItemBase)itholder.Item;
            _currentValue = itholder.CurrentValue;
            _maxItemValue = itholder.MaxItemValue;
        }
        public Item�arrierHolder(IEquippable item,int currentValue, int maxItemValue)
        {
            _item = (EquipableItemBase)item;
            _currentValue = currentValue;
            _maxItemValue = maxItemValue;
        }
        public void ChangeItemHolder(Item�arrierHolder holder)
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
        public IItem�arrierHolder GetItemInHolder(int value)
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
            Item�arrierHolder tempHolder = new Item�arrierHolder(Item, tempvalue, tempvalue);
            return tempHolder;
        }


    }
}
