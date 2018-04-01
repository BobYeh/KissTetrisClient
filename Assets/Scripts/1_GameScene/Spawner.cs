using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class Spawner : Singleton<Spawner>
    {
        [SerializeField]
        Transform cubeParent;

        Vector3 spawnStartPoint = new Vector3(3, 13, 0);

        public GameTileView[] SpawnGroups(int num)
        {
            List<GameTileView> spawnCubes = new List<GameTileView>();

            for(int i=0; i<num; i++)
            {
                TileType tileType = RandomUtils.RamdomSelectGameTitle(GetAllItemsPercentage(i == 0));
                var prefab = GameTileViewFactory.Instance.GenerateGameTileView(tileType);
                GameObject cube = Instantiate(prefab, cubeParent);
                GameTileViewFactory.Instance.AddGameTileViewComponent(tileType, cube);
                var tileView = cube.GetComponent<GameTileView>();
                tileView.Initialize(tileType);
                spawnCubes.Add(tileView);
                GameTilesViewManager.Instance.AddView(tileView);
            }

            return spawnCubes.ToArray();
        }

        //DummyData
        public Dictionary<TileType, float> GetRareItemsPercentage()
        {
            Dictionary<TileType, float> rareItemPercentage = new Dictionary<TileType, float>();
            rareItemPercentage.Add(TileType.Underwear, 15);
            rareItemPercentage.Add(TileType.SlapLeft, 15);
            rareItemPercentage.Add(TileType.SlapRight, 15);
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
