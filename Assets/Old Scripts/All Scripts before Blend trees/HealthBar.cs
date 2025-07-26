using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthbarSlider;
    public Image fillImage;

    private float maxHealth;

    // Desaturated, gritty horror colors
    public Color fullHealthColor = new Color32(42, 107, 61, 255);   // #2A6B3D
    public Color midHealthColor = new Color32(158, 107, 27, 255);   // #9E6B1B
    public Color lowHealthColor = new Color32(107, 27, 27, 255);    // #6B1B1B

    public void GiveFullHealth(float health)
    {
        maxHealth = health;
        healthbarSlider.maxValue = health;
        healthbarSlider.value = health;
        UpdateHealthColor();
    }

    public void SetHealth(float health)
    {
        healthbarSlider.value = health;
        UpdateHealthColor();
    }

    private void UpdateHealthColor()
    {
        float healthPercent = healthbarSlider.value / maxHealth;

        if (healthPercent > 0.5f)
        {
            fillImage.color = fullHealthColor;
        }
        else if (healthPercent > 0.2f)
        {
            fillImage.color = midHealthColor;
        }
        else
        {
            fillImage.color = lowHealthColor;
        }
    }
}
