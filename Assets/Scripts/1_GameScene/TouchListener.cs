using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KissTetris.GameScene
{
    public class TouchListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
    {

        Vector3 startPos;
        Vector3 pointerDownPos;
        float pressTime;

        float lastOnDragTime;

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPos = eventData.position;
            Debug.Log("OnBeginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            var newPos = eventData.position;
            Vector2 posDiff = new Vector2(newPos.x - startPos.x, newPos.y - startPos.y);

            if (posDiff.x > GameSettings.moveThreshold)
            {
                CubeMoveController.Instance.MoveCube(Vector3.right);
                startPos = newPos;
            }
            else if (posDiff.x < -1 * GameSettings.moveThreshold)
            {
                CubeMoveController.Instance.MoveCube(Vector3.left);
                startPos = newPos;
            }
            else if (eventData.delta.y < -1 * GameSettings.dropThreshold)
            {
                CubeMoveController.Instance.QuickDrop();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pressTime = Time.time;
            pointerDownPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var pointerUpPos = eventData.position;
            Vector2 posDiff = new Vector2(pointerUpPos.x - pointerDownPos.x, pointerUpPos.y - pointerDownPos.y);

            if (Time.time - pressTime < 0.3f && Mathf.Abs(posDiff.x) < GameSettings.clickPressTimeThreshold && Mathf.Abs(posDiff.y) < GameSettings.clickMovementThreshold)
            {
                CubeMoveController.Instance.RotateCubes();
            }
        }
    }
}
