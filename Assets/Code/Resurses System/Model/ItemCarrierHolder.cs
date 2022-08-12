using ResurseSystem;

namespace EquipmentSystem
{
    [System.Serializable]
    public class Item�arrierHolder : Holder<ItemModel>
    {
         
        public Item�arrierHolder(Item�arrierHolder itholder)
        {
            _objectInHolder = itholder.ObjectInHolder;
            _currentValue = itholder.CurrentValue;
            _maxValue = itholder.MaxValue;
        }
        public Item�arrierHolder(ItemModel item, float currentValue, float maxItemValue)
        {
            _objectInHolder = (ItemModel)item;
            _currentValue = currentValue;
            _maxValue = maxItemValue;
        }
    }
}

       
