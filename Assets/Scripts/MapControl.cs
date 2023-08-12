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
            GetComponent<Animator>().SetTrigger("isOpened");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goStage(int stage)
    {
        if (stage == 2)
            return;
        SceneManager.LoadScene(stage);

    }
}
