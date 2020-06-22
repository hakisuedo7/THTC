using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControll : MonoBehaviour
{
    enum Type
    {
        BGM,SE
    }

    [SerializeField] private Type type;
    Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        SetValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetValue()
    {
        switch (type)
        {
            case Type.BGM:
                slider.value = Setting.Config.BGMVOLUME;
                break;
            case Type.SE:
                slider.value = Setting.Config.SEVOLUME;
                break;
            default:
                Debug.Log("Type is empty");
                break;
        }
    }

    public void OnValueChange()
    {
        switch (type)
        {
            case Type.BGM:
                Setting.Config.BGMVOLUME = (int)slider.value;
                break;
            case Type.SE:
                Setting.Config.SEVOLUME = (int)slider.value;
                break;
            default:
                Debug.Log("Type is empty");
                break;
        }
    }
}
