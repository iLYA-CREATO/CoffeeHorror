using System;
using UnityEngine;

/// <summary>
/// Скрипт для тригера крышки стакана
/// </summary>
public class TrigCap : MonoBehaviour
{
    public static event Action<GameObject> OnRemoveCap;
    [SerializeField]
    private Transform capPosition;

    [SerializeField]
    private GameObject cap;
    [SerializeField]
    private GameObject coffee;
    [SerializeField]
    private ThiseItem thiseItem;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetComponent<ThiseItem>())
        {
            return;
        }

        if (thiseItem.item.id == "_Coffee")
        {
            if(coffee.activeSelf) // Проверим с начало налили ли мы кофе в стакан
            {
                Debug.Log("Я сяду на кофе");
                if (!cap.activeSelf)
                {
                    Destroy(other.gameObject);
                    cap.SetActive(true);
                }
            }
        }
    }

}
