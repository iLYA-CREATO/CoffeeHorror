using Unity.VisualScripting;
using UnityEngine;

public class TrigNPCDoor : MonoBehaviour
{
    [SerializeField]
    private Door door;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="NPC")
        {
            door.OpenClouseDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            door.OpenClouseDoor();
        }
    }
}
