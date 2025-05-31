using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneStart : MonoBehaviour
{
    [SerializeField]
    private int count;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private int clientValue;
    private void OnEnable()
    {
        NPCWaypointWalker.OnSpawnNextNPC += AddCount;
    }

    private void OnDisable ()
    {
        NPCWaypointWalker.OnSpawnNextNPC -= AddCount;
    }

    private void AddCount()
    {
        count++;


        if(count == clientValue)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadSceneMode()
    {
        SceneManager.LoadScene(sceneName);
    }
}
