using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.GameScene
{
    public class GameTileView : MonoBehaviour
    {
       protected TileType tileType;
       public GameTile Tile;

        float animationTime = 0.2f;

        public int Index;

        Vector3 currentStartPos;
        Vector3 nextPos;

        public virtual void Initialize(TileType type)
        {
            tileType = type;
            Tile = new GameTile(tileType);
        }

        public void InitializeIndex(int index)
        {
            this.Index = index;
            currentStartPos = PositionUtils.TransIndexToPosition(index);
            nextPos = PositionUtils.TransIndexToPosition(index);
        }

        public void UpdatePositionWithITweenAnimation()
        {
            if (Tile != null && Index != Tile.Index && Index >= 0)
            {
                Index = Tile.Index;
                MoveAnimation(PositionUtils.TransIndexToPosition(Index));
            }
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
        }

        public void MoveAnimation(Vector3 pos)
        {
            Vector2 moveOffset = new Vector2(pos.x - gameObject.transform.position.x, pos.y - gameObject.transform.position.y);

            iTween.MoveAdd(gameObject, iTween.Hash(
            "y", moveOffset.y,
            "easeType", iTween.EaseType.linear,
            "time", 0.2f
            ));
        }

        //With Input right or left
        public void UpdateTartgetPosition(Vector3 direct)
        {
            currentStartPos = new Vector3(currentStartPos.x + direct.x, currentStartPos.y + direct.y, currentStartPos.z + direct.z);
            nextPos = new Vector3(nextPos.x + direct.x, nextPos.y + direct.y, nextPos.z + direct.z);
        }

        public void UpdatePostion(float currentUpdateTime, float totalTime)
        {
            if (currentUpdateTime > totalTime)
                currentUpdateTime = totalTime;
            Vector3 pos = Vector3.Lerp(currentStartPos, nextPos, currentUpdateTime / totalTime);
            transform.position = pos;
        }

        public void UpdatePositionByIndex(int currentIndex, int nextIndex)
        {
            currentStartPos = PositionUtils.TransIndexToPosition(currentIndex);
            nextPos = PositionUtils.TransIndexToPosition(nextIndex);
            Index = nextIndex;
        }
    }
}
