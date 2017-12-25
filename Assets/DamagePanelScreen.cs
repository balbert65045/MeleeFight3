using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePanelScreen : MonoBehaviour {

    // Use this for initialization
    Image DamageImage;
    public float DamageDuration = .2f;
    float StartTime;

    public float IncreaseSpeed = 1;
    float a;

    void Start () {
        DamageImage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (StartTime + DamageDuration > Time.time)
        {
            a = DamageImage.color.a;
            a += Mathf.Clamp(Time.deltaTime * IncreaseSpeed, 0, 150);
            DamageImage.color = new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, a);
        }
        else
        {
            a = 0;
            DamageImage.color = new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, a);
        }
    }
    public void Damage()
    {
        StartTime = Time.time;
        a = 0;
    }


}
