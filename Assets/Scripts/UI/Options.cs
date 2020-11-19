using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public GameObject OptionsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
    }
    private void Start()
    {
        BGMSlider.value = AudioManager.current.BGMVolume;
        SFXSlider.value = AudioManager.current.SFXVolume;
    }

    public void ValueChanged()
    {
        AudioManager.current.BGMVolume = BGMSlider.value;
        AudioManager.current.SFXVolume = SFXSlider.value;
    }

    public void ToggleOptions() 
    {
        OptionsPanel.SetActive(!OptionsPanel.activeInHierarchy);
    }
}
