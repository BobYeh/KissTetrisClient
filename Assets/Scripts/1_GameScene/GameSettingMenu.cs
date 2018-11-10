using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.GameScene
{
    public class GameSettingMenu : Singleton<GameSettingMenu>
    {
        [SerializeField]
        GameObject menu;

        [SerializeField]
        GameObject startGameButton;

        [SerializeField]
        GameObject reStartGameButton;

        [SerializeField]
        GameObject resumetGameButton;

        [SerializeField]
        GameObject gameSpeedMenu;

        public void OnClickedStartGameButton()
        {
            startGameButton.SetActive(false);
            gameSpeedMenu.gameObject.SetActive(true);
        }

        public void OnClickedRestartGameButton()
        {
            GameManager.Instance.ResetGame();
            reStartGameButton.SetActive(false);
            resumetGameButton.SetActive(false);
            gameSpeedMenu.gameObject.SetActive(true);
        }

        public void OnSelectedGameSpeed()
        {
            GameManager.Instance.StartGame();
            gameSpeedMenu.gameObject.SetActive(false);
            reStartGameButton.SetActive(true);
            resumetGameButton.SetActive(true);
            OnClose();
        }

        public void OnClickedResumeGameButton()
        {
            GameManager.Instance.ResumeGame();
            OnClose();
        }

        public void OnClickedOpenMenuButton()
        {
            GameManager.Instance.PauseGame();
            OnOpen();
        }

        public void OnOpen()
        {
            menu.SetActive(true);
        }

        public void OnClose()
        {
            menu.SetActive(false);
        }
    }
}
