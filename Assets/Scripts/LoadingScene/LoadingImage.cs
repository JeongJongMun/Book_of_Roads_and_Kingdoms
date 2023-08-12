using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingImage : MonoBehaviour
{
    Image image;
    public Sprite[] sprites;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(LoadingView());
    }

    IEnumerator LoadingView()
    {
        yield return new WaitForSeconds(2f);
        image.sprite = sprites[1];
        yield return new WaitForSeconds(3f);
        image.sprite = sprites[2];
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Phase1");

    }
}
