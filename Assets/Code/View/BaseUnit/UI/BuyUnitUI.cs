using System;
using UnityEngine;
using UnityEngine.UI;


namespace Views.BaseUnit.UI
{
    public sealed class BuyUnitUI: MonoBehaviour
    {
        [SerializeField] private Button _buy;
        public Action buyUnit;
        private void Start()
        {
            _buy.onClick.AddListener(BuyNewUnit);
        }

        private void BuyNewUnit()
        {
            buyUnit.Invoke();
        }
    }
}