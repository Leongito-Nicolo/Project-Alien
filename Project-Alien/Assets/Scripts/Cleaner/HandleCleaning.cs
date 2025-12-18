using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleCleaning : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider _pressureInputSlider;
    [SerializeField] private Slider _pressureOutputSlider;
    [SerializeField] private Slider _temperatureSlider;

    [Header("Texts")]
    [SerializeField] private TMP_Text _temperatureTargetText;
    [SerializeField] private TMP_Text _currentTemperatureText;
    [SerializeField] private TMP_Text _status;

    [Header("Objective")]
    [SerializeField] private RectTransform _objective;

    [Header("Screen Deactivated")]
    [SerializeField] private GameObject _screenSaver;

    private bool hasSetValues;
    private Coroutine completion;
    private PointOfInterest target;
    private GameObject pointObj;
    private GameObject objToClean;

    void OnEnable()
    {
        EventManager.OnSetTarget += StartCleaningMinigame;
        EventManager.OnDestroyObject += DestroyObject;
    }

    void OnDisable()
    {
        EventManager.OnSetTarget -= StartCleaningMinigame;
        EventManager.OnDestroyObject -= DestroyObject;
    }

    void Update()
    {
        if (!target) return;

        if (CheckValues() && completion == null)
        {
            completion = StartCoroutine(WaitAndComplete());
        }
        else if (!CheckValues() && completion != null)
        {
            _status.gameObject.SetActive(false);
            StopCoroutine(completion);
            completion = null;
        }
    }


    private void StartCleaningMinigame(GenerateRandomObject point)
    {
        if (!point) return;

        target = point.GetRandomPoint();
        objToClean = point.SpawnObjectAtLocation(target);
        pointObj = point.gameObject;
        _screenSaver.SetActive(false);

        if (!hasSetValues)
        {
            hasSetValues = true;
            SetValues();
        }
    }

    public void SetValues()
    {
        _status.gameObject.SetActive(false);

        _temperatureSlider.maxValue = 100;
        _temperatureSlider.value = _temperatureSlider.minValue;
        _temperatureTargetText.text = $"{target.targetTemperature}%";

        _pressureInputSlider.maxValue = 100;
        _pressureInputSlider.value = _pressureInputSlider.minValue;

        _pressureOutputSlider.maxValue = 100;
        _pressureOutputSlider.value = _pressureOutputSlider.maxValue;

        GenerateObjective();
    }

    public void GenerateObjective()
    {
        Slider slider = _pressureOutputSlider;

        float normalized = Mathf.InverseLerp(slider.minValue, slider.maxValue, target.targetPressure);

        RectTransform handleArea = slider.handleRect.parent as RectTransform;

        float yPos = Mathf.Lerp(handleArea.rect.yMin, handleArea.rect.yMax, normalized);

        _objective.localPosition = new Vector3(slider.handleRect.localPosition.x, yPos, 0f);
    }

    public bool CheckValues()
    {
        if (_pressureOutputSlider.value == target.targetPressure
            && _temperatureSlider.value == target.targetTemperature)
            return true;

        return false;
    }

    public void ChangePressureSlider()
    {
        _pressureOutputSlider.value = _pressureInputSlider.maxValue - _pressureInputSlider.value;
    }

    public void ChangeTemperatureText()
    {
        _currentTemperatureText.text = $"{_temperatureSlider.value}%";
    }

    public IEnumerator WaitAndComplete()
    {
        _status.gameObject.SetActive(true);
        _status.text = "Wait...";
        yield return new WaitForSeconds(3f);
        _status.text = "Complete!";

        _screenSaver.SetActive(true);


        target.CleanObject(objToClean);

        Destroy(pointObj);
        EventManager.EndCleaning(target.valueOnSell);
        hasSetValues = false;
    }

    private void DestroyObject()
    {
        Destroy(objToClean);
    }
}
