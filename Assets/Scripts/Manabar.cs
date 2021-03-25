using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour
{
    public Slider slider;

    public void AdjustMana(float mana)
    {
        slider.value = mana;
    }
}
