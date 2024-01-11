using System.Collections.Generic;
using UnityEngine;

public class OrbSpawnPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> orbPrefabs;
    public List<Queue<GameObject>> availableObjects = new List<Queue<GameObject>>();

    public static OrbSpawnPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void GrowPool(GameObject orb)
    {
        int i = GetOrbIndex(orb);
        var InstanceToAdd = Instantiate(orb);
        InstanceToAdd.transform.SetParent(transform);
        AddToPool(InstanceToAdd);
    }

    public void AddToPool(GameObject instanceToAdd)
    {
        int index = GetOrbIndex(instanceToAdd);
        if (index == -1) return;
        instanceToAdd.SetActive(false);
        availableObjects[index].Enqueue(instanceToAdd);
    }

    public bool CheckIfOrbInPool(GameObject orb)
    {
        return orbPrefabs.Contains(orb);
    }
    public void AddOrbToOrbPrefabs(GameObject orb)
    {
        orbPrefabs.Add(orb);
        int i = GetOrbIndex(orb);
        availableObjects.Add(new Queue<GameObject>());
        orb.GetComponent<Orb>().enabled = true;
        orb.GetComponent<Orb>().SetOrbData(orb.GetComponent<OrbItem>().orbData);
    }
    public GameObject GetFromPool(GameObject orb)
    {
        orb.GetComponent<Orb>().enabled = true;
        int index = GetOrbIndex(orb);
        if (availableObjects[index].Count == 0)
        {
            GrowPool(orb);
        }
        var instance = availableObjects[index].Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public int GetOrbIndex(GameObject orb)
    {
        for (int i = 0; i < orbPrefabs.Count; i++)
        {
            if (orbPrefabs[i].name == orb.name.Replace("(Clone)", "").Trim()) return i;
        }
        return -1;
    }
}
