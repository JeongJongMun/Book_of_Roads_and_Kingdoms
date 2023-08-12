using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuccessPanel : MonoBehaviour
{
    public GameObject before;
    public Image fadeImage;
    public GameObject stageClearBtn;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FadeInOut", 1.5f);
    }

    // 트릭 성공 시 이펙트

    public void FadeInOut()
    {
        before.SetActive(false);
        stageClearBtn.SetActive(true);
        StartCoroutine(DoFadeInOut());
    }
    private IEnumerator DoFadeInOut()
    {
        yield return StartCoroutine(Fade(0, 1));

        yield return StartCoroutine(Fade(1, 0));

    }
    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / 0.1f;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;
            yield return null;
        }
    }

    public void OnClickStageClearButton()
    {
        SceneManager.LoadScene("Phase1ToPhase2");
    }
}
