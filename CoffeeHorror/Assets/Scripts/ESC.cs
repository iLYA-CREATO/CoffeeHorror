using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    #region ������
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
            Debug.LogError("������ KeyOptions");
        
    }

    /// <summary>
    ///  ��������� �������� ���� �����
    /// </summary>
    /// <param name="panel"></param>
    public void ClouseBasePanelESC(GameObject panel)
    {
        OnOpenESC?.Invoke(panel);
    }

    /// <summary>
    /// �������� ��������������� ����
    /// </summary>
    /// <param name="panel"></param>
    public void OpenDopPanelESC(GameObject panel)
    {
        OnOpenDopPanelESC?.Invoke(panel);
    }

    /// <summary>
    /// ��������� ��� ����
    /// </summary>
    /// <param name="panel"></param>
    public void ClouseDopPanelESC(GameObject panel)
    {
        OnClouseDopPanelESC?.Invoke(panel);
    }

    /// <summary>
    /// ����� �� ����
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
    /// ���������� �����
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
