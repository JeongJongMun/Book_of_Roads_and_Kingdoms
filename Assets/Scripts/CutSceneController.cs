using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public int sequence;
    bool isAniming;
    public GameObject[] cutObj;
    public Animator[] anim;

    void Awake()
    {
        for(int i = 0; i < cutObj.Length; i++)
        {
            anim[i] = cutObj[i].GetComponent<Animator>();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0) && !isAniming)
        {
            switch(sequence)
            {
                case 0:
                    StartCoroutine(Animing());
                    anim[0].SetTrigger("doShow");
                    sequence++;
                    break;
                case 1:
                    StartCoroutine(Animing());
                    anim[1].SetTrigger("doShow");
                    sequence++;
                    break;
                case 2:
                    StartCoroutine(Animing());
                    cutObj[0].SetActive(false);
                    cutObj[1].SetActive(false);
                    anim[2].SetTrigger("doShow");
                    sequence++;
                    break;
                case 3:
                    StartCoroutine(Animing());
                    anim[3].SetTrigger("doShow");
                    sequence++;
                    break;
                case 4:
                    StartCoroutine(Animing());
                    anim[4].SetTrigger("doShow");
                    sequence++;
                    break;
                case 5:
                    StartCoroutine(Animing());
                    cutObj[2].SetActive(false);
                    cutObj[3].SetActive(false);
                    cutObj[4].SetActive(false);
                    anim[5].SetTrigger("doShow");
                    sequence++;
                    break;
                case 6:
                    StartCoroutine(Animing());
                    anim[6].SetTrigger("doShow");
                    sequence++;
                    break;
                case 7:
                    StartCoroutine(Animing());
                    anim[7].SetTrigger("doShow");
                    sequence++;
                    break;
                case 8:
                    StartCoroutine(Animing());
                    cutObj[5].SetActive(false);
                    cutObj[6].SetActive(false);
                    cutObj[7].SetActive(false);
                    anim[8].SetTrigger("doShow");
                    sequence++;
                    break;
                case 9:
                    StartCoroutine(Animing());
                    anim[9].SetTrigger("doShow");
                    sequence++;
                    break;
                case 10:
                    SceneManager.LoadScene("Phase1");
                    break;
            }
        }
    }
    IEnumerator Animing()
    {
        isAniming = true;
        yield return new WaitForSeconds(1f);
        isAniming = false;
    }
}
