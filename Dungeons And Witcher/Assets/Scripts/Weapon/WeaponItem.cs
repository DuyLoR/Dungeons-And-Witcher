using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour, ICollectible, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event Action<WeaponData> OnWeaponCollected;
    public WeaponData weaponData;
    public Transform parentAfterDrag { get; private set; }
    public Transform droppedTransform { get; private set; }
    private Image image;
    public void InitializeWeaponItem(WeaponData newWeapodata)
    {
        this.weaponData = newWeapodata;
        image = GetComponent<Image>();
        image.sprite = weaponData.weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        image.preserveAspect = true;
    }
    public void InitalizeWeaponData(WeaponData newWeaponData)
    {
        this.weaponData = newWeaponData;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        droppedTransform = Player.Instance.transform;

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
        if (droppedTransform == Player.Instance.transform)
        {
            var weapon = Instantiate(weaponData.weaponPrefab);
            weapon.transform.position = droppedTransform.position;
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(droppedTransform);
            image.raycastTarget = true;
        }
    }

    public void SetDroppedTransform(Transform transform)
    {
        droppedTransform = transform;
    }
    public void SetTransform()
    {
        transform.SetParent(droppedTransform);
    }

    public void Collect()
    {
        Destroy(gameObject);
        OnWeaponCollected?.Invoke(weaponData);
    }
}
