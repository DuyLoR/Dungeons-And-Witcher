using UnityEngine;

public class EscapeUI : MonoBehaviour
{
    public static EscapeUI instance;
    public CanvasGroup canvasGroup;
    private bool isFreezeTime;
    private void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        isFreezeTime = false;
    }
    private void Update()
    {
        if (PlayerInputHandle.Instance.escapeInput) ActiveUI();
        else DeActiveUI();
    }
    public void Resume()
    {
        PlayerInputHandle.Instance.escapeInput = false;
    }
    public void ReturnMainMenu()
    {
        PlayerInputHandle.Instance.escapeInput = false;
        LevelLoader.instance.LoadScene();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ActiveUI()
    {
        if (!isFreezeTime)
        {
            Time.timeScale = 0f;
            isFreezeTime = true;
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    public void DeActiveUI()
    {
        if (isFreezeTime)
        {
            Time.timeScale = 1f;
            isFreezeTime = false;
        }
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}