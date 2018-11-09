using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.Title
{
    public class GameGuideView : MonoBehaviour
    {
        public void OnClickedCloseButton()
        {
            gameObject.SetActive(false);
        }
    }
}
