using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> orbItems = new List<GameObject>();
    //[SerializeField]
    //private float force = 0.5f;

    private bool isOpened = false;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void SpawnItem()
    {
        var orbItem = orbItems[Random.Range(0, orbItems.Count - 1)];
        var newOrbItem = Instantiate(orbItem, transform);
        newOrbItem.GetComponent<Orb>().enabled = false;
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
