using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image image;
    public Gradient gradient;
    void Awake()
    {
        image.fillAmount  = 0f;
        gradient.Evaluate(image.fillAmount);
    }

    public void SetProgress(float progress){
        image.fillAmount = progress/100;
        image.color = gradient.Evaluate(progress/100);
    }
}
