using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KeepItem : MonoBehaviour
{
    [SerializeField]
    [Header("��� ����� �������")]
    private Transform keepPosition;
    
    [Header("��� ������ ������ ����� � ����")]
    public GameObject keepObject;

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

    [Header("��������� ������")]
    public float throwForce = 10f; // ���� ������
    public float upwardForce = 1f; // ������������ ������������

    private void Update()
    {
        RaycastHit hit;
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);
        if (keepPosition.childCount < 1)
        {
            if (Physics.Raycast(ray, out hit, distance, layerMaskRaycast, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.tag == "Item")
                {
                    if (Input.GetKeyDown(keyOptions.keyActions))
                    {
                        keepObject = hit.collider.gameObject;

                        if (hit.transform.GetComponent<Rigidbody>() != null)
                        {
                            keepObject.transform.GetComponent<Rigidbody>().useGravity = false;
                            keepObject.transform.GetComponent<Rigidbody>().isKinematic = true;

                        }
                        keepObject.transform.SetParent(keepPosition);
                        keepObject.transform.localPosition = Vector3.zero;
                    }
                }
            }
        }
        

        if (Input.GetMouseButtonDown(0)) // ��� ��� ������
        {
            if(keepObject != null)
                ThrowObject();
        }
    }
    void ThrowObject()
    {
        // �������� Rigidbody
        Rigidbody rb = keepObject.GetComponent<Rigidbody>();
        if (rb == null) rb = keepObject.AddComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
        // ������� � ����������� �������
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);
        Vector3 throwDirection = ray.direction.normalized;

        // ��������� ������� ������������ ����
        throwDirection.y += upwardForce;

        // ��������� ����
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        keepObject.transform.SetParent(null);
        keepObject = null;
    }
}
