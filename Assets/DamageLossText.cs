using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageLossText : MonoBehaviour {

    // Use this for initialization
    Text text;

    public float DamageShownDuration = 1;
    float TimeValueShown;

	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeValueShown + DamageShownDuration < Time.time)
        {
            text.text = "";
        }

	}

    public void HealthLoss(int value)
    {
        text.text = "- " + value.ToString();
        TimeValueShown = Time.time;
    }


}
