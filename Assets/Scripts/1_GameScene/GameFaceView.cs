using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.GameScene
{
    public enum FaceAnimation
    {
        IdleFaceFront,
        IdleFaceRight,
        IdleFaceLeft,
        FaceTurnFrontToLeft,
        FaceTurnFrontToRight,
        FaceTurnLeftToRight,
        FaceTurnRightToLeft,
    }

    public class GameFaceView : GameTileView
    {
        [SerializeField]
        Animator animator;

        public override void Initialize(TileType type)
        {
            base.Initialize(type);
            animator = gameObject.GetComponentInChildren<Animator>();
            UpdateFaceDirection();
        }

        public void UpdateFaceDirection()
        {
            switch (Tile.direct)
            {
                case FaceDirect.Forward:
                    animator.SetInteger("FaceDirect", (int)FaceDirect.Forward);
                    break;
                case FaceDirect.Left:
                    animator.SetInteger("FaceDirect", (int)FaceDirect.Left);
                    break;
                case FaceDirect.Right:
                    animator.SetInteger("FaceDirect", (int)FaceDirect.Right);
                    break;
            }
        }

        public void PlayKissAnimation()
        {
            animator.SetInteger("Kiss", 1);
        }
    }
}