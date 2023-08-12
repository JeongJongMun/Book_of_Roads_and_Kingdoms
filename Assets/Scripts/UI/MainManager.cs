using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject nextStageGroup;

    public void OnClickStartBtn()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        nextStageGroup.SetActive(true);
    }
}
