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

    void Update()
    {
        if (!GameManager.Instance.target) return;
        _screenSaver.SetActive(false);

        if (!hasSetValues)
        {
            hasSetValues = true;
            SetValues();
        }

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

    public void SetValues()
    {
        // TODO reset sliders and delete POI
        _status.gameObject.SetActive(false);
        _temperatureSlider.maxValue = 100;
        _temperatureTargetText.text = $"{GameManager.Instance.target.targetTemperature}%";
        _pressureInputSlider.maxValue = 100;
        _pressureOutputSlider.maxValue = 100;
        _pressureOutputSlider.value = _pressureOutputSlider.maxValue;

        GenerateObjective();
    }

    public void GenerateObjective()
    {
        Slider slider = _pressureOutputSlider;

        float normalized = Mathf.InverseLerp(slider.minValue, slider.maxValue, GameManager.Instance.target.targetPressure);

        RectTransform handleArea = slider.handleRect.parent as RectTransform;

        float yPos = Mathf.Lerp(handleArea.rect.yMin, handleArea.rect.yMax, normalized);

        _objective.localPosition = new Vector3(slider.handleRect.localPosition.x, yPos, 0f);
    }

    public bool CheckValues()
    {
        if (_pressureOutputSlider.value == GameManager.Instance.target.targetPressure
            && _temperatureSlider.value == GameManager.Instance.target.targetTemperature)
            return true;

        return false;
    }

    public void ChangePressureSlider()
    {
        _pressureOutputSlider.value = _pressureInputSlider.maxValue - _pressureInputSlider.value;
        _currentTemperatureText.text = $"{_temperatureSlider.value}%";
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
        // change model
        GameManager.Instance.target = null;
    }
}
