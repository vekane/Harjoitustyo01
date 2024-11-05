using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    private int _score;
    public GameObject gameoverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI levelText;
    public GameObject startText;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gameoverScreen.SetActive(false);
        levelText.text = "Level 1";
    }



    //AddScore funktio mink‰ kutusmalla lis‰t‰‰n pelaajalle pisteit‰.
    public void AddScore()
    {
        _score = _score + 1;
        scoreText.text = "Score: " + _score.ToString();
    }


    //UpdateLifeCount p‰ivitt‰‰ el‰m‰pisteet mitk‰ ovat UI nÂkyvill‰
    public void UpdateLifeCount(int lifeAmount)
    {
        lifeText.text = "Lifes: " + lifeAmount.ToString();
    }


    //GameOverScreen mill‰ aktivoidaan gameover n‰kym‰
    public void GameOverScreen()
    {
        gameoverScreen.SetActive(true);
    }


    //RestartGame lataa peli scenen uudelleen
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    //HideStartText piilottaa alussa n‰kyv‰n ohjetekstin
    public void HideStartText()
    {
        startText.SetActive(false);
    }

}
