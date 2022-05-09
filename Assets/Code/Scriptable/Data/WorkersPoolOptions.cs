using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WorkersPoolOptions", menuName = "Workers/WorkersPoolOptions", order = 0)]
    public class WorkersPoolOptions : ScriptableObject
    {
        [SerializeField] private int _countOfStartWorkers;


        public int countOfStartWorkers => _countOfStartWorkers;
    }
}