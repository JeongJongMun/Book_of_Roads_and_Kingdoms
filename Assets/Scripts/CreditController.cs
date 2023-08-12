using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour
{
    RectTransform rect;

    public float speed;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.anchoredPosition += Time.deltaTime * speed * Vector2.up;
        if(rect.anchoredPosition.y > 3000)
        {
            gameObject.SetActive(false);
            rect.anchoredPosition = Vector2.zero;
        }
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
        rect.anchoredPosition = Vector2.zero;
    }
}
