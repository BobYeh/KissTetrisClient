using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.GameScene
{
    public class GameManager : Singleton<GameManager>
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void StartGame()
        {
            ScoreManager.Instance.Initialize();
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
            GameTileGroupManager.Instance.Reset();
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
