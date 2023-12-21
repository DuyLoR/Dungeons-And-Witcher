using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrbItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public OrbData orbData { get; private set; }
    public Transform parentAfterDrag { get; private set; }

    private Image image;
    public void InitialiseOrbItem(OrbData neworbData)
    {
        this.orbData = neworbData;
        image = GetComponent<Image>();
        image.sprite = orbData.orbPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
    public void SetParentAfterDrag(Transform transform)
    {
        parentAfterDrag = transform;
    }
}
