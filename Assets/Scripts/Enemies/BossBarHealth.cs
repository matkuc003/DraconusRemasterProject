using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossBarHealth : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private int maxHealth = 500;
    private int currentHealth;
    public bool isInvulnerable = false;
    public void resetHealthBar()
    {
        this.currentHealth = this.maxHealth;
        slider.maxValue = this.maxHealth;
        slider.value = this.maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void takeDamage(int damage)
    {
        if (isInvulnerable)
            return;
        if (this.currentHealth <= 200)
        {
            Debug.Log("RAGE");
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().SetBool("IsEnrage", true);
        }
        GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().SetTrigger("IsHitted");
        this.currentHealth -= damage;
        slider.value = this.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (checkBossDied())
             die();
    }

    private bool checkBossDied()
    {

        return this.currentHealth <= 0;
    }

    private void die()
    {
        GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().SetTrigger("IsDead");

        SceneManager.LoadScene("Winner");
    }
}
