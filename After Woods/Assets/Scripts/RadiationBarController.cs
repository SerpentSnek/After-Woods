using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiationBarController : MonoBehaviour
{
    public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxRadiation(float radiation)
	{
		slider.maxValue = radiation;
		slider.value = radiation;

		fill.color = gradient.Evaluate(1f);
	}

    public void UpdateRadiation(float radiation)
	{
		slider.value = radiation;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
