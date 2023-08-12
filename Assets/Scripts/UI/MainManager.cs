using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject nextStageGroup;

    public void OnClickStartBtn()
    {
        nextStageGroup.SetActive(true);
    }
}
