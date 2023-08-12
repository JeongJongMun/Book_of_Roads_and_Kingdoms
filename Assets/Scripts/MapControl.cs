using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapControl : MonoBehaviour
{
    public GameObject lineUp;
    public GameObject lineDown;

    public int id;

    void Awake()
    {
        try
        {
            GetComponent<Animator>().SetTrigger("isOpened");
        }
        catch
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goStage(int stage)
    {
        //AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        Debug.Log("z");
        SceneManager.LoadScene(stage);

    }
}
