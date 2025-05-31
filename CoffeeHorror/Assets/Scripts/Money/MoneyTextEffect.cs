using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyTextEffect : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private float lifeTime = 2f;

    private TextMeshProUGUI moneyText;
    private float currentAlpha = 1f;
    private float timer = 0f;

    private void Awake()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Движение вверх
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Плавное исчезновение
        currentAlpha = Mathf.Lerp(1f, 0f, timer / lifeTime);
        moneyText.color = new Color(moneyText.color.r, moneyText.color.g, moneyText.color.b, currentAlpha);

        // Уничтожение после завершения времени жизни
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(float amount)
    {
        moneyText.text = (amount > 0 ? "+" : "") + amount + "$";
        moneyText.color = amount > 0 ? Color.green : Color.red;
    }
}