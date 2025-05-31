using System.Collections;
using UnityEngine;

public class LampStreetHorror : MonoBehaviour
{
    [SerializeField]
    private GameObject light;

    [SerializeField]
    private bool isActive;
    [SerializeField]
    private float time;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip audioClip;
    private void Start()
    {
        if(isActive)
        {
            StartCoroutine(SwitcherLight());
        }
    }

    private IEnumerator SwitcherLight()
    {
        while (true) // Бесконечный цикл
        {
            yield return new WaitForSecondsRealtime(time);
            light.SetActive(!light.activeSelf);
            source.PlayOneShot(audioClip);
        }
    }
}
