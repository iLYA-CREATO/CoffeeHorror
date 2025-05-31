using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private bool isOpenDoor;

    public void OpenClouseDoor()
    {
        if (isOpenDoor)
            animator.Play("ClouseDoor");
        else 
            animator.Play("OpenDoor");
        isOpenDoor = !isOpenDoor;
    }
}
