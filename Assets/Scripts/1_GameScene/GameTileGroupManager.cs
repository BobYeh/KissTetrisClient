using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KissTetris.Utilities;

namespace KissTetris.GameScene
{
    public class GameTileGroupManager : Singleton<GameTileGroupManager>
    {
        [SerializeField]
        Transform nextGroupContent;
        [SerializeField]
        Transform holdGroupContent;
        [SerializeField]
        Transform gameTilesGroupContent;

        GameTileView[] currentControllGroup;

        GameTileView[] nextControllGroup;

        GameTileView[] holdControllGroup;

        public bool Holdable = true;

        public void Initialize()
        {
            
        }

        public void Reset()
        {
            currentControllGroup = null;
            nextControllGroup = null;
            holdControllGroup = null;
        }

        public GameTileView[] GetNextTileGroup()
        {
            if (nextControllGroup == null)
            {
                GenerateNextTileGroup();
            }

            currentControllGroup = nextControllGroup;
            InitializeGroupDataAndPosition(currentControllGroup);

            GenerateNextTileGroup();

            Holdable = true;

            return currentControllGroup;
        }

        public void InitializeGroupDataAndPosition(GameTileView[] group)
        {
            Vector2 tileStartUnitPos = GameSettings.spawnStartPoint;

            for (int i = 0; i < group.Length; i++)
            {
                group[i].transform.SetParent(gameTilesGroupContent);
                group[i].transform.localPosition = PositionUtils.TransUnitPositionToPosition(new Vector3 (tileStartUnitPos.x, tileStartUnitPos.y + i));
                group[i].InitializeIndex(PositionUtils.TransPositionToIndex(group[i].transform.position));
            }
        }

        public void ResetGroupData(GameTileView[] group)
        {
            for (int i = 0; i < group.Length; i++)
            {
                group[i].InitializeIndex(-1);
            }
        }

        public GameTileView[] GenerateNextTileGroup()
        {
            nextControllGroup = Spawner.Instance.SpawnGroups(GameSettings.numberOfGroupItems);
            SetGroupToNextContent(nextControllGroup);
            return nextControllGroup;
        }

        public GameTileView[] SetHoldGroupAndGetNextTileGroup()
        {
            Holdable = false;

            GameTileView[] tempGroup = null;

            if (holdControllGroup != null)
            {
                tempGroup = holdControllGroup;
            }

            SetGroupToHoldContent(currentControllGroup);
            holdControllGroup = currentControllGroup;
            ResetGroupData(holdControllGroup);

            if (tempGroup != null)
            {
                currentControllGroup = tempGroup;
                InitializeGroupDataAndPosition(currentControllGroup);
                return currentControllGroup;
            }
            else
            {
                return GetNextTileGroup();
            }
        }

        public void SetGroupToHoldContent(GameTileView[] group)
        {
            for (int i = 0; i < group.Length; i++)
            {
                group[i].transform.SetParent(holdGroupContent);
                group[i].transform.localPosition = new Vector3(0, GameSettings.unitHeight * i, 0);
            }
        }

        public void SetGroupToNextContent(GameTileView[] group)
        {
            for(int i=0; i< group.Length; i++)
            {
                group[i].transform.SetParent(nextGroupContent);
                group[i].transform.localPosition = new Vector3(0, GameSettings.unitHeight * i, 0);
            }
        }
    }
}
