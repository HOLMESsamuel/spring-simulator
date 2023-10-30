using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GravitySliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gravityValueText = null;

    [SerializeField] private float maxSliderAmount = 5;

    public float localValue;

    public void SliderChange(float value)
    {
        localValue = value * maxSliderAmount;
        gravityValueText.text = localValue.ToString("0.0");
    }

    public float GetLocalValue() { return localValue; }
}
