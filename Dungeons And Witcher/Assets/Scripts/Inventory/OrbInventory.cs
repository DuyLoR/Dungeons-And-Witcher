using System;

[Serializable]
public class OrbInventory
{
    public OrbData orbData;
    public bool isExisted;
    public OrbInventory(OrbData orbData)
    {
        this.orbData = orbData;
        AddToInventory();
    }

    public void AddToInventory()
    {
        isExisted = true;
    }
    public void RemoveFromInventory()
    {
        isExisted = false;
    }
}
