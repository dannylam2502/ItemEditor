using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : MonoBehaviour, IDragHandler
{

    private bool dragging;

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta * 0.01f;
    }

    public void Update()
    {
    }

}