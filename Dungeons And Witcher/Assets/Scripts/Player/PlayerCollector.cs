using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public Player player { get; private set; }
    private List<ICollectible> collectiblesInRange = new List<ICollectible>();
    private bool canCollect = true;

    public void Initialize(Player player)
    {
        this.player = player;
    }

    private void Update()
    {
        if (player.inputHandle.collectorInput && canCollect && collectiblesInRange.Count > 0)
        {
            ICollectible currentCollectible = collectiblesInRange[0];
            currentCollectible.Collect();
            canCollect = false;
        }
        else if (!player.inputHandle.collectorInput)
        {
            canCollect = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null && !collectiblesInRange.Contains(collectible))
        {
            collectiblesInRange.Add(collectible);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null && !collectiblesInRange.Contains(collectible))
        {
            collectiblesInRange.Add(collectible);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectiblesInRange.Remove(collectible);
        }
    }
}
