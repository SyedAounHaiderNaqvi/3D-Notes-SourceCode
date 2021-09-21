using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Material blurMat;

    public Image blurBG;
    //public Material blurMatTitle;

    public GameObject warningGpu;
    public Toggle blurToggle;

    public void ToggleBlurOnStart()
    {
        if (Convert.ToInt16(SystemInfo.graphicsMemorySize) < 1400)
        {
            // The GPU is not good so disable blurring by default and show warning
            warningGpu.SetActive(true);
            blurToggle.interactable = false;
            blurToggle.isOn = false;
            blurBG.material = null;
            PlayerPrefs.SetInt("Blur", 0);
        }

        else
        {
            blurToggle.interactable = true;
            if (PlayerPrefs.GetInt("Blur", 1).Equals(1))
            {
                blurToggle.isOn = true;
                warningGpu.SetActive(false);
                blurBG.material = blurMat;
            }
            else
            {
                blurToggle.isOn = false;
                blurBG.material = null;
            }
        }
    }

    public void ToggleBlur(bool choice)
    {
        if (choice)
        {
            //enable blur
            blurBG.material = blurMat;
            PlayerPrefs.SetInt("Blur", 1);
        }
        else
        {
            blurBG.material = null;
            PlayerPrefs.SetInt("Blur", 0);
        }
    }
}
