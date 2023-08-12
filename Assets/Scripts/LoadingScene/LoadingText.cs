using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingText : MonoBehaviour
{
    TMP_Text text;

    public float timer;
    public float effectTime;
    public int index;
    public string[] contents;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= effectTime)
        {
            text.text = contents[index];

            if(index == 2)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            timer = 0;
        }
    }
}
