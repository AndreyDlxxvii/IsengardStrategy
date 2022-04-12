using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace EquipSystem
{ 
    public class ChangeEquipmentButtonView : MonoBehaviour
    {
        [SerializeField] private EquipableItemBase item;
        private Image _iconItem;        
        private Button _itemButton;
        private TextMeshProUGUI _nameItem;
        public Action<EquipableItemBase> _equipAction;

        private void Awake()
        {
            _iconItem = gameObject.GetComponent<Image>();            
            _itemButton = gameObject.GetComponent<Button>();
            _nameItem = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if(item!=null)
            {
                _iconItem.sprite=item.Icon;
                _nameItem.text = item.NameOfItem;
            }
            else
            {
                throw new UnityException("Not emplemented Item in Button");
            }
        }
        public void GetEquiptedItemButtonOnClick()
        {
            _equipAction.Invoke(item);
        }

    }
}
