using System;
using Controllers.BaseUnit;
using Controllers.OutPost;
using UnityEngine;
using Views.Outpost;

namespace Controllers
{
    public class InputController: MonoBehaviour
    {
        [SerializeField] private BaseUnitSpawner _spawner;
        
        private void Update()
        {
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
                if(Physics.Raycast(ray, out hit, 100))
                {
                    var outpost = hit.collider.gameObject.GetComponent<OutpostUnitView>();
                    if (outpost)
                    {
                        _spawner.ShowMenu(outpost);
                    }
                }
        
            }
        }
    }
}