using System;
using UnityEngine;

public class TrigCoffeeMachine : MonoBehaviour
{
    public static event Action OnGetCup;
    public static event Action OnRemoveCup; /// ��� �������� �������� �����
    public static event Action<GameObject> OnGetCupMachine;

    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<ThiseItem>()) return;

        if (other.GetComponent<ThiseItem>().item.id == "_Cup")
        {
            OnGetCup?.Invoke();
            OnGetCupMachine?.Invoke(other.gameObject);
        }
        else
        {
            Debug.Log("������ � ���� ������ �� ������");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<ThiseItem>()) return;

        if (other.GetComponent<ThiseItem>().item.id == "_Coffee")
        {
            OnRemoveCup?.Invoke();
            other.transform.SetParent(null);
            Debug.Log("�� ������� ����");
        }
    }
}
