using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator animator;

    private void Awake()
    {
        instance = this;

    }
    public void LoadScene()
    {
        GetComponentInChildren<Canvas>().sortingOrder = 2;
        StartCoroutine(Loading(SceneManager.GetActiveScene().buildIndex == 0 ? 1 : 0));
    }

    IEnumerator Loading(int level)
    {
        animator.Play("CrossfadeStart");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level);
    }
}
