using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string id;
    public string name;

    public DopItemData dopItemData;
}

public class DopItemData
{
    public float price;
    public string dopName;
}
