using System;
using UnityEngine;

public class OrbItem : MonoBehaviour, ICollectible
{
    public static event Action<OrbData> OnOrbCollected;
    public OrbData orbData;
    public void Collect()
    {
        Destroy(gameObject);
        OnOrbCollected?.Invoke(orbData);
    }
}
