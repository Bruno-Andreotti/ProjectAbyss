using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resDropdown;
    Resolution[] resolutions;

    void Start()
    {
        //Procura as resoluções que o PC suporta, e lista elas para serem escolhidas no dropdown
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        //modifica a resolução da tela
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen );
    }
    public void SetVolume(float volume)
    {
        //muda o volume Master
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        //muda a qualidade da imagem
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        //determina se o jogo está em tela cheia ou nao
        Screen.fullScreen = isFullscreen;
    }
}
