using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    Stat _stat;

    private void Start()
    {
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position;

        float ratio = _stat.HP / (float)_stat.MaxHP;
        setHpRatio(ratio);
    }

    public void setHpRatio(float ratio)
    {
        if (ratio < 0)
            ratio = 0;
        if (ratio > 1)
            ratio = 1;
        this.transform.GetChild(0).GetComponent<Slider>().value = ratio;
    }
}
