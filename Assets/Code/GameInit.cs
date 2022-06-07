using Controllers;
using Controllers.BaseUnit;
using Controllers.OutPost;
using UnityEngine;
using ResurseSystem;
using BuildingSystem;
using UnityEngine.AI;
using Views.BaseUnit.UI;

public class GameInit
{
    public GameInit(Controller controller, GameConfig gameConfig, RightUI rightUI, NavMeshSurface navMeshSurface,
        Transform canvas, LeftUI leftUI, LayerMask layerMask,UnitUISpawnerTest unitUISpawnerTest, BuildingsUI buildingsUI,GlobalResurseStock globalResStock, TopResUiVew topResUI,GlobalBuildingsModels globalBuildingmodel)
    {

        var tiles = GetTileList.GetTiles(gameConfig);
            
        var btnConroller = new BtnUIController(rightUI, gameConfig);
        var levelGenerator = new GeneratorLevelController(tiles, gameConfig, rightUI, btnConroller, canvas, navMeshSurface);
        var unitController = new UnitController();
        var outPostSpawner = new OutpostSpawner(unitUISpawnerTest);
        var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outPostSpawner);
        var timeRemaining = new TimeRemainingController();
        var unitSpawner = new BaseUnitSpawner(gameConfig, unitController, outPostSpawner, gameConfig.BaseUnit);
        var buildingController = new BuildingResursesUIController(buildingsUI, globalBuildingmodel);
        var inputController = new InputController(unitSpawner, buildingController);
        
        var globalResController = new MainResursesController(globalResStock, topResUI);
        //var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outPostSpawner);
        if (!gameConfig.ChangeVariant)
        {
            new ResourceGenerator(buildController.Buildings, gameConfig, levelGenerator);
        }
        else
        {
            new ResourceGenerator(buildController.Buildings, gameConfig, levelGenerator, 2);
        }

        controller.Add(btnConroller);
        controller.Add(levelGenerator);
        controller.Add(buildController);
        controller.Add(timeRemaining);
        controller.Add(unitController);
        controller.Add(outPostSpawner);
        controller.Add(unitSpawner);
        controller.Add(inputController);
        controller.Add(buildingController);
        controller.Add(globalResController);

    }
}