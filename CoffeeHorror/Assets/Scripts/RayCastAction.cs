using NUnit.Framework;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RayCastAction : MonoBehaviour
{
    [SerializeField]
    [Header("��� �������")]
    private KeyOptions keyOptions;
    [SerializeField]
    [Header("������ ������")]
    private Camera cameraPlayer;
    [SerializeField]
    [Header("� ����� ����� ��������")]
    private LayerMask layerMaskRaycast;
    [SerializeField]
    [Header("��������� ��������������")]
    private float distance;


    [SerializeField]
    [Header("�������� ��� �������� �����")]
    private float timedeleyOpenDoor;

    private bool isOpen;
    private Door door;

    [SerializeField]
    private TextMeshProUGUI textAction;// �������� � ���������
    private void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        RaycastHit hit;
        Ray ray = cameraPlayer.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out hit, distance, layerMaskRaycast, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.tag == "Door")
            {
                textAction.text = "�����������������";
                textAction.gameObject.SetActive(true);

                door = hit.collider.GetComponent<Door>();
                if (Input.GetKeyDown(keyOptions.keyActions))
                {
                    if (isOpen == false)
                        StartCoroutine(OpenerDoor());
                }
            }
            else
            {
                textAction.gameObject.SetActive(false);
            }
        }
        else
        {
            textAction.gameObject.SetActive(false);
        }
    }


    private IEnumerator OpenerDoor()
    {
        door.OpenClouseDoor();
        isOpen = true;
        yield return new WaitForSeconds(timedeleyOpenDoor);
        isOpen = false;
        yield break;
    }
}
