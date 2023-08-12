using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("����ũ ����!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.questManager.GetComponent<QuestController>().count++;
            Destroy(this.gameObject);
            Debug.Log("����ũ ȹ��!");

            if (GameManager.Instance.questManager.GetComponent<QuestController>().count == 3)
            {
                GameManager.Instance.questManager.GetComponent<QuestController>().successPanel.SetActive(true);
            }
        }
    }
}
