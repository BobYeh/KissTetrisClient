using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.GameScene
{
    public class CameraSizeChanger : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            float defaultAspectRatio = 16.0f / 9.0f;
            float currentAspectRatio = (float)Screen.height / Screen.width;

            if (currentAspectRatio > defaultAspectRatio)
            {
                var cameraSize = GetComponent<UnityEngine.Camera>().orthographicSize;
                GetComponent<UnityEngine.Camera>().orthographicSize = cameraSize * (currentAspectRatio / defaultAspectRatio);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
