using System;
using TMPro;
using UnityEngine;

public class AllRaycast : MonoBehaviour
{
    public static event Action<bool, GameObject, string> OnRaycastTovar;
    [SerializeField]
    private TextMeshProUGUI textAction;// Картинка с действием

    public LayerMask targetLayer;
    public Lamp lamp;
    private void LateUpdate()
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        // Создаем луч из камеры в направлении взгляда
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        // Выполняем raycast
        if (Physics.Raycast(ray, out hit, 2, targetLayer))
        {
            if (hit.collider.tag != "Untagged")
            {
                if (hit.collider.tag == "Item")
                {
                    textAction.text = "Взять";
                    textAction.gameObject.SetActive(true);
                }
                else if (hit.collider.tag == "Light")
                {
                    textAction.text = "Взаимодействовать";
                    textAction.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        lamp.SwitchLight();
                    }
                }
                else
                {
                    textAction.gameObject.SetActive(false);
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnRaycastTovar?.Invoke(true, hit.collider.gameObject, hit.collider.tag);
                }
            }
        }
        else
        {
            textAction.gameObject.SetActive(false);
        }
    }
}
