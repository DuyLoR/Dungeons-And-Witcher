using UnityEngine;

public class Hearth : MonoBehaviour, ICollectible
{
    [SerializeField]
    private int minHpCollect = 10;
    [SerializeField]
    private int maxHpCollect = 30;
    public void Collect()
    {
        Player.Instance.UpHp(Random.Range(minHpCollect, maxHpCollect));
        Destroy(gameObject);
    }
}
