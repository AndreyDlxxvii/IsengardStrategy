using Controllers;
using Controllers.BaseUnit;
using Controllers.OutPost;
using Controllers.ResouresesPlace;
using UnityEngine;
using ResurseSystem;
using UnityEngine.AI;
using Views.BaseUnit.UI;

public class GameInit
{
    public GameInit(Controller controller, GameConfig gameConfig, RightUI rightUI, NavMeshSurface navMeshSurface,
        Transform canvas, LeftUI leftUI, LayerMask layerMask,UnitUISpawnerTest unitUISpawnerTest, BuildingsUI buildingsUI,
        GlobalResurseStock globalResStock, TopResUiVew topResUI)
        //Transform canvas, LeftUI leftUI, LayerMask layerMask,UnitUISpawnerTest unitUISpawnerTest)
        {
        var tiles = GetTileList.GetTiles(gameConfig);
        var btnRightUIController = new BtnRightUIController(rightUI, gameConfig);
        var btnLeftUIController = new BtnLeftUIController(leftUI, gameConfig);
        var levelGenerator = new GeneratorLevelController(tiles, gameConfig, rightUI, btnRightUIController, canvas, navMeshSurface);
        var unitController = new UnitController();
        var outPostSpawner = new OutpostSpawner(unitUISpawnerTest);
        var resPlaceSpawner = new ResourcesPlaceSpawner(unitUISpawnerTest);
        
        var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outPostSpawner, btnLeftUIController);
        
        var timeRemaining = new TimeRemainingController();
        var unitSpawner = new BaseUnitSpawner(gameConfig, unitController, outPostSpawner,resPlaceSpawner, gameConfig.BaseUnit);
        var buildingController = new BuildingResursesUIController(buildingsUI);
        var inputController = new InputController(unitSpawner, buildingController);

        
        var globalResController = new MainResursesController(globalResStock, topResUI);
        //var unitSpawner = new BaseUnitSpawner(gameConfig,unitController,outPostSpawner,gameConfig.BaseUnit);
        //var inputController = new InputController(unitSpawner);
        //var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outPostSpawner);
        if (!gameConfig.ChangeVariant)
        {
            new ResourceGenerator(buildController.Buildings, gameConfig, levelGenerator,resPlaceSpawner);
        }
        else
        {
            new ResourceGenerator(buildController.Buildings, gameConfig, levelGenerator, 2,resPlaceSpawner);
        }

        controller.Add(btnRightUIController);
        controller.Add(levelGenerator);
        controller.Add(buildController);
        controller.Add(timeRemaining);
        controller.Add(unitController);
        controller.Add(outPostSpawner);
        controller.Add(resPlaceSpawner);
        controller.Add(unitSpawner);
        controller.Add(inputController);
        controller.Add(buildingController);
        controller.Add(globalResController);
        controller.Add(btnLeftUIController);
        }
}