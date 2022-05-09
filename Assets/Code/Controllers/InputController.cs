using Code.View;
using Code.View.ResourcesPlace;
using Controllers.BaseUnit;
using Controllers.ResouresesPlace;
using Controllers.Worker;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using ResurseSystem;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers
{
    public class InputController : IOnController, IOnUpdate
    {
        private readonly UnitUISpawnerTest _uiSpawnerTest;
        private BuildingResursesUIController _rescontoller;
        private readonly BuyUnitUI _buyUnitUI;
        private ResourcesPlaceView _resourcesPlaceViewCopy;

        public InputController(UnitUISpawnerTest uiSpawnerTest,BuildingResursesUIController rescontoller,
            BuyUnitUI buyUnitUI)
        {
            _uiSpawnerTest = uiSpawnerTest;
            _rescontoller = rescontoller;
            _buyUnitUI = buyUnitUI;
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
                    var workersPlaceView = hit.collider.gameObject.GetComponentInParent<WorkersPlaceView>();
                    var currBuild = hit.collider.gameObject.GetComponentInParent<BuildingView>();
                    var currMine = hit.collider.gameObject.GetComponentInParent<Mineral>();
                    
                    if (workersPlaceView)
                    {
                        _buyUnitUI.gameObject.SetActive(true);
                    }
                    else
                    {
                        _buyUnitUI.gameObject.SetActive(false);
                    }
                    if (currBuild)
                    {                        
                        _rescontoller.SetActiveUI(currBuild); 
                    }
                    else
                    {
                        if (currMine)
                        {
                            _uiSpawnerTest.gameObject.SetActive(true);
                            _rescontoller.SetActiveUI(currMine);
                                                       
                        }
                        else
                        {
                            _uiSpawnerTest.gameObject.SetActive(false);
                            _rescontoller.DisableMenu();
                        }
                    }
                    
                    /*switch (outpost)
                    {
                        case ResourcesPlaceView resourcesPlaceView:
                            _resourcesPlaceSpawner.ShowMenu(resourcesPlaceView);
                            _resourcesPlaceViewCopy = resourcesPlaceView;
                            break;
                    }*/
                }

            }

        }

    }
}