using EquipmentSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Global Resurse Stock", menuName = "Resurse System/Global Resurse Stock", order = 1)]
    public class GlobalResurseStock :ScriptableObject
    {
        public ResurseStock GlobalResStock=> _globalResursesStock;
        public GoldHolder AllGoldHolder => _goldHolder;
        public ItemStock AllItemPlayer => _globalItemStock;

        private List<ResurseStock> AllResursesStockPlayer;
        [SerializeField]
        private ResurseStock _globalResursesStock;
        [SerializeField] 
        private GoldHolder _goldHolder;
        [SerializeField]
        private ItemStock _globalItemStock;
        public Action<ResurseStock> GlobalResChange;
        public Action<GoldHolder> GlobalGoldChange;
        public Action<ItemStock> GLobalItemStockChange;

        
        public GlobalResurseStock( ResurseStock resurseStock,GoldHolder holder,ItemStock itemStock)
        {
            _globalResursesStock = new ResurseStock (resurseStock);
            _goldHolder = holder;
            _globalItemStock = new ItemStock (itemStock);
            AllResursesStockPlayer = new List<ResurseStock>();           
            ResetGlobalRes();
        }
        public GlobalResurseStock(GlobalResurseStock globalResurseStock)
        {
            _globalResursesStock = new ResurseStock (globalResurseStock.GlobalResStock);
            AllResursesStockPlayer = new List<ResurseStock>(globalResurseStock.AllResursesStockPlayer);
            _goldHolder = globalResurseStock.AllGoldHolder;
            _globalItemStock = new ItemStock(globalResurseStock._globalItemStock);
            ResetGlobalRes();

        }
        /// <summary>
        /// ����� ��������� �������� �� ����������� ����� ��� ������������ �������
        /// </summary>
        /// <param name="product">������ ��������� �������� ��� ������������</param>
        public void GetResurseForProduceFromGlobalStock(ResurseProduct product)
        {
            product.PaidCostForProduceProduct(_globalResursesStock);
            GlobalResChange?.Invoke(GlobalResStock);
        }
        /// <summary>
        /// ����� ��������� �������� �� ����������� ����� ��� ������������ ��������
        /// </summary>
        /// <param name="product">������� ��������� ��������</param>
        public void GetResurseForProduceFromGlobalStock(ItemProduct product)
        {
            product.PaidCostForProduceProduct(_globalResursesStock);
            GlobalResChange?.Invoke(GlobalResStock);
        }
        public void GetResurseForProduceFromGlobalStock(ResurseCost cost)
        {
            cost.GetNeededResurse(_globalResursesStock);
            GlobalResChange?.Invoke(GlobalResStock);
        }
        /// <summary>
        /// ����� ��������� ������ �� ������� �� ����������� �����
        /// </summary>
        /// <param name="cost">��������� ��������� ������</param>
        /// <returns></returns>
        public bool PriceGoldFromGlobalStock(GoldCost cost)
        {
            bool result = false;
            if (cost.Cost <= AllGoldHolder.CurrentValue)
            {
                _goldHolder.GetGold(cost);
                GlobalGoldChange?.Invoke(_goldHolder);
                result = true;
            }
            else Debug.Log("�� ������� ������!");
            return result;
        }
        /// <summary>
        /// ����� ���������� ������ � ���������� ����
        /// </summary>
        /// <param name="cost">����� ������ ��� ����������</param>
        public void AddGoldToGLobalStock(GoldCost cost)
        {
            _goldHolder.AddGold(cost);
        }
        /// <summary>
        /// ����� ���������� �������� � ���������� ����
        /// </summary>
        /// <param name="holder">��������� ������� ����������</param>
        public void AddResurseToGlobalResurseStock(ResurseHolder holder)
        {
            _globalResursesStock.AddInStock(holder);
            GlobalResChange?.Invoke(GlobalResStock);
        }
        /// <summary>
        /// Add item in global item stock
        /// </summary>
        /// <param name="holder"></param>
        public void AddItemToGlobalResurseStock(Item�arrierHolder holder)
        {
            _globalItemStock.AddInStock(holder);
        }
        /// <summary>
        /// Return pay for product in global stock
        /// </summary>
        /// <param name="cost"></param>
        public void ReturnPayForGlobalResurseStock(ResurseCost cost)
        {
            foreach(ResurseHolder holder in cost.CostInResurse)
            {
                AddResurseToGlobalResurseStock(holder);
            }
        }
        /// <summary>
        /// ����� ������� ����� � ���������� ������ (����������� ������������ ������� �������� � ���������� �����)
        /// </summary>
        /// <param name="stock">������������� ����� ����</param>
        public void ChangeMaxValueOfGlobalResurseStock(ResurseStock stock)
        {
            _globalResursesStock.CompileStocks(stock);
            GlobalResChange?.Invoke(_globalResursesStock);
        }
        /// <summary>
        /// Compile item stock with global item stock
        /// </summary>
        /// <param name="stock"></param>
        public void ChangeMaxValueOfGlobalResurseStock(ItemStock stock)
        {
            _globalItemStock.CompileStocks(stock);
            GLobalItemStockChange?.Invoke(_globalItemStock);
        }
        /// <summary>
        /// ����� ���������� ������� � ���� ��� �������� ����� � ��������� ���� �������.
        /// </summary>
        /// <param name="obj">������ ��� ���������� � ����</param>
        /// <param name="value">���������� �������</param>
        public void AddObjectInStock(ScriptableObject obj,float value)
        {
            if (obj is ItemModel)
            {
                _globalItemStock.AddInStock((ItemModel)obj, value);
                GLobalItemStockChange?.Invoke(_globalItemStock);
            }
            if (obj is ResurseCraft)
            {
                _globalResursesStock.AddInStock((ResurseCraft)obj, value);
                GlobalResChange?.Invoke(_globalResursesStock);
            }
        }
        public void AddProductInStock (ResurseProduct product)
        {
            _globalResursesStock.AddInStock(product.ObjectProduct, product.ProduceValue);
        }
        public void AddProductInStock(ItemProduct product)
        {
            _globalItemStock.AddInStock(product.ObjectProduct, product.ProduceValue);
        }
        
        /// <summary>
        /// ����� ���������� ����������� �����
        /// </summary>
        public void ResetGlobalRes()
        {
            _globalResursesStock.ResetStockHoldersValue();
            _goldHolder.SetCurrentGold(0);
            _globalItemStock.ResetStockHoldersValue();
            GlobalResChange?.Invoke(_globalResursesStock);
            GlobalGoldChange?.Invoke(_goldHolder);
        }
        
    }
}
