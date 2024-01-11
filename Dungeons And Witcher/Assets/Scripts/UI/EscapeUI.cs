using UnityEngine;

public class EscapeUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        Debug.Log(PlayerInputHandle.Instance.EscapeInput);
        if (PlayerInputHandle.Instance.EscapeInput)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
