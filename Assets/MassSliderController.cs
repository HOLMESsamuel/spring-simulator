using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MassSliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI massValueText = null;

    [SerializeField] private float maxSliderAmount = 1;

    public float localValue;

    public void SliderChange(float value)
    {
        localValue = value * maxSliderAmount;
        massValueText.text = localValue.ToString("0.0");
    }

    public float GetLocalValue() { return localValue; }
}
