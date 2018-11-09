using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KissTetris.Common;

namespace KissTetris.GameScene
{
    public class ComboItemView : MonoBehaviour
    {
        [SerializeField]
        SpriteChanger numberChanger;

        public void Initialize(int number)
        {
            numberChanger.ChangeSprite(number);
        }
    }
}
