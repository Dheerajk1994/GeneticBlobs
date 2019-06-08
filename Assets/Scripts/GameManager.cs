using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] Evolution evolutionScript;

    [SerializeField] private Slider populationSlider;
    [SerializeField] private Slider matingChanceSlider;
    [SerializeField] private Slider mutationChanceSlider;
    [SerializeField] private Slider mutationAmountSlider;
    [SerializeField] private Slider stepIntervalSlider;

    [SerializeField] private Text populationAmountText;
    [SerializeField] private Text matingChanceAmountText;
    [SerializeField] private Text mutationChanceAmountText;
    [SerializeField] private Text mutationAmountText;
    [SerializeField] private Text stepIntervalText;

    [SerializeField] private Button startButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resetButton;

    private bool paused = false;


    private int populationAmount = 10;
    private float matingChance = 0.1f;
    private float mutationChance = 0.05f;
    private float mutationAmount = 0.05f;
    private float stepInterval = 0.05f;

    private void Start()
    {
        populationAmount = (int)populationSlider.value;
        matingChance = matingChanceSlider.value;
        mutationChance = mutationChanceSlider.value;
        mutationAmount = mutationAmountSlider.value;
        stepInterval = stepIntervalSlider.value;

        populationAmountText.text = populationAmount.ToString();
        matingChanceAmountText.text = matingChance.ToString();
        mutationChanceAmountText.text = mutationChance.ToString();
        mutationAmountText.text = mutationAmount.ToString();
        stepIntervalText.text = stepInterval.ToString();

        populationSlider.onValueChanged.AddListener(PopulationValueChanged);
        matingChanceSlider.onValueChanged.AddListener(MatingChanceValueChanged);
        mutationChanceSlider.onValueChanged.AddListener(MutationChanceValueChanged);
        mutationAmountSlider.onValueChanged.AddListener(MutationAmountValueChanged);
        stepIntervalSlider.onValueChanged.AddListener(StepIntervalValueChanged);

        startButton.onClick.AddListener(StartButtonClicked);
        pauseButton.onClick.AddListener(PauseButtonClicked);
        resetButton.onClick.AddListener(ResetButtonClicked);

        pauseButton.interactable = false;
        resetButton.interactable = false;
    }

   

    public void StartButtonClicked()
    {
        populationAmount = (int)populationSlider.value;
        matingChance = matingChanceSlider.value;
        mutationChance = mutationChanceSlider.value;
        mutationAmount = mutationAmountSlider.value;
        stepInterval = stepIntervalSlider.value;

        startButton.interactable          = false;
        populationSlider.interactable     = false;
        matingChanceSlider.interactable   = false;
        mutationChanceSlider.interactable = false;
        mutationAmountSlider.interactable = false;

        pauseButton.interactable = true;
        resetButton.interactable = true;

        evolutionScript.StartInitialPopulation(populationAmount, matingChance, mutationChance, mutationAmount, stepInterval);
    }

    public void PauseButtonClicked()
    {
        if (!paused)
        {
            pauseButton.GetComponentInChildren<Text>().text = "Resume";
            populationSlider.interactable     = true;
            matingChanceSlider.interactable   = true;
            mutationChanceSlider.interactable = true;
            mutationAmountSlider.interactable = true;

            paused = true;
        }
        else
        {
            populationAmount = (int)populationSlider.value;
            matingChance = matingChanceSlider.value;
            mutationChance = mutationChanceSlider.value;
            mutationAmount = mutationAmountSlider.value;

            pauseButton.GetComponentInChildren<Text>().text = "Pause";
            populationSlider.interactable     = false;
            matingChanceSlider.interactable   = false;
            mutationChanceSlider.interactable = false;
            mutationAmountSlider.interactable = false;

            paused = false;
        }
        
    }

    public void ResetButtonClicked()
    {

    }

    private void MutationAmountValueChanged(float arg0)
    {
        mutationAmountText.text = arg0.ToString();
    }

    private void MutationChanceValueChanged(float arg0)
    {
        mutationChanceAmountText.text = arg0.ToString();
    }

    private void MatingChanceValueChanged(float arg0)
    {
        matingChanceAmountText.text = arg0.ToString();
    }

    private void PopulationValueChanged(float arg0)
    {
        populationAmountText.text = arg0.ToString();
    }

    private void StepIntervalValueChanged(float arg0)
    {
        stepIntervalText.text = arg0.ToString();
    }


}
