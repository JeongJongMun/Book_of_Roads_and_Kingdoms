using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject nextStageGroup;

    public bool isOnNextStageGroup;

    public void onClick()
    {
        Debug.Log("z");
        if(!isOnNextStageGroup)
        {
            nextStageGroup.SetActive(true);
        }
        else
        {
            nextStageGroup.SetActive(false);
        }
    }
}
