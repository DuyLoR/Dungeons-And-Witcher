using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        LevelLoader.instance.LoadScene();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
