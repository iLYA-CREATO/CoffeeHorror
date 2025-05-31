using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private int frameCount = 0;
    private float elapsedTime = 0f;
    private float fps = 0f;
    private float updateRate = 4f; // 4 ���������� � �������

    void Awake()
    {
        Application.targetFrameRate = 300; // �����������
    }

    void Update()
    {
        frameCount++;
        elapsedTime += Time.unscaledDeltaTime;

        if (elapsedTime > 1f / updateRate)
        {
            fps = frameCount / elapsedTime;
            text.text = $"FPS: {Mathf.Round(fps)}";
            frameCount = 0;
            elapsedTime = 0f;
        }
    }
}