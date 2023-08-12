using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("¸¶½ºÅ© ½ºÆù!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.questManager.GetComponent<QuestController>().count++;
            Destroy(this.gameObject);
            Debug.Log("¸¶½ºÅ© È¹µæ!");

            if (GameManager.Instance.questManager.GetComponent<QuestController>().count == 3)
            {
                GameManager.Instance.questManager.GetComponent<QuestController>().successPanel.SetActive(true);
            }
        }
    }
}
