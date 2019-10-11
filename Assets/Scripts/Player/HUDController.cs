using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public RectTransform[] crosshair;
    public Vector2 crosshairSize;

    private Canvas canvas;

    public void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Zoom(float zoom)
    {
        crosshair[0].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, crosshairSize.x / zoom);
        crosshair[0].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, crosshairSize.y / zoom);

        crosshair[1].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, crosshairSize.x);
        crosshair[1].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, crosshairSize.y);
    }

    public void SetCrosshairSprites(Sprite[] sprites)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            crosshair[i].GetComponent<Image>().sprite = sprites[i];
        }
    }

    public void SetAim(Vector3 crosshairPos)
    {
        float moveSpeed = 400f;
        Vector2 uiOffset = canvas.pixelRect.size * .5f;

        Vector2 oldPos = new Vector2(crosshair[0].localPosition.x, crosshair[0].localPosition.y);
        Vector2 newPos = new Vector2(crosshairPos.x * canvas.pixelRect.size.x, crosshairPos.y * canvas.pixelRect.size.y);
        newPos -= uiOffset;

        float moveDiff = (newPos - oldPos).magnitude;

        newPos = Vector2.Lerp(oldPos, newPos, Mathf.Clamp01(moveSpeed * Time.deltaTime / moveDiff));
        crosshair[0].localPosition = newPos;
    }
}
