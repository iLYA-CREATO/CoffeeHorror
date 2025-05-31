using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCToCashier : MonoBehaviour
{
    [Header("���������")]
    public Transform cashierPoint;       // ����� ����� (���� ����)
    public Transform lookAtPoint;       // �����, ���� �������� ����� ��������� (������)
    public Transform backPoint;       // �����, ���� �������� ����
    public float stoppingDistance = 0.5f; // ��������� ���������
    public float rotationSpeed = 5f;     // �������� ��������

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

        // ��������� ������
        agent.stoppingDistance = stoppingDistance;
        agent.SetDestination(cashierPoint.position);
    }

    void Update()
    {
        if (!hasReachedDestination)
        {
            // �������� ���������� �����
            if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
            {
                hasReachedDestination = true;
                agent.isStopped = true;

                // ���������� �������� ������
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
            // ������� ������� � lookAtPoint
            if (lookAtPoint != null)
            {
                Vector3 direction = (lookAtPoint.position - transform.position).normalized;
                direction.y = 0; // ���������� ������� �� ������
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