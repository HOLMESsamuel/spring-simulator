using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KSliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kValueText = null;

    [SerializeField] private float maxSliderAmount = 5;

    public float localValue;

    private void Start()
    {
        localValue = 1;
        kValueText.text = localValue.ToString("0.0");
    }

    public void SliderChange(float value)
    {
        localValue = value * maxSliderAmount;
        kValueText.text = localValue.ToString("0.0");
    }

    public float GetLocalValue() { return localValue; }
}
