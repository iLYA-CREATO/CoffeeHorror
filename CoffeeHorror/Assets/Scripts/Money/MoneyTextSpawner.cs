using UnityEngine;

public class MoneyTextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject moneyTextPrefab;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform spawnPositionText;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnHeight = 1f;

    private void OnEnable()
    {
        OrderUI.OnSpawnNextNPC += SpawnMoneyText;
    }

    private void OnDisable()
    {
        OrderUI.OnSpawnNextNPC -= SpawnMoneyText;
    }
    public void SpawnMoneyText(float amount)
    {
        // ��������� ������� ����� �������
        Vector3 spawnPosition = playerCamera.position +
                              playerCamera.forward * spawnDistance +
                              Vector3.up * spawnHeight;

        // ������� �����
        GameObject textInstance = Instantiate(moneyTextPrefab, spawnPosition, Quaternion.identity);
        textInstance.transform.LookAt(2 * textInstance.transform.position - playerCamera.position);

        // ����������� �����
        MoneyTextEffect effect = textInstance.GetComponent<MoneyTextEffect>();
        effect.SetText(amount);
        textInstance.transform.SetParent(spawnPositionText);
    }
}