using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KissTetris.GameScene
{
    public enum TileType
    {
        BoyForward,
        BoyLeft,
        BoyRight,
        GirlForward,
        GirlLeft,
        GirlRight,
        Underwear,
        SlapLeft,
        SlapRight,
    }

    public enum ItemType
    {
        Face,
        Item,
    }

    public enum Sex
    {
        None,
        Boy,
        Girl
    }

    public enum FaceDirect
    {
        Left,
        Forward,
        Right
    }

    public class GameTile
    {
        public TileType tileType;
        public ItemType itemType;
        public Sex sex;
        public FaceDirect direct;

        public int Index;
        
        public GameTile(TileType type)
        {
            tileType = type;
            Initialize(type);
        }

        public void Initialize(TileType type)
        {
            switch (type)
            {
                case TileType.BoyForward:
                    sex = Sex.Boy;
                    direct = FaceDirect.Forward;
                    itemType = ItemType.Face;
                    break;
                case TileType.BoyLeft:
                    sex = Sex.Boy;
                    direct = FaceDirect.Left;
                    itemType = ItemType.Face;
                    break;
                case TileType.BoyRight:
                    sex = Sex.Boy;
                    direct = FaceDirect.Right;
                    itemType = ItemType.Face;
                    break;
                case TileType.GirlForward:
                    sex = Sex.Girl;
                    direct = FaceDirect.Forward;
                    itemType = ItemType.Face;
                    break;
                case TileType.GirlLeft:
                    sex = Sex.Girl;
                    direct = FaceDirect.Left;
                    itemType = ItemType.Face;
                    break;
                case TileType.GirlRight:
                    sex = Sex.Girl;
                    direct = FaceDirect.Right;
                    itemType = ItemType.Face;
                    break;
                case TileType.SlapLeft:
                    sex = Sex.None;
                    direct = FaceDirect.Left;
                    itemType = ItemType.Item;
                    break;
                case TileType.SlapRight:
                    sex = Sex.None;
                    direct = FaceDirect.Right;
                    itemType = ItemType.Item;
                    break;
                case TileType.Underwear:
                    sex = Sex.None;
                    direct = FaceDirect.Forward;
                    itemType = ItemType.Item;
                    break;
            }
        }

        public void SetNewIndex(int index)
        {
            Index = index;
        }

        public void UpdateDirectBySlap(TileType type)
        {
            if (type == TileType.SlapLeft)
                direct += -1;
            else if (type == TileType.SlapRight)
                direct += 1;

            if (direct > FaceDirect.Right)
                direct = FaceDirect.Left;
            else if (direct < FaceDirect.Left)
                direct = FaceDirect.Right;
        }
    }
}
