using System;
using ResurseSystem;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{ 
    
    public class MainResursesController : IOnController,IOnStart,IDisposable

    {
        
        private List<ResurseStock> AllResursesStockPlayer; 
        [SerializeField]
        private TopResUiVew _topUI; 
        [SerializeField]
        private GlobalResurseStock _globalResursesStock;
        

        public MainResursesController(GlobalResurseStock globalResurseStock, TopResUiVew topResUIView)
        {
            _globalResursesStock = globalResurseStock;
            _topUI = topResUIView;
        }

        public void Dispose()
        {
            
            _globalResursesStock.GlobalResChange -= UpdateTopUIValues;
        }

        public void OnStart()
        {
           _globalResursesStock.GlobalResChange+=UpdateTopUIValues;
        }

        public void UpdateTopUIValues()
        {
            foreach (ResurseHolder holder in _globalResursesStock.GetGlobalResurseStock().ResursesInStock)
            {
                _topUI.UpdateResursesCount(holder.ResurseInHolder.ResurseType, holder.CurrentResurseCount);
            }
        }
    }
}
