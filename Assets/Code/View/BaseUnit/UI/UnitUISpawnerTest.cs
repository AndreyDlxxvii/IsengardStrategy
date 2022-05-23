using System;
using Controllers.OutPost;
using Data;
using Interfaces;
using Models.BaseUnit;
using UnityEngine;
using UnityEngine.UI;

namespace Views.BaseUnit.UI
{
    public class UnitUISpawnerTest : MonoBehaviour
    {
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _lessButton;
        public Action addUnit;
        public Action lessUnit;

        private void Start()
        {
            _addButton.onClick.AddListener(buttonAddPressed);
            _lessButton.onClick.AddListener(buttonLessPressed);
        }

        private void buttonAddPressed()
        {
            addUnit.Invoke();
        }
        
        private void buttonLessPressed()
        {
            lessUnit.Invoke();
        }

        private void OnDestroy()
        {
            _addButton.onClick.RemoveAllListeners();
            _lessButton.onClick.RemoveAllListeners();
        }
    }
}