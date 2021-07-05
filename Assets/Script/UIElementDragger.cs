using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : MonoBehaviour, IDragHandler
{
    public const float SENSIVITY = 0.1f;

    private bool dragging;

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta * SENSIVITY;
    }

    public void Update()
    {
    }

}