using UnityEngine.UI;
using UnityEngine;
using System;

namespace EquipSystem
{ 
    public class ESLeftUIPresenter : MonoBehaviour
    {
        [SerializeField] 
        private EquipSystemBehaviour character;

        private ChangeEquipmentButtonView[] _EquipButtonViews;

        [SerializeField]
        private TestAnimationControllerBehavior _characterAnimatorCB;

        

        private void Awake()
        {            
            _EquipButtonViews = gameObject.GetComponentsInChildren<ChangeEquipmentButtonView>();
            foreach (ChangeEquipmentButtonView buttonView in _EquipButtonViews)
            {
                buttonView._equipAction += EquipmentChange;
            }
        }

        public void EquipmentChange(EquipableItemBase item)
        {
            character.GetEquipped(item);
            _characterAnimatorCB.CheckStateCharacter();
            _characterAnimatorCB.CheckWorkerEquipment();
        }
        private void OnDestroy()
        {
            foreach (ChangeEquipmentButtonView buttonView in _EquipButtonViews)
            {
                buttonView._equipAction -= EquipmentChange;
            }
        }
        public void CheckWorker()
        {
            
        }

    }
}
