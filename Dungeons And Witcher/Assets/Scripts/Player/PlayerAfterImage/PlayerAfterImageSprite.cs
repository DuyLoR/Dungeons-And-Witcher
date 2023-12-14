using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSpriteRender;

    private Color color;

    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActived;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaDecay = 0.85f;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSpriteRender = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        spriteRenderer.sprite = playerSpriteRender.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActived = Time.time;
    }

    private void Update()
    {
        alpha *= alphaDecay;
        color = new Color(1f, 1f, 1f, alpha);

        spriteRenderer.color = color;

        if (Time.time >= timeActived + activeTime)
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
