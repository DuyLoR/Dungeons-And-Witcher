using NavMeshPlus.Components;
using UnityEngine;

public class NavMesh : MonoBehaviour
{
    public static NavMesh instance;
    public NavMeshSurface surface;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }
    public void UpdateNavMesh()
    {
        surface.BuildNavMesh();
    }
}
