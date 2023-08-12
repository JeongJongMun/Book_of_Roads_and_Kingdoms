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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GetComponent<Animator>().SetTrigger("isOpened");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goStage(int stage)
    {
        SceneManager.LoadScene(stage);

    }
}
