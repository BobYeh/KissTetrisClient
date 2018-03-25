using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class GameManager : Singleton<GameManager>
    {
        public void StartGame()
        {
            CubeDataManager.Instance.Initialize();
            CubeMoveController.Instance.Initialize();
            GameTilesViewManager.Instance.Initialize();
            CubeMoveController.Instance.SpawnNewCube();
            CubeDataManager.Instance.UpdateLock = false;
        }

        public void ResetGame()
        {
            CubeDataManager.Instance.Reset();
            CubeMoveController.Instance.Reset();
            GameTilesViewManager.Instance.Reset();
        }

        public void PauseGame()
        {
            CubeDataManager.Instance.UpdateLock = true;
        }

        public void ResumeGame()
        {
            CubeDataManager.Instance.UpdateLock = false;
        }

        public void OnGameOver()
        {
            CubeDataManager.Instance.UpdateLock = true;
            GameSettingMenu.Instance.OnOpen();
        }
    }
}
