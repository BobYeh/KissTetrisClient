using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KissTetris.Common
{
    public class SpriteChanger : MonoBehaviour
    {

        [SerializeField]
        Sprite[] sprites;
        [SerializeField]
        Image image;

        public void ChangeSprite(int index)
        {
            if (sprites.Length > 0 && index >= 0 && index < sprites.Length)
            {
                image.sprite = sprites[index];
            }
        }
    }
}
