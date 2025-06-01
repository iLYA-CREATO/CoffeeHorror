using UnityEngine;
using System.IO;

public class ScreenshotTaker : MonoBehaviour
{
    [Tooltip("Кнопка для создания скриншота")]
    public KeyCode screenshotKey; 

    [Tooltip("Имя файла (без расширения)")]
    public string fileName = "Screenshot";

    [Tooltip("Масштаб (1 = оригинальный размер, 2 = 2x увеличение и т.д.)")]
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