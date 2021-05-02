using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Utils
{

    public static class UIExtension
    {
        public static PointerEventData Clone(this PointerEventData ped)
        {
            var clone = new PointerEventData(EventSystem.current);
            clone.position = ped.position;
            clone.delta = ped.delta;
            clone.clickTime = ped.clickTime;
            clone.clickCount = ped.clickCount;

            return clone;

        }

        public static Vector2 WorldToScreen(this Vector3 position) => Camera.main.WorldToScreenPoint(position);

        public static Vector2 WorldToCanvas(this Vector3 position, RectTransform canvasTransform) => Camera.main.WorldToScreenPoint(position).ToVector2().ScreenToCanvas(canvasTransform);

        public static Vector2 ScreenToCanvas(this Vector2 position, RectTransform canvasTransform)
        {
            Vector2 pos = position - (new Vector2(Screen.width, Screen.height) * 0.5f);
            pos *= new Vector2(canvasTransform.rect.width / Screen.width, canvasTransform.rect.height / Screen.height);
            return pos;
        }

        public static Vector3 ScreenToWorld(this Vector2 pos, Vector3 targetPosition, RectTransform canvasTransform)
        {
            var cameraPos = Camera.main.transform.position;

            var worldPosition = canvasTransform.TransformPoint(pos);

            var convertedPosition = new Vector3(
                (worldPosition.x - cameraPos.x) * (targetPosition.z - cameraPos.z) / (worldPosition.z - cameraPos.z) + cameraPos.x,
                (worldPosition.y - cameraPos.y) * (targetPosition.z - cameraPos.z) / (worldPosition.z - cameraPos.z) + cameraPos.y,
                targetPosition.z);

            return convertedPosition;
        }

        public static Vector2 CanvasToScreen(this Vector2 position, RectTransform canvasTransform)
        {
            position /= new Vector2(canvasTransform.rect.width / Screen.width, canvasTransform.rect.height / Screen.height);
            position += (new Vector2(Screen.width, Screen.height) * 0.5f);
            return position;
        }
    }




}
