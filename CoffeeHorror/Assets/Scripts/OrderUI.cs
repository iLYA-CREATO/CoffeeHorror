using System;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public static event Action<float> OnSpawnNextNPC;
    [SerializeField]
    private List<ItemOrder> itemOrder;

    [SerializeField]
    private Transform spawnOrder;
    [SerializeField]
    private GameObject prefabOrder;


    [SerializeField]
    [Header("Список заказов на панели")]
    private List<GameObject> insOrder;
    [SerializeField]
    [Header("Список заказов с скриптом OrderData")]
    private List<OrderData> insOrderData;


    private float sumOrder = 0f;
    private void OnEnable()
    {
        NPCWaypointWalker.OnSpawnNextNPC += ClearDisplay;
        NPCWaypointWalker.OnStartOrder += GetOrderItem;
        NPCDesire.OnAddItem += OnAddItemNPC;
    }

    private void OnDisable()
    {
        NPCWaypointWalker.OnSpawnNextNPC -= ClearDisplay;
        NPCWaypointWalker.OnStartOrder -= GetOrderItem;
        NPCDesire.OnAddItem -= OnAddItemNPC;
    }

    /// <summary>
    /// NPC говорит что ему что-то дали и мы обновляем дисплей
    /// </summary>
    private void OnAddItemNPC(ThiseItem thiseItem)
    {
        for(int i = 0; i < itemOrder.Count; i++)
        {
            if (itemOrder[i].item.id ==  thiseItem.item.id)
            {
                if(itemOrder[i].value - 1 > 0)
                {
                    itemOrder[i].value--;
                }
                else if(itemOrder[i].value - 1 == 0)
                {
                    insOrderData[i].goodItem.SetActive(true);
                }
            }
        }
    }
    private void GetOrderItem(List<Item> needItemOrder)
    {
        

        ItemOrder ddd = null;

        foreach (Item itemToAdd in needItemOrder)
        {
            bool itemFound = false;

            for (int j = 0; j < itemOrder.Count; j++)
            {
                if (itemOrder[j].item.id == itemToAdd.id)
                {
                    // Если предмет уже есть в инвентаре, увеличиваем его количество
                    itemOrder[j].value++;
                    itemFound = true;
                    break; // Выходим из цикла, т.к. предмет найден и обработан
                }
            }

            // Если предмет не найден в инвентаре, добавляем его
            if (!itemFound)
            {
                ItemOrder newItemOrder = new ItemOrder();
                newItemOrder.item = itemToAdd;
                newItemOrder.value = 1;
                itemOrder.Add(newItemOrder);
            }
        }

        OrderData order;

        for (int i = 0; i < itemOrder.Count; i++)
        {
            insOrder.Add(Instantiate(prefabOrder, spawnOrder));
            order = insOrder[insOrder.Count - 1].GetComponent<OrderData>();
            insOrderData.Add(order);
            order.SetData(itemOrder[i].item.dopItemData.sprite, itemOrder[i].item.name, itemOrder[i].value, itemOrder[i].item.dopItemData.price);
        }

        for (int i = 0; i < itemOrder.Count; i++)
        {
            for (int j = 0; j < itemOrder[i].value; j++)
            {
                sumOrder += itemOrder[i].item.dopItemData.price;
            }
        }
        Debug.Log(sumOrder);
    }

    // Удали все предметы в заказе
    private void ClearDisplay()
    {
        OnSpawnNextNPC?.Invoke(sumOrder);

        for (int i = 0; i < insOrder.Count;i++)
        {
            Destroy(insOrder[i]);
        }
        //Очисти пустые
        insOrder.Clear();
        itemOrder.Clear();
        insOrderData.Clear();
        sumOrder = 0f;
    }
}
