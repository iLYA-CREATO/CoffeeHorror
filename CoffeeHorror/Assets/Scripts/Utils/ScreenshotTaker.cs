using UnityEngine;
using System.IO;

public class ScreenshotTaker : MonoBehaviour
{
    [Tooltip("������ ��� �������� ���������")]
    public KeyCode screenshotKey; 

    [Tooltip("��� ����� (��� ����������)")]
    public string fileName = "Screenshot";

    [Tooltip("������� (1 = ������������ ������, 2 = 2x ���������� � �.�.)")]
    public int scale = 1;

    void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fullPath = Path.Combine(desktopPath, $"{fileName}_{timestamp}.png");

        ScreenCapture.CaptureScreenshot(fullPath, scale);
    }
}