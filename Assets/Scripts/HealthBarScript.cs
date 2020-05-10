using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private int maxHealth = 100;
    private int currentHealth;
    
    public void resetHealthBar()
    {
        this.currentHealth = this.maxHealth;
        slider.maxValue = this.maxHealth;
        slider.value = this.maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void takeDamage(int damage)
    {
        this.currentHealth -= damage;
        slider.value = this.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (checkPlayerDied())
            showGameOverScene();
    }

    private bool checkPlayerDied()
    {
        return this.currentHealth <= 0;
    }

    private void showGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
