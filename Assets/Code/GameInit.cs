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
        var levelGenerator = new GenerateLevelController(tiles, gameConfig, rightUI, btnConroller, canvas, navMeshSurface);
        var buildController = new BuildGenerator(gameConfig, leftUI, layerMask, outpostSpawner);
        
        controller.Add(btnConroller);
        controller.Add(levelGenerator);
        controller.Add(buildController);
    }
}