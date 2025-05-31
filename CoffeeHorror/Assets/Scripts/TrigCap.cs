using System;
using UnityEngine;

/// <summary>
/// ������ ��� ������� ������ �������
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

        if (other.GetComponent<ThiseItem>().item.id == "_Cap")
        {
            if(coffee.activeSelf) // �������� � ������ ������ �� �� ���� � ������
            {
                Debug.Log("� ���� �� ����");
                if (!cap.activeSelf)
                {
                    Destroy(other.gameObject);
                    cap.SetActive(true);
                }
            }
        }
    }

}
