using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrbItem : MonoBehaviour, ICollectible, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<OrbData> OnOrbCollected;
    public OrbData orbData;
    public Transform parentAfterDrag { get; private set; }
    public GameObject gameObjectToDropped { get; private set; }

    private Image image;
    public void InitializeOrbItem(OrbData neworbData)
    {
        this.orbData = neworbData;
        image = GetComponent<Image>();
        image.sprite = orbData.orbPrefab.GetComponent<SpriteRenderer>().sprite;
        image.preserveAspect = true;
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
        if (gameObjectToDropped == null)
        {
            Destroy(gameObject);
        }
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
    public void SetParentAfterDrag(Transform transform)
    {
        gameObjectToDropped = transform.gameObject;
        parentAfterDrag = transform;
    }
    public void SetTransform()
    {
        transform.SetParent(parentAfterDrag);
    }

    public void Collect()
    {
        Destroy(gameObject);
        OnOrbCollected?.Invoke(orbData);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTip.instance.gameObject.SetActive(true);
        ToolTip.instance.GenerateToolTip(GetComponentInChildren<OrbItem>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.instance.gameObject.SetActive(false);
    }


}
