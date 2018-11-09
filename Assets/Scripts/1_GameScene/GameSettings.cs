using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.GameScene
{
    public class GameSettings
    {
        public static int numberOfGroupItems = 3;

        public static int sizeX = 6;
        public static int sizeY = 12;

        public static float unitWidth = 1.23f;
        public static float unitHeight = 1f;

        public static float moveThreshold = 70;
        public static float dropThreshold = 25;

        public static float clickMovementThreshold = 30;
        public static float clickPressTimeThreshold = 0.3f;

        public static int baseScoreForEachFaces = 5;

        public static Vector3 spawnStartPoint = new Vector3(3, 11, 0);
    }
}
