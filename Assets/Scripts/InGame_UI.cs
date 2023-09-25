using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGame_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] TextMeshProUGUI yourScoreText;
    [SerializeField] TextMeshProUGUI highestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Update_ScoreText(0);
        Update_Health(10);
    }

    public void Update_ScoreText(float value)
    {
        scoreText.SetText("Score : " + value);
    }

    public void Update_Health(float value)
    {
        healthSlider.GetComponent<Slider>().value = value;
    }

    public void GameOverUI()
    {
        highestScoreText.SetText("Highest Score : " + GameManager.instance.GetHighestScore()); 
        yourScoreText.SetText("Your " + scoreText.text);
        gameoverUI.SetActive(true);

    }

    public void RestartGame_WithManager()
    {
       GameManager.instance.RestartGame();
    }

}
