using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class CubeMoveController : Singleton<CubeMoveController>
    {
       GameObject[] currentControlCubes;

        float updateTimeBase = 0.7f;
        float speedFactor = 1;

        float updateTime = 0.2f;
        float maxUpdateTime = 2.0f;
        float currentUpdateTime = 0.0f;

        float changeDirectWaitTime = 0.3f;
        float changeDirectBufferTime = 0;

        int sizeX = 6;
        int sizeY = 12;

        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            if (CubeDataManager.Instance.UpdateLock)
                return;

            currentUpdateTime += Time.deltaTime;

            UpdateCubePositionWithTime();

            if (currentUpdateTime > updateTime)
            {
                if (UpdateMoveCubeWithUnitTime())
                {
                    currentUpdateTime = 0;
                    changeDirectBufferTime = 0;
                }
                else if(changeDirectBufferTime <= changeDirectWaitTime && currentUpdateTime < maxUpdateTime)
                {
                    UpdateChangeBufferTime();
                }
                else if (CurrentCubeSetted())
                {
                    StartCoroutine(CubeDataManager.Instance.UpdateWithTileSetted());
                    currentUpdateTime = 0;
                    changeDirectBufferTime = 0;
                }
                else
                {
                    GameManager.Instance.OnGameOver();
                }
            }

            CheckForInput();
        }

        void UpdateChangeBufferTime()
        {
            changeDirectBufferTime += Time.deltaTime;
        }

        public void Initialize()
        {
            currentUpdateTime = 0;
            updateTime = updateTimeBase / speedFactor;
        }

        public void InitializeDropSpeed()
        {
            updateTime = updateTimeBase / (speedFactor + ((int)ScoreManager.Instance.CurrentScore / GameDefine.UPDATE_SPEED_SCORE));
        }

        public void Reset()
        {
            currentControlCubes = null;
            currentUpdateTime = 0;
        }

        public void SetSpeedFactor(int speed)
        {
            speedFactor = speed;
        }

        bool CheckCubeMoveable(Vector3 moveDirect)
        {
            for (int i = 0; i < currentControlCubes.Length; i++)
            {
                var newPos = VectorUtility.AddPosition(PositionUtils.TransIndexToUnitPosition(currentControlCubes[i].GetComponent<GameTileView>().Index), moveDirect);

                if (newPos.x < 0 || newPos.x >= sizeX || newPos.y < 0)
                    return false;

                if (newPos.y < sizeY && !CubeDataManager.Instance.IsEmpty((int)newPos.x, (int)newPos.y))
                    return false;
            }
            return true;
        }

        public bool UpdateMoveCubeWithUnitTime()
        {
            if (CubeDataManager.Instance.UpdateLock)
                return false;

            if (CheckCubeMoveable(Vector3.down))
            {
                foreach (var cube in currentControlCubes)
                {
                    var newPos = VectorUtility.AddPosition(PositionUtils.TransIndexToUnitPosition(cube.GetComponent<GameTileView>().Index), Vector3.down);
                    cube.GetComponent<GameTileView>().UpdatePositionByIndex(cube.GetComponent<GameTileView>().Index, PositionUtils.TransUnitPositionToIndex(newPos));
                }

                return true;
            }

            return false;
        }

        public bool MoveCube(Vector3 moveDirect)
        {
            if (CubeDataManager.Instance.UpdateLock)
                return false;

            if (CheckCubeMoveable(moveDirect))
            {
                foreach (var cube in currentControlCubes)
                {
                    var newPos = VectorUtility.AddPosition(PositionUtils.TransIndexToUnitPosition(cube.GetComponent<GameTileView>().Index), moveDirect);
                    cube.GetComponent<GameTileView>().UpdateTartgetPosition(PositionUtils.TransUnitPositionToPosition(moveDirect));
                    cube.GetComponent<GameTileView>().Index = PositionUtils.TransUnitPositionToIndex(newPos);
                }

                changeDirectBufferTime = 0;

                return true;
            }

            return false;
        }

        public void UpdateCubePositionWithTime()
        {
            if (CubeDataManager.Instance.UpdateLock)
                return;

            if (currentControlCubes != null)
            {
                foreach (var cube in currentControlCubes)
                {
                    cube.GetComponent<GameTileView>().UpdatePostion(currentUpdateTime, updateTime);
                }
            }

        }

        bool CurrentCubeSetted()
        {
            foreach (var cube in currentControlCubes)
            {
                var originPos = PositionUtils.TransIndexToUnitPosition(cube.GetComponent<GameTileView>().Index);

                if (originPos.y >= sizeY)
                {
                    return false;
                }

                if (!CubeDataManager.Instance.IsEmpty((int)originPos.x, (int)originPos.y))
                {
                    Debug.Log("CurrentCubeSetted Error 1");
                    return false;
                }
                else
                {
                    var gameTileView = cube.GetComponent<GameTileView>();
                    CubeDataManager.Instance.AddGameTile((int)originPos.x, (int)originPos.y, gameTileView.Tile);
                }
            }

            return true;
        }

        public void SpawnNewCube()
        {
            currentControlCubes = GameTileGroupManager.Instance.GetNextTileGroup().Select(a=>a.gameObject).ToArray();
        }

        public void SpawnFromClickedHoldContentArea()
        {
            if (CubeDataManager.Instance.UpdateLock || !GameTileGroupManager.Instance.Holdable)
                return;

            CubeDataManager.Instance.UpdateLock = true;
            currentControlCubes = GameTileGroupManager.Instance.SetHoldGroupAndGetNextTileGroup().Select(a => a.gameObject).ToArray();
            CubeDataManager.Instance.UpdateLock = false;
        }

        void CheckForInput()
        {
            Vector3 direct = Vector3.zero ;

            if (Input.GetKeyDown(KeyCode.A))
            {
                direct = Vector3.left;
                if (CheckCubeMoveable(direct))
                    MoveCube(direct);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                direct = Vector3.right;
                if (CheckCubeMoveable(direct))
                    MoveCube(direct);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                RotateCubes();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                QuickDrop();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
               //gameObject.GetComponent<Highscore>().setNewSpeed();
            }
        }

        public void QuickDrop()
        {
            updateTime = 0.02f;
        }

        public void RotateCubes()
        {
            if (CubeDataManager.Instance.UpdateLock)
                return;

            List<int> cubeIndex = new List<int>();

            for(int i = 0; i < currentControlCubes.Length; i++)
            {
                cubeIndex.Add(currentControlCubes[i].GetComponent<GameTileView>().Index);
            }

            for (int i = 0; i < currentControlCubes.Length; i++)
            {
                int newIndex = 0;

                if (i == 0)
                {
                    newIndex = cubeIndex[cubeIndex.Count - 1];
                }
                else
                {
                    newIndex = cubeIndex[i - 1];
                }

                GameTileView view = currentControlCubes[i].GetComponent<GameTileView>();
                view.UpdateTartgetPosition(VectorUtility.MinusPosition(PositionUtils.TransIndexToPosition(view.Index), PositionUtils.TransIndexToPosition(newIndex)));
                view.Index = newIndex;
            }

            changeDirectBufferTime = 0;
        }
    }
}
