using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;

    public Image fill;

    public void SetMaxPotion(int potion)
    {
        slider.maxValue=potion;
        slider.value=potion;

        fill.color=gradient.Evaluate(1f);
    }

    public void SetPotion(int potion)
    {
        slider.value=potion;
        fill.color=gradient.Evaluate(slider.normalizedValue);
    }

}
