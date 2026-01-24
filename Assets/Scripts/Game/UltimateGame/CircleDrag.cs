using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class CircleDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform cylinder;
    public UltimateGameManager ultimate;
    RectTransform rect;
    RectTransform parentRect;

    Vector2 lineStart;
    Vector2 lineEnd;

    Vector2 pos;

    void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        parentRect = rect.parent as RectTransform;

        if (cylinder == null)
            cylinder = transform.parent.GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases();
        RecalculateLine();
    }


    public void RecalculateLine()
    {
        float endPadding = 50f; 
        
        float width = cylinder.rect.width;
        float height = cylinder.rect.height;

        Vector3 pivotOffset = new Vector3(
            (0.5f - cylinder.pivot.x) * width,
            (0.5f - cylinder.pivot.y) * height,
            0f
        );

        Vector3 localLeft = pivotOffset + Vector3.left * (width * 0.5f - endPadding);
        Vector3 localRight = pivotOffset + Vector3.right * (width * 0.5f - endPadding);

        Vector3 worldLeft = cylinder.TransformPoint(localLeft);
        Vector3 worldRight = cylinder.TransformPoint(localRight);

        lineStart = parentRect.InverseTransformPoint(worldLeft);
        lineEnd = parentRect.InverseTransformPoint(worldRight);
        SnapToStart();
    }

    public void SnapToStart()
    {
        rect.anchoredPosition = lineStart;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        pos = rect.anchoredPosition;
        MoveAlongLine(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveAlongLine(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(rect.anchoredPosition, lineEnd) < 3f)
        {
            ultimate.Next();
        }
        else
        {
            rect.anchoredPosition = pos;
        }
    }

    void MoveAlongLine(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 mouseLocal
        );

        Vector2 dir = (lineEnd - lineStart).normalized;
        float length = Vector2.Distance(lineStart, lineEnd);

        float d = Vector2.Dot(mouseLocal - lineStart, dir);
        d = Mathf.Clamp(d, 0f, length);

        rect.anchoredPosition = lineStart + dir * d;
    }



}
