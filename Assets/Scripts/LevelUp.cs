using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    public int maxLevelNum;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    void Update()
    {

    }
    public void Show()
    {
        //AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
        //AudioManager.Instance.EffectBgm(true);
        rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        Next();
    }

    public void Hide()
    {
        //AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
        //AudioManager.Instance.EffectBgm(false);
        rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();

    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    public void Next()
    {
        bool[] isMaxLevel = new bool[4];

        foreach (Item item in items)
        {

            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].level == items[i].data.damages.Length)
            {
                isMaxLevel[i] = true;
            }
        }

        int[] ran = new int[2];
        while (true)
        {
            if (maxLevelNum <= 2)
            {
                ran[0] = Random.Range(0, items.Length - 1);
                ran[1] = Random.Range(0, items.Length - 1);
            }
            else if (maxLevelNum == 3)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (!isMaxLevel[i])
                    {
                        ran[0] = i;
                        ran[1] = i;
                        break;
                    }
                }
            }


            if ((ran[0] != ran[1] && !isMaxLevel[ran[0]] && !isMaxLevel[ran[1]]) || maxLevelNum >= 3)
                break;
        }


        for (int i = 0; i < ran.Length; i++)
        {
            if (maxLevelNum <= 2)
            {
                items[ran[i]].gameObject.SetActive(true);
            }
            if (maxLevelNum == 3)
            {
                items[ran[i]].gameObject.SetActive(true);
                items[4].gameObject.SetActive(true);
            }
            if (maxLevelNum == 4)
            {
                items[4].gameObject.SetActive(true);
            }

        }

    }

    public void RandomShow()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.SetActive(false);
        }
        int[] ran = new int[3];
        for(int i = 0; i < ran.Length; i++)
        {
            ran[i] = Random.Range(0, items.Length);
        }
        for (int i = 0; i < ran.Length; i++)
        {
            items[ran[i]].gameObject.SetActive(true);
        }
    }
}
