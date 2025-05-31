using UnityEngine;

public class MoneyTextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject moneyTextPrefab;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform spawnPositionText;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnHeight = 1f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpawnMoneyText(1);
        }
    }
    public void SpawnMoneyText(int amount)
    {
        // Вычисляем позицию перед камерой
        Vector3 spawnPosition = playerCamera.position +
                              playerCamera.forward * spawnDistance +
                              Vector3.up * spawnHeight;

        // Создаем текст
        GameObject textInstance = Instantiate(moneyTextPrefab, spawnPosition, Quaternion.identity);
        textInstance.transform.LookAt(2 * textInstance.transform.position - playerCamera.position);

        // Настраиваем текст
        MoneyTextEffect effect = textInstance.GetComponent<MoneyTextEffect>();
        effect.SetText(amount);
        textInstance.transform.SetParent(spawnPositionText);
    }
}