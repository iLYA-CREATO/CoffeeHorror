using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderData : MonoBehaviour
{
    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textPrice;

    public GameObject goodItem;

    public void  SetData(Sprite icon, string TextName, int TextValue, float TextPrice)
    {
        imageIcon.sprite = icon;
        textName.text = TextName;
        textValue.text = TextValue + "��";
        textPrice.text = TextPrice + "$";
    }

    public void GoodItem()
    {
        goodItem.SetActive(true);
    }
}
