using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject nextStageGroup;
    public GameObject settingPanel;

    public void OnClickStartBtn()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        nextStageGroup.SetActive(true);
    }
    public void OnClickSettingBtn()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    public void OnClickExitBtn()
    {
        Application.Quit();
    }
}
