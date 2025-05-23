using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image hpFill;

    public void Init()
    {
        hpFill.fillAmount = 1;
        UIManager.Instance.OnChangeHealth += ChangeHealthUI;
    }

    public void ChangeHealthUI(float currentHealth, float maxHealth)
    {
        hpFill.fillAmount = currentHealth / maxHealth;
    }
}
