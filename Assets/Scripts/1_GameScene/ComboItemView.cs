using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboItemView : MonoBehaviour
{
    [SerializeField]
    SpriteChanger numberChanger;

    public void Initialize(int number)
    {
        numberChanger.ChangeSprite(number);
    }
}
