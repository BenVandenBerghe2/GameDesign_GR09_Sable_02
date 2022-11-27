using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SC_StaminaController : MonoBehaviour
{
    public Slider staminaBar;

    private int maxStamina = 100;
    private float currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(.1f);
    private Coroutine regen;

    public static SC_StaminaController instance;
    [SerializeField] private SC_TPSController player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= (amount);
            staminaBar.value = currentStamina; // send bar size to bar

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            StartCoroutine(WaitForRegen());
        }

    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        // regen stamina
        while (currentStamina < maxStamina)
        {
            currentStamina += (maxStamina / 100) * 1.1f;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }

    private IEnumerator WaitForRegen()
    {
        player.canSprint = false;
        player.canClimb = false;
        yield return new WaitForSeconds(5f);
        player.canSprint = true;
        player.canClimb = true;
    }
}