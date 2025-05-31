using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    [SerializeField]
    private int maxValueOrder;

    [SerializeField]
    private List<GameObject> prefabNPC;

    [SerializeField]
    private List<Item> allItemGame;

    [SerializeField]
    private List<Item> generateItem;

    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private GameObject NPCInstance;


    [SerializeField]
    public Transform cashierPoint;          // “очка кассы (куда идти)
    [SerializeField]
    public Transform lookAtPoint;           // “очка, куда смотреть после остановки (кассир)
    [SerializeField]
    public Transform backPoint;             // куда уходить

    private void OnEnable()
    {
        NPCWaypointWalker.OnSpawnNextNPC += SpawnNextNPC;
    }

    private void OnDisable()
    {
        NPCWaypointWalker.OnSpawnNextNPC -= SpawnNextNPC;
    }
    private void Start()
    {
        SpawnNextNPC();
    }

    private int GenerateNumber(int min, int max)
    {
        int rnd = Random.Range(min, max);
        return rnd;
    }


    private void SpawnNextNPC()
    {
        NPCInstance = Instantiate(prefabNPC[(GenerateNumber(0, prefabNPC.Count))],
            spawnPoints[(GenerateNumber(0, spawnPoints.Count))]);

        NPCWaypointWalker nPCToCashier;
        nPCToCashier = NPCInstance.gameObject.GetComponent<NPCWaypointWalker>();

        int rnd = Random.Range(1,maxValueOrder);
        for(int i = 0; i < rnd; i++)
        {
            generateItem.Add(allItemGame[GenerateNumber(0, allItemGame.Count)]);
        }
        nPCToCashier.cashierPoint = cashierPoint;
        nPCToCashier.backPoint = backPoint;
        nPCToCashier.lookAtPoint = lookAtPoint;

        NPCInstance.gameObject.GetComponent<NPCNeedItem>().needItem = new List<Item>(generateItem);

        generateItem.Clear();
    }
}
