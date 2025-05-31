using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private float wallet;

    [SerializeField]
    private TextMeshProUGUI textWallet;

    private void OnEnable()
    {
        OrderUI.OnSpawnNextNPC += AddMoney;
    }

    private void OnDisable()
    {
        OrderUI.OnSpawnNextNPC -= AddMoney;
    }
    private void AddMoney(float amount)
    {
        wallet += amount;
        textWallet.text = wallet.ToString();
    }
}
