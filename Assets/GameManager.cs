using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject GameOverScreen;
    public Text PlayerWin;
    public Text PlayerLose;

    public GameObject DamagePanel;
    public float DamageScreenTime = .5f;
    float DamageStartTime;

    public Slider PlayerSlider;
    public DamageLossText DamageLossPlayer;
    public Slider EnemySlider;
    public DamageLossText DamageLossEnemy;

    // Use this for initialization

    public void PlayerDamge(int damage)
    {
        PlayerSlider.value -= damage;
        DamageLossPlayer.HealthLoss(damage);

        DamagePanel.GetComponent<DamagePanelScreen>().Damage();

        if (PlayerSlider.value <= 0) {
            GameOverScreen.SetActive(true);
            PlayerLose.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void EnemyDamage(int damage)
    {
        EnemySlider.value -= damage;
        DamageLossEnemy.HealthLoss(damage);

        if (EnemySlider.value <= 0) {
            GameOverScreen.SetActive(true);
            PlayerWin.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        PlayerWin.gameObject.SetActive(false);
        PlayerLose.gameObject.SetActive(false);
        GameOverScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
	}
}
