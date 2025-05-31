using UnityEngine;

/// <summary>
/// Скрпт контролирует курсор игры в любых действиях
/// </summary>
public class CursorGame : MonoBehaviour
{
    private void OnEnable()
    {
        PanelController.OnChangeCursor += CursorController;
    }

    private void OnDisable()
    {
        PanelController.OnChangeCursor -= CursorController;
    }

    private void CursorController(bool isCursor)
    {
        if(isCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Debug.Log(Cursor.lockState);
    }
}
