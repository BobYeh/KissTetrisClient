using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class GameTileViewFactory : Singleton<GameTileViewFactory>
    {
        public GameObject GenerateGameTileView(TileType type)
        {
            string prefabPath = "";

            switch (type)
            {
                case TileType.BoyForward:
                case TileType.BoyLeft:
                case TileType.BoyRight:
                    prefabPath = PrefabPath.Man_Face;
                    break;
                case TileType.GirlForward:
                case TileType.GirlLeft:
                case TileType.GirlRight:
                    prefabPath = PrefabPath.Woman_Face;
                    break;
                case TileType.Underwear:
                    prefabPath = PrefabPath.Underwear;
                    break;
                case TileType.SlapLeft:
                    prefabPath = PrefabPath.SlapLeft;
                    break;
                case TileType.SlapRight:
                    prefabPath = PrefabPath.SlapRight;
                    break;
            }

            var gameObject = Resources.Load<GameObject>(prefabPath);
            return gameObject;
        }

        public void AddGameTileViewComponent(TileType type, GameObject gameObject)
        {
            switch (type)
            {
                case TileType.BoyForward:
                case TileType.BoyLeft:
                case TileType.BoyRight:
                    gameObject.AddComponent<GameFaceView>();
                    break;
                case TileType.GirlForward:
                case TileType.GirlLeft:
                case TileType.GirlRight:
                    gameObject.AddComponent<GameFaceView>();
                    break;
                case TileType.Underwear:
                case TileType.SlapLeft:
                case TileType.SlapRight:
                    gameObject.AddComponent<GameTileView>();
                    break;
            }
        }
    }
}
