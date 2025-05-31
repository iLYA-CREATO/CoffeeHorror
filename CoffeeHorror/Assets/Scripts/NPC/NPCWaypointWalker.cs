using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCToCashier : MonoBehaviour
{
    [Header("Настройки")]
    public Transform cashierPoint;       // Точка кассы (куда идти)
    public Transform lookAtPoint;       // Точка, куда смотреть после остановки (кассир)
    public Transform backPoint;       // Точка, куда персонаж уйдёт
    public float stoppingDistance = 0.5f; // Дистанция остановки
    public float rotationSpeed = 5f;     // Скорость поворота

    private NavMeshAgent agent;
    private Animator animator;
    private bool hasReachedDestination = false;

    [SerializeField]
    private bool onGoBack = false;

    private void OnEnable()
    {
        NPCDesire.OnGoBack += GoBack;
    }

    private void OnDisable()
    {
        NPCDesire.OnGoBack -= GoBack;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Настройка агента
        agent.stoppingDistance = stoppingDistance;
        agent.SetDestination(cashierPoint.position);
    }

    void Update()
    {
        if (!hasReachedDestination)
        {
            // Проверка достижения кассы
            if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
            {
                hasReachedDestination = true;
                agent.isStopped = true;

                // Выключение анимации ходьбы
                if (animator != null)
                    animator.SetBool("IsWalking", false);
            }
            else if (animator != null)
            {
                animator.SetBool("IsWalking", true);
            }
        }
        else
        {
            // Плавный поворот к lookAtPoint
            if (lookAtPoint != null)
            {
                Vector3 direction = (lookAtPoint.position - transform.position).normalized;
                direction.y = 0; // Игнорируем разницу по высоте
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void GoBack(bool isGoBack)
    {
        if (animator != null)
            animator.SetBool("IsWalking", true);

        agent.isStopped = false;
        onGoBack = isGoBack;
        agent.SetDestination(backPoint.position);
        hasReachedDestination = false;
    }
}