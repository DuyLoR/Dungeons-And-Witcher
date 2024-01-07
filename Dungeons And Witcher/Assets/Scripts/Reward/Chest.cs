using System;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> orbItems = new List<GameObject>();
    [SerializeField, Range(0, 1)]
    private float spawnItemChange = 0.7f;
    [SerializeField]
    private float force = 0.5f;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void SpawnItem()
    {
        var orbItem = orbItems[UnityEngine.Random.Range(0, orbItems.Count - 1)];
        var newOrbItem = Instantiate(orbItem, transform);
        Rigidbody2D rb = newOrbItem.GetComponent<Rigidbody2D>();
        rb.AddForce((Vector2)Direction2D.GetRandomCardinal8Direction() * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.Play("open");
            SpawnItem();
        }
    }
}
