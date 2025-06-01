using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static event Action<bool> OnChangeCursor;

    [SerializeField, Header("������ �������� �������")]
    private List<GameObject> panelsIsOpen;
    [SerializeField, Header("������ ��� �������� �������")]
    private List<GameObject> panelsInfoIsOpen;

    // public List<GameObject> basePanelTab; // ����� ���� ������� ����� �������� ����� ����� ������ �����������
    public GameObject basePanelPlayer;
    public GameObject basePanelPlayerInventory;

    [SerializeField, Header("���������� ��������")]
    private PlayerController playerController;

    private void Start()
    {
        LockerControllers();
    }
    private void OnEnable()
    {
        ESC.OnOpenESC += CheckOpenPanel;
        ESC.OnOpenDopPanelESC += OpenDopPanelESC;
        ESC.OnClouseDopPanelESC += ClouseDopPanelESC;
    }

    private void OnDisable()
    {
        ESC.OnOpenESC -= CheckOpenPanel;
        ESC.OnOpenDopPanelESC -= OpenDopPanelESC;
        ESC.OnClouseDopPanelESC -= ClouseDopPanelESC;
    }

    /// <summary>
    /// ����� ��������� ����� ��������� ���� ���-�� ������� �� �� ��������� 
    /// </summary>
    private void CheckOpenPanel()
    {
        if (panelsIsOpen.Count > 0)
        {
            for (int i = 0; i < panelsIsOpen.Count; i++)
            {
                ClousePanel(i);
            }
            panelsIsOpen.Clear();
        }
    }

    #region
    /// <summary>
    /// ��� ������� ESC
    /// </summary>
    /// <param name="panelESC"></param>
    private void CheckOpenPanel(GameObject panelESC)
    {
        if (panelsIsOpen.Count > 0)
        {
            for (int i = 0; i < panelsIsOpen.Count; i++)
            {
                ClousePanel(i);
            }
            panelsIsOpen.Clear();
        }
        else if(panelsIsOpen.Count == 0)
        {
            panelESC.SetActive(true);
            panelsIsOpen.Add(panelESC);
        }
        LockerControllers();
    }

    private void OpenDopPanelESC(GameObject panelESC)
    {
        panelESC.SetActive(true);
        panelsIsOpen.Add(panelESC);
    }

    private void ClouseDopPanelESC(GameObject panelESC)
    {
        panelESC.SetActive(true);
        for(int i = 0; i < panelsIsOpen.Count; i++)
        {
            if (panelsIsOpen[i] == panelESC)
            {
                panelsIsOpen[i].SetActive(false);
                panelsIsOpen.Remove(panelsIsOpen[i]); 
            }
        }
    }
    #endregion
    private void ClousePanel(int i)
    {
        panelsIsOpen[i].SetActive(false);
    }
    /// <summary>
    /// ����� ��������� ���������� ���������� ���� � ����� ������ ���������
    /// </summary>
    private void LockerControllers()
    {
        // �������� ���� �� �������� ���� ��� ������� ������� ����
        if (panelsIsOpen.Count > 0)
        {
            playerController.LockController(true);
            OnChangeCursor?.Invoke(false);
        }
        else
        {
            playerController.LockController(false);
            OnChangeCursor?.Invoke(true);
        }
    }
}
