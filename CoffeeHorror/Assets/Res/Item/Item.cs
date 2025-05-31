using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string id;
    public string name;

    public DopItemData dopItemData;
}

[Serializable]
public class DopItemData
{
    public Sprite sprite;
    public float price;
}
