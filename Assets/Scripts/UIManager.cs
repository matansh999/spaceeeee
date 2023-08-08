using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Sprite[] _liveSprites;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private GameObject _restartText;
    private bool flicker = false;
    private GameManager _gameManager;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score = 0";
        _gameOver.SetActive(flicker);
        _restartText.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("Game Manager is null");
        }
    }

    // Update is called once per frame
    public void UpdateScore(int NewScore)
    {
        _scoreText.text = "Score = " + NewScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRutine());
        _restartText.SetActive(true);
        _gameManager.GameOver();
    }

    private IEnumerator GameOverRutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            flicker = !flicker;
            _gameOver.SetActive(flicker);
        }
    }
}
