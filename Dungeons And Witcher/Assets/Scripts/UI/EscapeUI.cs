using UnityEngine;

public class EscapeUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private bool isResumeBtnClick;
    private void Awake()
    {
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
    private void ActiveUI()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    private void DeActiveUI()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}