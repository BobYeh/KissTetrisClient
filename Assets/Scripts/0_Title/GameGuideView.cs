using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGuideView : MonoBehaviour
{
    public void OnClickedCloseButton()
    {
        gameObject.SetActive(false);
    }
}
