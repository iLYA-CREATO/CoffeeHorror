using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    #region Данные
    public static event Action<GameObject> OnOpenESC;
    public static event Action<GameObject> OnOpenDopPanelESC;
    public static event Action<GameObject> OnClouseDopPanelESC;

    [SerializeField]  
    private KeyOptions inputKeyManager;

    [SerializeField]
    private GameObject escMenu;

    #endregion
    private void LateUpdate()
    {
        if (inputKeyManager)
        {
            if (Input.GetKeyDown(inputKeyManager.openEsc))
            {
                OnOpenESC?.Invoke(escMenu);
            }
        }
        else
            Debug.LogError("Пустой KeyOptions");
        
    }

    /// <summary>
    ///  Закрывает основное окно паузы
    /// </summary>
    /// <param name="panel"></param>
    public void ClouseBasePanelESC(GameObject panel)
    {
        OnOpenESC?.Invoke(panel);
    }

    /// <summary>
    /// Открытие дополнительного окно
    /// </summary>
    /// <param name="panel"></param>
    public void OpenDopPanelESC(GameObject panel)
    {
        OnOpenDopPanelESC?.Invoke(panel);
    }

    /// <summary>
    /// Закрывает доп окна
    /// </summary>
    /// <param name="panel"></param>
    public void ClouseDopPanelESC(GameObject panel)
    {
        OnClouseDopPanelESC?.Invoke(panel);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Перезапуск сцены
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
