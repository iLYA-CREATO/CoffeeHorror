using System.Collections;
using UnityEngine;

public class CoffeMachine : MonoBehaviour
{
    [SerializeField]
    private Transform capPosition;

    [SerializeField]
    private Item item;
    [SerializeField]
    private ThiseItem itemGameObject;
    [SerializeField]
    private Coffee coffee;


    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;


    [SerializeField]
    [Header("Как ставится стакан")]
    private Transform positionCup;

    [SerializeField]
    [Header("Для запуска анимации коффе")]
    private Animator animatorCoffee;
    private void OnEnable()
    {
        TrigCoffeeMachine.OnGetCupMachine += StartCoffee;
        TrigCoffeeMachine.OnRemoveCup += RemoveCoffee;
    }

    private void OnDisable()
    {
        TrigCoffeeMachine.OnGetCupMachine -= StartCoffee;
        TrigCoffeeMachine.OnRemoveCup -= RemoveCoffee;
    }

    private void StartCoffee(GameObject gameObject)
    {
        if (itemGameObject == null)
        {
            itemGameObject = gameObject.GetComponent<ThiseItem>();
            coffee = gameObject.GetComponent<Coffee>();
            
            gameObject.transform.SetParent(positionCup);

            gameObject.transform.position = positionCup.position;
            gameObject.transform.rotation = positionCup.rotation;

            StartCoroutine(StartCoffeeMachine());
            animatorCoffee.Play("StartCoffee");
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Ошибка тут уже стаит чашка с кофе");
        }
    }

    private IEnumerator StartCoffeeMachine()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        Debug.Log("Кофе готово");
        itemGameObject.item = item;
        coffee.CofffeeObject.SetActive(true);
    }


    private void RemoveCoffee()
    {
        itemGameObject = null;
        coffee = null;
    }
}
