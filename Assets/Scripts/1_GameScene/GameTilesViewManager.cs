using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KissTetris.GameScene
{
    public class GameTilesViewManager : Singleton<GameTilesViewManager>
    {
        List<GameTileView> gameTileViews = new List<GameTileView>();

        public void Initialize()
        {
            gameTileViews = new List<GameTileView>();
        }

        public void AddView(GameTileView view)
        {
            gameTileViews.Add(view);
        }

        public void UpdateViews()
        {
            foreach (var view in gameTileViews)
            {
                view.UpdatePositionWithITweenAnimation();
            }
        }

        public GameTileView GetView(int index)
        {
            return gameTileViews.Where(a => a.Index == index).FirstOrDefault();
        }

        public void RemoveView(int index)
        {
            var view = gameTileViews.Where(a=>a.Index == index).FirstOrDefault();

            if (view != null)
            {
                view.Disappear();
                gameTileViews.Remove(view);
                //Temp: should use pool
                Destroy(view.gameObject);
            }
        }

        public void Reset()
        {
            foreach(var view in gameTileViews)
            {
                Destroy(view.gameObject);
            }
        }
    }
}