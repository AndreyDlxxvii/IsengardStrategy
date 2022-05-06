using Code.View.ResourcesPlace;
using Controllers.BaseUnit;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using ResurseSystem;

using Views.Outpost;

namespace Controllers
{
    public class InputController : IOnController, IOnUpdate
    {
        private BaseUnitSpawner _spawner;
        private BuildingResursesUIController _rescontoller;   

        public InputController(BaseUnitSpawner baseUnitSpawner, BuildingResursesUIController rescontoller)
        {
            _spawner = baseUnitSpawner;
            _rescontoller = rescontoller;            


        }

        public void OnUpdate(float deltaTime)
        {

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100))

                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    var outpost = hit.collider.gameObject.GetComponent<ISpawnerLogicView>();
                    var currBuild = hit.collider.gameObject.GetComponentInParent<BuildingView>();
                    var currMine = hit.collider.gameObject.GetComponentInParent<Mineral>();

                    if (_spawner.SpawnIsActiveIndex != -1)
                    {
                         _spawner.UnShowMenu(outpost);
                    }
                    if (currBuild)
                    {                        
                        _rescontoller.SetActiveUI(currBuild); 
                    }
                    else
                    {
                        if (currMine)
                        {
                            _rescontoller.SetActiveUI(currMine);
                                                       
                        }
                        else
                        {
                            _rescontoller.DisableMenu();
                        }
                    }
                    
                    switch (outpost)
                    {
                        case ResourcesPlaceView resourcesPlaceView:
                            _spawner.ShowMenu(resourcesPlaceView);
                            break;
                        case OutpostUnitView outpostUnitView:
                            _spawner.ShowMenu(outpostUnitView);
                            break;
                    }
                }

            }

        }

    }
}