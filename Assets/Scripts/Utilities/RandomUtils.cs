using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class RandomUtils
    {
        public static TileType RamdomSelectGameTitle(Dictionary<TileType, float> data)
        {
            int rand = Random.Range(0, 10000);
            TileType currentTileType;
            float currentPrecentage = 0;

            var tileTypes = data.Keys.ToList();
            var percentages = data.Values.ToList();

            for (int i = 0; i < tileTypes.Count; i++)
            {
                currentTileType = tileTypes[i];
                currentPrecentage += percentages[i] * 100;
                if (currentPrecentage > rand)
                    return currentTileType;
            }

            return TileType.BoyForward;
        }
    }
}
