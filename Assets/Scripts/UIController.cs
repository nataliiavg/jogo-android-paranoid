using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider sliderLife;
    public Slider sliderStamina;

    public void SetLife(float maxLife, float currentLife){
        float newPositionSlider = currentLife * 1 / maxLife;
        sliderLife.value = newPositionSlider;
    }

    public void SetStamina(float maxStamina, float currentStamina){
        float newPositionSlider = currentStamina * 1 / maxStamina;
        sliderStamina.value = newPositionSlider;
    }

    void Start(){
        
    }

    
    void Update(){
        
    }
}
