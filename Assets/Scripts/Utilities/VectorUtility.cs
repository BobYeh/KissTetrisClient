using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.GameScene
{
    public class VectorUtility : MonoBehaviour
    {

        public static Vector3 AddPosition(Vector3 pointA, Vector3 pointB)
        {
            return new Vector3(pointA.x + pointB.x, pointA.y + pointB.y, pointA.z + pointB.z);
        }

        public static Vector3 CopyPoint(Vector3 point)
        {
            return new Vector3(point.x, point.y, point.z);
        }

        public static Vector3 MinusPosition(Vector3 startPoint, Vector3 destinationPoint)
        {
            return new Vector3(destinationPoint.x - startPoint.x, destinationPoint.y - startPoint.y, destinationPoint.z - startPoint.z);
        }

        public static Vector3 MidPoints(Vector3 pointA, Vector3 pointB)
        {
            return new Vector3((pointA.x + pointB.x) / 2, (pointA.y + pointB.y) / 2, (pointA.z + pointB.z) / 2);
        }
    }
}
