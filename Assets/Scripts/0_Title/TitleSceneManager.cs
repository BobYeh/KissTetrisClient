using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameGuideView;
	
    public void OnClickedGameGuideButton()
    {
        gameGuideView.SetActive(true);
    }


    public void OnClickedGameStartButton()
    {
        SceneManager.LoadSceneAsync(SceneName.GameScene);
    }
}
