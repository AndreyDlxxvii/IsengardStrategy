using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{ 
    [System.Serializable]
    public class GoldHolder  
    {
        public ResurseCraft GoldObject => _goldObject;
        public float CurrentValue => _currentValue;

        [SerializeField] private ResurseCraft _goldObject;
        [SerializeField] private float _currentValue;

        public void AddGold(GoldCost cost)
        {
            if (cost.GoldObject==GoldObject)
            {
                _currentValue += cost.Cost;
            }
            else
            {
                Debug.Log("���-�� ����� �� ��� ���� � ����, ���� � ��������� ������!");
            }
        }
        public void GetGold(GoldCost cost)
        {
            if (cost.GoldObject == GoldObject)
            {
                if (cost.Cost <= CurrentValue)
                {
                    _currentValue -= cost.Cost;
                }
                else Debug.Log($"����� ������ ������!�� �������: {cost.Cost-CurrentValue}");
            }
            else
            {
                Debug.Log("���-�� ����� �� ��� ���� � ����, ���� � ��������� ������!");
            }
        }
        public void SetCurrentGold(float value)
        {
            _currentValue = value;
        }

    }
}
