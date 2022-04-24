using ResurseSystem;
using System;
using UnityEngine;
using UnityEngine.UI;


 namespace ResurseSystem
{ 
    public class ResurseStockView : MonoBehaviour,IDisposable
    {
        [SerializeField]
        private ResurseStock BaseResurseStock;        
        [SerializeField]
        private Sprite iconStock;
        [SerializeField]
        private GlobalResurseStock _globalResurseStock;
        private ResurseStock thisResurseStock;
        public Action<ResurseStock> ResStockBorn;
        public Action<ResurseStock> ResStockDie;                

        private void Awake()
        {
            
            ResStockBorn += _globalResurseStock.AddResurseStock;
            ResStockDie += _globalResurseStock.DeleteStock;
            thisResurseStock = Instantiate<ResurseStock>(BaseResurseStock);
            thisResurseStock.SetStockName(gameObject.name);
            thisResurseStock.SetIconStock(iconStock);
            ResStockBorn?.Invoke(thisResurseStock);
        }
        public ResurseStock GetStock()
        {
            return thisResurseStock;
        }
        public void Dispose()
        {
            ResStockBorn -= _globalResurseStock.AddResurseStock;
            ResStockDie?.Invoke(thisResurseStock);
            ResStockDie -= _globalResurseStock.DeleteStock;
    }
    }
}


