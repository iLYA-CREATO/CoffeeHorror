using System;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class NPCDesire : MonoBehaviour
{
    public static event Action<ThiseItem> OnAddItem;
    [SerializeField]
    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioClip audioClip;
    public static event Action<bool> OnGoBack;
    [SerializeField]
    private NPCNeedItem _NPCNeadItemm;

    private void OnTriggerEnter(Collider other)
    {
        ThiseItem thiseItem;
        if (other.tag == "Item")
        {
            thiseItem = other.GetComponent<ThiseItem>();

            for(int i=0; i < _NPCNeadItemm.needItem.Count; i++)
            {
                if (thiseItem.item == _NPCNeadItemm.needItem[i])
                {
                    OnAddItem?.Invoke(thiseItem);
                    m_AudioSource.PlayOneShot(audioClip);
                    Debug.Log("NPC говорит это то что я хотел");
                    _NPCNeadItemm.needItem.RemoveAt(i);
                    Destroy(other.gameObject);
                    break;
                }
            }

            if (_NPCNeadItemm.needItem.Count == 0)
            {
                OnGoBack?.Invoke(true);
                Debug.Log("Я всё взял пойду домой");
            }
        }
        else
        {
            Debug.Log("Это не то что мне нужно");
        }
    }
}
