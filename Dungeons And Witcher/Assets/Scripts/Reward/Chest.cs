using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> orbItems = new List<GameObject>();
    [SerializeField]
    private GameObject healItem;
    [SerializeField]
    private GameObject rellicUI;
    [SerializeField]
    private WeaponData weaponItem;

    private bool isOpened = false;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void SpawnItem()
    {
        float randomValue = Random.value;
        if (randomValue < 0.1)
        {
            Instantiate(weaponItem.weaponPrefab, transform);
        }
        else if (randomValue < 0.2f)
        {
            Time.timeScale = 0f;
            Instantiate(rellicUI, UI.instance.transform);
        }
        else if (randomValue < 0.4f)
        {
            Instantiate(healItem, transform);
        }
        else
        {
            var orbItem = orbItems[Random.Range(0, orbItems.Count - 1)];
            var newOrbItem = Instantiate(orbItem, transform);
            newOrbItem.GetComponent<Orb>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpened)
        {
            isOpened = true;
            animator.Play("open");
            Invoke(nameof(SpawnItem), 2f);
        }
    }
}
