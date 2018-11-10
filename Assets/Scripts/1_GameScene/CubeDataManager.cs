using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KissTetris.Utilities;

namespace KissTetris.GameScene
{
    public class CubeDataManager : Singleton<CubeDataManager>
    {
        GameTile[,] tiles;

        int sizeX = 6;
        int sizeY = 12;

        List<List<int>> IndexesToDisappear = new List<List<int>>();

        public bool UpdateLock = false;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            UpdateLock = true;
            tiles = new GameTile[sizeX, sizeY];
        }

        public void Reset()
        {
            UpdateLock = true;
        }

        public IEnumerator UpdateWithTileSetted()
        {
            UpdateLock = true;
            ScoreManager.Instance.InitializeCombo();
            yield return RunTileSetted();
        }

        public IEnumerator RunTileSetted()
        {
            ScoreManager.Instance.AddCombo();
            ScoreManager.Instance.UpdateScoreText();
            int itemIndex = CheckIfItemExistAndReturnIndex();

            if (CheckTiles())
            {
                PlayKissAnimation();
                yield return new WaitForSeconds(0.15f);
                UpdateDisappear();
                yield return new WaitForSeconds(0.15f);
                UpdateTilesPosition();
                GameTilesViewManager.Instance.UpdateViews();
                yield return new WaitForSeconds(0.2f);
                yield return RunTileSetted();
            }
            else if (itemIndex != -1)
            {
                HandleItemEffect(itemIndex);
                UpdateItemDisappear();
                yield return new WaitForSeconds(0.15f);
                UpdateTilesPosition();
                GameTilesViewManager.Instance.UpdateViews();
                yield return new WaitForSeconds(0.2f);
                yield return RunTileSetted();
            }
            else
            {
                CubeMoveController.Instance.SpawnNewCube();
                CubeMoveController.Instance.InitializeDropSpeed();
                UpdateLock = false;
            }
        }

        public void AddGameTile(int x, int y, GameTile tile)
        {
            if (tile != null)
            {
                tiles[x, y] = tile;
                tile.SetNewIndex(PositionUtils.TransUnitPositionToIndex(new Vector2(x, y)));
            }
        }

        public bool IsEmpty(int x, int y)
        {
            return tiles[x, y] == null;
        }

        public void RemoveGameTile(int x, int y)
        {
            tiles[x, y] = null;
        }

        #region CheckTile

        public bool CheckTiles()
        {
            IndexesToDisappear = new List<List<int>>();

            for (int j = 0; j < sizeY; j++)
                for (int i = 0; i < sizeX; i++)
                {
                    int index = PositionUtils.TransUnitPositionToIndex(new Vector2(i, j));

                    if (i <= sizeX - 2 && CheckLeftRightTiles(tiles[i, j], tiles[i + 1, j]))
                    {
                        IndexesToDisappear.Add(new List<int>() { index, index + 1 });
                        ScoreManager.Instance.AddScore(DestroyType.Kiss, 2);
                    }
                    else if (i <= sizeX - 3)
                    {
                        int lastReturnPosX = CheckMiddleKiss(new Vector2(i, j));

                        if (lastReturnPosX != -1)
                        {
                            List<int> middleKissIndexes = new List<int>();

                            for (int z = 0; z <= lastReturnPosX - i; z++)
                            {
                                middleKissIndexes.Add(index + z);
                            }

                            IndexesToDisappear.Add(middleKissIndexes);
                            ScoreManager.Instance.AddScore(DestroyType.Kiss, middleKissIndexes.Count);
                        }
                    }
                }

            if (IndexesToDisappear.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckLeftRightTiles(GameTile tileLeft, GameTile tileRight)
        {
            if (tileLeft == null || tileRight == null)
                return false;

            if (tileLeft.itemType == ItemType.Face && tileRight.itemType == ItemType.Face
               && tileLeft.direct == FaceDirect.Right && tileRight.direct == FaceDirect.Left)
                return true;

            return false;
        }

        public bool CheckThreeKiss(GameTile tileLeft, GameTile tileCenter, GameTile tileRight)
        {
            if (tileLeft == null || tileRight == null || tileCenter == null)
                return false;

            if (tileLeft.itemType == ItemType.Face && tileCenter.itemType == ItemType.Face && tileRight.itemType == ItemType.Face
                && tileLeft.direct == FaceDirect.Right && tileCenter.direct == FaceDirect.Forward && tileRight.direct == FaceDirect.Left)
                return true;

            return false;
        }

        public int CheckMiddleKiss(Vector2 startPosition)
        {
            if (tiles[(int)startPosition.x, (int)startPosition.y] == null || tiles[(int)startPosition.x, (int)startPosition.y].itemType != ItemType.Face || tiles[(int)startPosition.x, (int)startPosition.y].direct != FaceDirect.Right)
                return -1;

            for (int i = (int)startPosition.x + 1; i <= GameSettings.sizeX - 1; i++)
            {
                if (tiles[i, (int)startPosition.y] != null && tiles[i, (int)startPosition.y].itemType == ItemType.Face && tiles[i, (int)startPosition.y].direct == FaceDirect.Forward)
                {
                    continue;
                }
                else if (tiles[i, (int)startPosition.y] != null && tiles[i, (int)startPosition.y].itemType == ItemType.Face && tiles[i, (int)startPosition.y].direct == FaceDirect.Left)
                {
                    return i;
                }
                else
                {
                    return -1;
                }
            }

            return -1;
        }

        public void PlayKissAnimation()
        {
            for (int i = 0; i < IndexesToDisappear.Count; i++)
            {
                if (IndexesToDisappear[i].Count == 2)
                {
                    GameTilesViewManager.Instance.GetView(IndexesToDisappear[i][0]).GetComponent<GameFaceView>().PlayKissAnimation();
                    GameTilesViewManager.Instance.GetView(IndexesToDisappear[i][1]).GetComponent<GameFaceView>().PlayKissAnimation();
                }
                else if (IndexesToDisappear[i].Count >= 3)
                {
                    GameTilesViewManager.Instance.GetView(IndexesToDisappear[i][0]).GetComponent<GameFaceView>().PlayKissAnimation();
                    GameTilesViewManager.Instance.GetView(IndexesToDisappear[i][IndexesToDisappear[i].Count - 1]).GetComponent<GameFaceView>().PlayKissAnimation();
                }
            }
        }

        public void UpdateDisappear()
        {
            for (int i = 0; i < IndexesToDisappear.Count; i++)
            {
                if (IndexesToDisappear[i].Count == 2)
                {
                    var heartPosition = VectorUtility.MidPoints(PositionUtils.TransIndexToPosition(IndexesToDisappear[i][0]), PositionUtils.TransIndexToPosition(IndexesToDisappear[i][1]));
                    EffectViewManager.Instance.GenerateEffects("heart", heartPosition, 0.3f);
                }
                else if (IndexesToDisappear[i].Count >= 3)
                {
                    var heartPosition1 = VectorUtility.MidPoints(PositionUtils.TransIndexToPosition(IndexesToDisappear[i][0]), PositionUtils.TransIndexToPosition(IndexesToDisappear[i][1]));
                    var heartPosition2 = VectorUtility.MidPoints(PositionUtils.TransIndexToPosition(IndexesToDisappear[i][IndexesToDisappear[i].Count - 2]), PositionUtils.TransIndexToPosition(IndexesToDisappear[i][IndexesToDisappear[i].Count - 1]));
                    EffectViewManager.Instance.GenerateEffects("heart", heartPosition1, 0.3f);
                    EffectViewManager.Instance.GenerateEffects("heart", heartPosition2, 0.3f);
                }
            }
            
            if (IndexesToDisappear.Count > 0)
            {
                for (int i = 0; i < IndexesToDisappear.Count; i++)
                {
                    for(int j=0; j<IndexesToDisappear[i].Count; j++)
                    {
                        var tilesPos = PositionUtils.TransIndexToPosition(IndexesToDisappear[i][j]);
                        var tileUnitPos = PositionUtils.TransIndexToUnitPosition(IndexesToDisappear[i][j]);
                        var tile = tiles[(int)tileUnitPos.x, (int)tileUnitPos.y];
                        EffectViewManager.Instance.GenerateEffects("vanish", tilesPos,0.3f);
                        GameTilesViewManager.Instance.RemoveView(IndexesToDisappear[i][j]);
                        RemoveGameTile((int)tileUnitPos.x, (int)tileUnitPos.y);
                    }
                }
            }
        }

        #endregion

        #region CheckItems

        public int CheckIfItemExistAndReturnIndex()
        {
            for (int j = 0; j < sizeY; j++)
                for (int i = 0; i < sizeX; i++)
                {
                    if (tiles[i,j] != null && tiles[i, j].itemType == ItemType.Item)
                        return PositionUtils.TransUnitPositionToIndex(new Vector2(i, j));
                }

            return -1;
        }

        public void HandleItemEffect(int index)
        {
            IndexesToDisappear = new List<List<int>>();

            var tilePosition = PositionUtils.TransIndexToUnitPosition(index);
            var tileType = tiles[(int)tilePosition.x, (int)tilePosition.y].tileType;
            switch (tileType)
            {
                case TileType.Underwear:
                    HandleUnderwearEffect(index);
                    break;
                case TileType.SlapLeft:
                case TileType.SlapRight:
                    HandleSlapEffect(index, tileType);
                    break;
            }
        }

        public void HandleUnderwearEffect(int index)
        {
            if (index - sizeX >=0)
            {
                IndexesToDisappear.Add(new List<int>() { index, index - sizeX });
                ScoreManager.Instance.AddScore(DestroyType.Underwear, 1);
            }
            else
            {
                IndexesToDisappear.Add(new List<int>() { index});
            }
        }

        public void HandleSlapEffect(int index, TileType type)
        {
            IndexesToDisappear.Add(new List<int>() { index });

            if (index - sizeX >= 0)
            {
                var faceTileIndex = index - sizeX;
                var tile = GetGameTileByIndex(faceTileIndex);
                tile.UpdateDirectBySlap(type);
                ((GameFaceView)GameTilesViewManager.Instance.GetView(faceTileIndex)).UpdateFaceDirection();
            }
        }

        public void UpdateItemDisappear()
        {
            if(IndexesToDisappear.Count > 0)
            {
                for (int i = 0; i < IndexesToDisappear.Count; i++)
                {
                    for (int j = 0; j < IndexesToDisappear[i].Count; j++)
                    {
                        var tilesPos = PositionUtils.TransIndexToPosition(IndexesToDisappear[i][j]);
                        var tileUnitPos = PositionUtils.TransIndexToUnitPosition(IndexesToDisappear[i][j]);
                        var tile = tiles[(int)tileUnitPos.x, (int)tileUnitPos.y];
                        EffectViewManager.Instance.GenerateEffects("vanish", tilesPos, 0.3f);
                        GameTilesViewManager.Instance.RemoveView(IndexesToDisappear[i][j]);
                        RemoveGameTile((int)tileUnitPos.x, (int)tileUnitPos.y);
                    }
                }
            }
        }

        #endregion

        public void UpdateTilesPosition()
        {
            for (int i = 0; i < sizeX; i++)
            {
                int offsetY = 0;

                for (int j = 0; j < sizeY; j++)
                {
                    if (IsEmpty(i, j))
                    {
                        offsetY++;
                    }
                    else if(offsetY > 0)
                    {
                        int index = PositionUtils.TransUnitPositionToIndex(new Vector2(i, j - offsetY));
                        tiles[i, j].SetNewIndex(index);
                        tiles[i, j - offsetY] = tiles[i, j];
                        tiles[i, j] = null;
                    }
                }
            }
        }

        public GameTile GetGameTileByIndex(int index)
        {
            var tileUnitPosition = PositionUtils.TransIndexToUnitPosition(index);
            return tiles[(int)tileUnitPosition.x, (int)tileUnitPosition.y];
        }
    }
}