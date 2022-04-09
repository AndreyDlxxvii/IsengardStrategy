using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "OutpostParametersData", menuName = "OutPost/OutpostParametersData", order = 0)]
    public class OutpostParametersData: ScriptableObject
    {
        
        #region Fields

        [SerializeField] private int _maxCountOfNPC;
        private int _currentCountOfNPC = 0;

        #endregion


        #region Methods

        public void AddMaxCountOfNPC(int number)
        {
            _maxCountOfNPC += number;
        }

        public void AddCurrentCountOfNPC(int number)
        {
            _currentCountOfNPC += number;
        }

        public int GetMaxCountOfNPC()
        {
            return _maxCountOfNPC;
        }

        public int GetCurrentCountOfNPC()
        {
            return _currentCountOfNPC;
        }

        #endregion

    }
}