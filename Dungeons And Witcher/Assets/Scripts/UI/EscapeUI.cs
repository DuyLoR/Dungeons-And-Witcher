using UnityEngine;

public class EscapeUI : MonoBehaviour
{
    public static EscapeUI instance;
    public CanvasGroup canvasGroup;
    private void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
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
        LevelLoader.instance.LoadScene();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ActiveUI()
    {
        Time.timeScale = 0f;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    public void DeActiveUI()
    {
        Time.timeScale = 1f;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}