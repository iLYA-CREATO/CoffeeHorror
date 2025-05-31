using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static event Action<bool> OnChangeCursor;

    [SerializeField, Header("Список открытых панелей")]
    private List<GameObject> panelsIsOpen;
    [SerializeField, Header("Список доп открытых панелей")]
    private List<GameObject> panelsInfoIsOpen;

    // public List<GameObject> basePanelTab; // Потом надо сделать чтобы панельки проще через список открывались
    public GameObject basePanelPlayer;
    public GameObject basePanelPlayerInventory;

    [SerializeField, Header("Конторллер песонажа")]
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
 /*   // Метод реагирует на действия инвенторя и распределяет

    /// <summary>
    /// Закрывает все окна
    /// </summary>
    private void OnClouseAllPanel()
    {
        for(int i = 0; i < panelsIsOpen.Count; i++)
        {
            panelsIsOpen[i].SetActive(false);
        }
        panelsIsOpen.Clear();
        OnChangeCursor?.Invoke(true);
    }
    private void OpenBuildPanel(GameObject panelBuild, bool state)
    {
        if (panelsIsOpen.Count > 0)
        {
            if (panelsIsOpen[0].name == panelBuild.name)
            {
                panelBuild.SetActive(false);
                panelsIsOpen.Clear();
                OnChangeCursor?.Invoke(true);
                return;
            }
        }

        if (panelsIsOpen.Count > 0) return;

        panelBuild.SetActive(state);
        panelsIsOpen.Add(panelBuild);
        OnChangeCursor?.Invoke(false);
    }
    private void IsInventory(GameObject panelInventory, bool state)
    {
        CheckOpenPanel();

        if (state == true)
        {
            OnChangeCursor?.Invoke(false);

            basePanelPlayer.SetActive(state);
            basePanelPlayerInventory.SetActive(state);

            panelsIsOpen.Add(basePanelPlayer);
            panelsIsOpen.Add(basePanelPlayerInventory);
        }

        if (state == false)
        {
            OnChangeCursor?.Invoke(true);

            basePanelPlayer.SetActive(state);
            basePanelPlayerInventory.SetActive(state);

            for (int i = 0; i < panelsIsOpen.Count; i++)
            {
                panelsIsOpen[i].SetActive(false);
            }

            panelsIsOpen.Clear();
        }
    }

    public void IsOppenNewChildPanel(GameObject panelInventory)
    {
        CheckOpenPanel();

        for (int i = 0; i < panelsIsOpen.Count; i++)
        {
            panelsIsOpen[i].SetActive(false);
        }
        panelsIsOpen.Add(basePanelPlayer);
        panelsIsOpen.Add(panelInventory);
        panelInventory.SetActive(true);
        basePanelPlayer.SetActive(true);
    }

    public void IsOppenNewPanel(GameObject panelInventory, bool state)
    {
        CheckOpenPanel();
        OnChangeCursor?.Invoke(false);
        LockerControllers();
        for (int i = 0; i < panelsIsOpen.Count; i++)
        {
            panelsIsOpen[i].SetActive(false);
        }
        panelInventory.SetActive(state);
        basePanelPlayer.SetActive(state);

        panelsIsOpen.Add(basePanelPlayer);
        panelsIsOpen.Add(panelInventory);
    }

    private void IsInventoryChild(GameObject panelChild, bool state)
    {
        CheckOpenPanel();

        if (state == true)
        {
            panelsIsOpen.Add(panelChild);
            panelsIsOpen.Add(basePanelPlayerInventory);

            OnChangeCursor?.Invoke(false);

            basePanelPlayer.SetActive(state);
            basePanelPlayerInventory.SetActive(state);
            panelChild.SetActive(state);
        }

        if (state == false)
        {
            panelsIsOpen.Clear();
            OnChangeCursor?.Invoke(true);
            basePanelPlayerInventory.SetActive(state);
            basePanelPlayer.SetActive(state);
        }
    }*/

    /// <summary>
    /// Метод проверяет перед открытием если что-то открыто то он закрывает 
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
    /// При нажатии ESC
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

/*    #region Стандартные методы на открытие закрытие панелей
    private void OpenDopPanel(GameObject panel, bool cursorState)
    {
        panel.SetActive(true);
        panelsInfoIsOpen.Add(panel);
        OnChangeCursor?.Invoke(cursorState);
    }
    private void ClouseDopPanel(GameObject panel, bool cursorState)
    {
        for (int i = 0; i < panelsInfoIsOpen.Count;i++)
        {
            if (panelsInfoIsOpen[i] == panel)
            {
                panelsInfoIsOpen[i].SetActive(false);
                panelsInfoIsOpen.RemoveAt(i);
            }
        }
        OnChangeCursor?.Invoke(cursorState);
    }
    #endregion*/

    private void ClousePanel(int i)
    {
        panelsIsOpen[i].SetActive(false);
    }
    /// <summary>
    /// Метод блокирует управление персонажам пока у игрок открыт интерфейс
    /// </summary>
    private void LockerControllers()
    {
        // Проверим есть ли активные окна или открыто базовое окно
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
