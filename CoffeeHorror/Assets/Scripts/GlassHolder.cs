using UnityEngine;

public class GlassHolder : MonoBehaviour
{


    [Header("Настройки")]
    public float holdDistance = 0.5f; // Дистанция удержания перед игроком
    public float maxMoveForce = 10f; // Сила удержания позиции
    public float breakDistance = 0.7f; // Дистанция срыва
    public LayerMask collisionMask; // Слои для проверки столкновений

    [Header("Ссылки")]
    public Transform playerCamera;
    public Rigidbody glassRigidbody;
    public BoxCollider glassCollider;
    public bool isHolding;
    public Vector3 targetPosition;

    public GameObject raiseObject;

    [Space(10)]
    [Header("Настройки броска")]
    public float throwForce = 10f; // Сила броска
    public float upwardForce = 1f; // Вертикальная составляющая
    private void OnEnable()
    {
        AllRaycast.OnRaycastTovar += OnRaise;
        TrigCoffeeMachine.OnGetCup += RemoveData;
    }

    private void OnDisable()
    {
        AllRaycast.OnRaycastTovar -= OnRaise;
        TrigCoffeeMachine.OnGetCup -= RemoveData;
    }
    private void Update()
    {
        if (!isHolding) return;


        if (Input.GetMouseButtonDown(0))
        {
            ThrowObject();
            ReleaseGlass();
        }

        // Рассчитываем целевую позицию перед игроком
        targetPosition = playerCamera.position + playerCamera.forward * holdDistance;

        // Проверяем столкновения
        CheckCollisions();
    }

    private void FixedUpdate()
    {
        if (!isHolding) return;

        if (!glassRigidbody) return;
        if (!glassCollider) return;
        // Плавное перемещение к целевой позиции
        Vector3 forceDirection = targetPosition - glassRigidbody.position;
        float distance = forceDirection.magnitude;

        raiseObject.transform.position = targetPosition;
        // Если стаканчик слишком далеко - отпускаем
        if (distance > breakDistance)
        {
            return;
        }

        // Применяем силу удержания
        glassRigidbody.linearVelocity = forceDirection * maxMoveForce;
    }

    private void CheckCollisions()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, holdDistance + 0.2f, collisionMask))
        {
            // Если на пути есть препятствие - смещаем целевую позицию
            targetPosition = hit.point - playerCamera.forward * 0.1f;
        }
    }

    public void ReleaseGlass()
    {
        isHolding = false;

        glassCollider.isTrigger = false;
        glassRigidbody.useGravity = true;
        glassRigidbody.isKinematic = false;

        glassRigidbody = null;  
        glassCollider = null;  
        raiseObject = null;
        // Дополнительные эффекты при падении
    }

    // Вызывайте этот метод, когда игрок берет стаканчик
    public void GrabGlass()
    {
        glassCollider.isTrigger = true;
        glassRigidbody.useGravity = false;
        glassRigidbody.isKinematic = true;
        isHolding = true;
    }
    private void OnRaise(bool isRay, GameObject rayObject, string tag)
    {
        if (isRay && tag == "Item")
        {
            targetPosition = new Vector3(0f, 0f, 0f);
            raiseObject = rayObject;

            glassRigidbody = raiseObject.GetComponent<Rigidbody>();
            glassCollider = raiseObject.GetComponent<BoxCollider>();

            if (isHolding == true)
            {
                ReleaseGlass();
                return;
            }

            GrabGlass();
        }
    }
    private void RemoveData()
    {
        raiseObject = null;

        glassRigidbody = null;
        glassCollider = null;
        isHolding = false;
    }


    /// <summary>
    /// Метод для броска предмета из рук
    /// </summary>
    private void ThrowObject()
    {
        raiseObject.transform.localRotation = Quaternion.Euler(180f, 180f, 180f);
        // Получаем Rigidbody
        Rigidbody rb = raiseObject.GetComponent<Rigidbody>();
        if (rb == null) rb = raiseObject.AddComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
        // Бросаем в направлении курсора
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 throwDirection = ray.direction.normalized;

        // Добавляем немного вертикальной силы
        throwDirection.y += upwardForce;

        // Применяем силу
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        raiseObject.transform.SetParent(null);
        raiseObject = null;
    }
}