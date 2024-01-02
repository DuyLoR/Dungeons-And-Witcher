using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField]
    private float timeToActive = .5f;

    void Start()
    {
        Destroy(gameObject, timeToActive);
    }

}
