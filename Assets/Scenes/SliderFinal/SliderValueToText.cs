using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SliderValueToText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textElement;
    [SerializeField] private Button saveButton;

    [Header("Text Settings")]
    [SerializeField] private string prefix = "Estimated scene time: ";

    [SerializeField] private string suffix = " seconds";

    [Header("Data Storage")]
    public List<int> savedValues = new List<int>();

    void Start()
    {
        // Update the text immediately on start
        UpdateText(slider.value);

        // Add a listener that calls the function whenever the slider moves
        slider.onValueChanged.AddListener((val) => {
            UpdateText(val);
        });

        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveCurrentValue);
        }
    }

    void UpdateText(float value)
    {
        // "f2" limits the decimal places to 2. Use "f0" for whole numbers.
        textElement.text = prefix + '\n' + value.ToString("f0") + suffix;
    }

    public void SaveCurrentValue()
    {
        int valToSave = Mathf.RoundToInt(slider.value);

        if (GameDirector.Instance != null)
        {
            // 1. Save the final value
            GameDirector.Instance.savedValuesFromSession.Add(valToSave);
            Debug.Log($"Successfully saved {valToSave} to GameDirector!");
            Debug.Log("Current List: " + string.Join(", ", GameDirector.Instance.savedValuesFromSession));
            
            GameDirector.Instance.LoadNextScene();
        }
        else
        {
            Debug.LogError("Could not find GameDirector!");
        }
}
}