using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWaypointWalker : MonoBehaviour
{
    public static event Action OnSpawnNextNPC;
    public static event Action<List<Item>> OnStartOrder;
    [Header("���������")]
    public Transform cashierPoint;          // ����� ����� (���� ����)
    public Transform lookAtPoint;           // �����, ���� �������� ����� ��������� (������)
    public Transform backPoint;             // �����, ���� �������� ����
    public float stoppingDistance = 0.5f;   // ��������� ���������
    public float rotationSpeed = 5f;        // �������� ��������

    private NavMeshAgent agent;
    private Animator animator;
    private bool hasReachedDestination = false;

    [SerializeField]
    private bool onGoBack = false;


    private bool isPlayAudio;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clip;
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

            // ���������, ������ �� NPC ����
            if (agent.remainingDistance <= stoppingDistance && !agent.pathPending && onGoBack == true)
            {
                Destroy(gameObject);
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

                if(isPlayAudio == false)
                {
                    OnStartOrder?.Invoke(gameObject.GetComponent<NPCNeedItem>().needItem);
                    audioSource.PlayOneShot(clip);
                    isPlayAudio = true;
                }
            }
        }
    }

    private void GoBack(bool isGoBack)
    {
        OnSpawnNextNPC?.Invoke();
        if (animator != null)
            animator.SetBool("IsWalking", true);

        agent.isStopped = false;
        onGoBack = isGoBack;
        agent.SetDestination(backPoint.position);
        hasReachedDestination = false;
    }
}