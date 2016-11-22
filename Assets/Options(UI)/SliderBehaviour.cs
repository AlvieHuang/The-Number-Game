using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderBehaviour : MonoBehaviour {
    public string optionString;
    private Text SliderPercentLabel;
    private Slider slider;
    
	// Use this for initialization
	void Start () {
        SliderPercentLabel = transform.Find("Handle Slide Area/Handle/Text").GetComponent<Text>();
        slider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(optionString))
            slider.value = PlayerPrefs.GetInt(optionString) / 100.0f;
	}

    public void SliderUpdate()
    {
        if (slider == null)
            return; //this gets called when everything loads, before start

        //update visuals
        int newValue = (int)(100*slider.value);
        SliderPercentLabel.text = (newValue).ToString() + '%';

        //update data
        PlayerPrefs.SetInt(optionString, newValue);
        PlayerPrefs.Save();
        //Observers.Post(new Message(optionString));
    }
}
