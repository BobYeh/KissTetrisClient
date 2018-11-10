using System.Collections;
using System.Collections.Generic;
using KissTetris.GameScene;
using UnityEngine;

namespace KissTetris.Utilities
{
    public class PositionUtils
    {
        public static int TransPositionToIndex(Vector2 pos)
        {
            return (int)Mathf.Round(pos.x/ GameSettings.unitWidth + GameSettings.sizeX * (pos.y/GameSettings.unitHeight));
        }

        public static Vector2 TransIndexToPosition(int index)
        {
            return new Vector2((index % GameSettings.sizeX) * GameSettings.unitWidth, (index / GameSettings.sizeX) * GameSettings.unitHeight);
        }

        public static int TransUnitPositionToIndex(Vector2 pos)
        {
            return (int)(pos.x + GameSettings.sizeX * pos.y);
        }

        public static Vector2 TransIndexToUnitPosition(int index)
        {
            return new Vector2(index % GameSettings.sizeX, index / GameSettings.sizeX);
        }

        public static Vector2 TransUnitPositionToPosition(Vector2 pos)
        {
            return new Vector2(pos.x * GameSettings.unitWidth, pos.y * GameSettings.unitHeight);
        }
    }
}
