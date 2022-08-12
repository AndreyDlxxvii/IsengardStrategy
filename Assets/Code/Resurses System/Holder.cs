using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    [System.Serializable]
    public abstract class Holder<T> : IHolder<T> where T : ScriptableObject 
    {
        public T ObjectInHolder => _objectInHolder;
        public float CurrentValue => _currentValue;
        public float MaxValue => _maxValue;

        [SerializeField] protected T _objectInHolder;
        [SerializeField] protected float _currentValue;
        [SerializeField] protected float _maxValue;                
            

       
        public Holder<T> AddInHolder(Holder<T> holder)
        {
            if (ObjectInHolder == holder.ObjectInHolder)
            {
                if (holder.CurrentValue <= MaxValue - CurrentValue)
                {
                    _currentValue += holder.CurrentValue;
                    holder.SetCurrentValueHolder(0);
                }
                else
                {
                    float tempValue = CurrentValue + holder.CurrentValue - MaxValue;
                    holder.SetCurrentValueHolder(tempValue);
                    _currentValue = _maxValue;
                    Debug.Log($"� ��������� �� ������� ����� ��� {tempValue} ������");
                }
            }
            else
            {
                Debug.Log("�� ���������� ������� ��� ���������!");
            }
            return holder;
        }
        public void AddInHolder(T obj,float value)
        {
            if (obj==ObjectInHolder)
            {
                _currentValue += value;
            }
            else
            {
                Debug.Log("�� ��������� �������!");
            }    
        }
        public Holder<T> AddInHolder(Holder<T> holder,float value)
        {
            if (value>holder.CurrentValue)
            {
                Debug.Log("� ��������� ������� ���!");
                return holder;
            }
            if (ObjectInHolder == holder.ObjectInHolder)
            {
                if (value <= MaxValue - CurrentValue)
                {
                    _currentValue += value;
                    holder.SetCurrentValueHolder(holder.CurrentValue-value);
                }
                else
                {
                    float tempValue = CurrentValue + value - MaxValue;
                    holder.SetCurrentValueHolder(tempValue);
                    _currentValue = _maxValue;
                    Debug.Log($"� ��������� �� ������� ����� ��� {tempValue} ������");
                }
            }
            else
            {
                Debug.Log("�� ���������� ������� ��� ���������!");
            }
            return holder;
        }

        public Holder<T> GetFromHolder(Holder<T> holder)
        {
            float neededValue = holder.MaxValue - holder.CurrentValue;
            if (ObjectInHolder == holder.ObjectInHolder)
            {
                if (neededValue <= CurrentValue)
                {
                    holder.SetCurrentValueHolder(holder.MaxValue);
                    _currentValue = _currentValue- neededValue;
                }
                else
                {
                    float tempValue = holder.MaxValue- CurrentValue - holder.CurrentValue;
                    holder.SetCurrentValueHolder(holder.CurrentValue+CurrentValue);
                    _currentValue = 0;
                    Debug.Log($"� ��������� �� ������� {tempValue} ������");
                }
            }
            else
            {
                Debug.Log("�� ���������� ������� ��� ���������!");
            }
            return holder;
        }
        public Holder<T> GetFromHolder(Holder<T> holder, float value)
        {
            if (ObjectInHolder == holder.ObjectInHolder)
            {
                if (holder.MaxValue - holder.CurrentValue >= value && CurrentValue <= value)
                {
                    holder.SetCurrentValueHolder(holder.CurrentValue + value);
                    _currentValue -= value;
                }
                else
                {
                    if (value > CurrentValue)
                    {
                        float tempValue = value - CurrentValue;
                        holder.SetCurrentValueHolder(value - tempValue);
                        _currentValue = 0;
                        Debug.Log($"� ��������� �� ������� {tempValue} ������");
                    }
                    if (holder.MaxValue - holder.CurrentValue >= value)
                    {
                        float tempValue = value - CurrentValue;
                        holder.SetCurrentValueHolder(value - tempValue);
                        _currentValue = 0;
                        Debug.Log($"� ��������� �� ������� {tempValue} ������");
                    }
                }
            }
            else
            {
                Debug.Log("�� ���������� ������� ��� ���������!");
            }
            return holder;
        }

        public void ChangeObjectInHolder(T obj)
        {
            _objectInHolder=obj;
        }        

        public void SetCurrentValueHolder(float value)
        {
            if(value<=MaxValue)
            { 
            _currentValue=value;
            }
            else Debug.Log("������ ������ ��� ����� � ��������� ���������� ����������, ������ �������������!");
        }

        public void SetMaxValueHolder(float value)
        {
            if (CurrentValue<value)
            {
                Debug.Log("������ ������ ��� ����� � ��������� ������������ ����������, ������ �����������!");
                return;
            }
            _maxValue=value;
        }        
    }
}
