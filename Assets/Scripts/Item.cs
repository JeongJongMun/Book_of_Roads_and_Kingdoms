using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;

    Image icon;
    public TMP_Text levelText;
    public TMP_Text nameText;
    public TMP_Text descText;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        //텍스트 순서 바꾸면 안됨!
        levelText = texts[0];
        nameText = texts[1];
        descText = texts[2];

        nameText.text = data.itemName;
    }

    // Update is called once per frame
    void OnEnable()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.FireBall:
            case ItemData.ItemType.Bone:
                if (level == 0)
                    descText.text = data.initItemDesc;
                else
                {
                    descText.text = string.Format(data.itemDesc, data.damages);
                }
                break;
        }
        levelText.text = string.Format("Lv.{0:D2}", level);

    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.FireBall:
            case ItemData.ItemType.Bone:
                if (level == 0)
                {
                    if (level == 0)
                        descText.text = data.initItemDesc;
                    else
                    {
                        descText.text = string.Format(data.itemDesc, data.damages);
                    }
                }
                else if (level > 0)
                {
                    
                }
                level++;
                break;
        }


        /*if (level == data.damages.Length)
        {
            GetComponentInParent<LevelUp>().maxLevelNum++;
            GetComponent<Button>().interactable = false;
        }*/
    }
}
