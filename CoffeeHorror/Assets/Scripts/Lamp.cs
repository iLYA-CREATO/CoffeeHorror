using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    private Material materialLight;

    [SerializeField]
    private bool isActivLamp;

    [SerializeField]
    private List<GameObject> lights;
    public void SwitchLight()
    {
        isActivLamp = !isActivLamp;

        if (isActivLamp)
        {
            materialLight.EnableKeyword("_EMISSION");
        }
        else
        {
            materialLight.DisableKeyword("_EMISSION");
        }

        for(int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(isActivLamp);
        }
    }
}
