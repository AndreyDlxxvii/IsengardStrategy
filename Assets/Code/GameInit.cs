using Controllers.OutPost;
using UnityEngine;
using UnityEngine.AI;

public class GameInit
{
    public GameInit(Controller controller, GameConfig gameConfig, RightUI rightUI, NavMeshSurface navMeshSurface,
        Transform canvas, LeftUI leftUI, LayerMask layerMask, OutpostSpawner outpostSpawner)
    {

        var tiles = GetTileList.GetTiles(gameConfig);
            
        var btnConroller = new BtnUIController(rightUI, gameConfig);
        var levelGenerator = new GeneratorLevelController(tiles, gameConfig, rightUI, btnConroller, canvas, navMeshSurface);
        var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outpostSpawner);
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
    }
}