using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class GameSpeedMenuView : MonoBehaviour
    {
        public void OnClickedButton1()
        {
            OnSelectedSpeed(1);
        }

        public void OnClickedButton2()
        {
            OnSelectedSpeed(2);
        }

        public void OnClickedButton3()
        {
            OnSelectedSpeed(3);
        }

        public void OnClickedButton4()
        {
            OnSelectedSpeed(4);
        }

        public void OnClickedButton5()
        {
            OnSelectedSpeed(5);
        }

        public void OnSelectedSpeed(int speed)
        {
            CubeMoveController.Instance.SetSpeedFactor(speed);
            GameSettingMenu.Instance.OnSelectedGameSpeed();
        }
    }
}
