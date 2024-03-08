using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxValue(float health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void UpdateValue(float health)
	{
		health = Math.Min(health, slider.maxValue);
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}