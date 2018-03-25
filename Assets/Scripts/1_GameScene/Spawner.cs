using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        Transform cubeParent;

        Vector3 spawnStartPoint = new Vector3(3, 13, 0);

        public GameObject[] SpawnGroups(int num)
        {
            List<GameObject> spawnCubes = new List<GameObject>();

            for(int i=0; i<num; i++)
            {
                TileType tileType = RandomUtils.RamdomSelectGameTitle(GetAllItemsPercentage(i == 0));
                var prefab = GameTileViewFactory.Instance.GenerateGameTileView(tileType);
                GameObject cube = Instantiate(prefab, cubeParent);
                cube.transform.position = PositionUtils.TransUnitPositionToPosition( new Vector2(spawnStartPoint.x, spawnStartPoint.y + i));
                GameTileViewFactory.Instance.AddGameTileViewComponent(tileType, cube);
                var tileView = cube.GetComponent<GameTileView>();
                tileView.Initialize(tileType);
                tileView.InitializeIndex(PositionUtils.TransPositionToIndex(tileView.transform.position));
                spawnCubes.Add(cube);
                GameTilesViewManager.Instance.AddView(cube.GetComponent<GameTileView>());
            }

            return spawnCubes.ToArray();
        }

        //DummyData
        public Dictionary<TileType, float> GetRareItemsPercentage()
        {
            Dictionary<TileType, float> rareItemPercentage = new Dictionary<TileType, float>();
            rareItemPercentage.Add(TileType.Underwear, 10);
            rareItemPercentage.Add(TileType.SlapLeft, 40);
            rareItemPercentage.Add(TileType.SlapRight, 40);
            return rareItemPercentage;
        }

        public Dictionary<TileType, float> GetAllItemsPercentage(bool isIncludeRareItems)
        {
            List<TileType> facesType = new List<TileType>() { TileType.BoyForward, TileType.BoyLeft, TileType.BoyRight, TileType.GirlForward, TileType.GirlLeft, TileType.GirlRight };
            Dictionary<TileType, float> allItemsPercentage = new Dictionary<TileType, float>();
            float totalFacesPercentage = 100;

            if (isIncludeRareItems)
            {
                var rareItemsPercentage = GetRareItemsPercentage();

                foreach(var tileType in rareItemsPercentage.Keys)
                {
                    allItemsPercentage.Add(tileType, rareItemsPercentage[tileType]);
                    totalFacesPercentage -= rareItemsPercentage[tileType];
                }
            }

            float faceAveragePrecentage = totalFacesPercentage / (float)facesType.Count;

            foreach (var tileType in facesType)
            {
                allItemsPercentage.Add(tileType, faceAveragePrecentage);
            }

            return allItemsPercentage;
        }
    }
}
