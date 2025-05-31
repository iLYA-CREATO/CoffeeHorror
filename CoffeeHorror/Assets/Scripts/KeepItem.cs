using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KeepItem : MonoBehaviour
{
    [SerializeField]
    [Header("Где будет держать")]
    private Transform keepPosition;
    
    [Header("Что сейчас держит игрок в руке")]
    public GameObject keepObject;

    [SerializeField]
    [Header("Все клавиши")]
    private KeyOptions keyOptions;
    [SerializeField]
    [Header("Камера игрока")]
    private Camera cameraPlayer;
    [SerializeField]
    [Header("С каким слоем работаем")]
    private LayerMask layerMaskRaycast;
    [SerializeField]
    [Header("Дистанция взаимодействия")]
    private float distance;

    [Header("Настройки броска")]
    public float throwForce = 10f; // Сила броска
    public float upwardForce = 1f; // Вертикальная составляющая

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
        

        if (Input.GetMouseButtonDown(0)) // ЛКМ для броска
        {
            if(keepObject != null)
                ThrowObject();
        }
    }
    void ThrowObject()
    {
        // Получаем Rigidbody
        Rigidbody rb = keepObject.GetComponent<Rigidbody>();
        if (rb == null) rb = keepObject.AddComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
        // Бросаем в направлении курсора
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);
        Vector3 throwDirection = ray.direction.normalized;

        // Добавляем немного вертикальной силы
        throwDirection.y += upwardForce;

        // Применяем силу
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        keepObject.transform.SetParent(null);
        keepObject = null;
    }
}
