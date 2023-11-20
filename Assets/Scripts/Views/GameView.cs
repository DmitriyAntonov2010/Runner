using Items;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Views
{
    public class GameView : MonoBehaviour, IGameViewObserver
    {
        private const string HighScore = "HighScore";
    
        [SerializeField] 
        private Player _player;

        [SerializeField] 
        private TextMeshProUGUI _waitText;

        [SerializeField] 
        private TextMeshProUGUI _score;
    
        [SerializeField]
        private TextMeshProUGUI _highScore;

        [SerializeField] 
        private GameObject _gameOver;

        [SerializeField]
        private Button _retry;

        private void OnEnable()
        {
            UpdateHighScore();
            _retry.onClick.AddListener(OnRetry);
        }
    
        private void OnDisable()
        {
            _retry.onClick.RemoveListener(OnRetry);
        }

        private void Start()
        {
            _player.SetObserver(this);
        }
    
        public void UpdateTimeLeft(string timeLeft)
        {
            _waitText.text = timeLeft;
        }

        public void UpdateScore(string score)
        {
            _score.text = score;
        }

        public void GameOver(float score)
        {
            _waitText.gameObject.SetActive(false);
            _gameOver.SetActive(true);
            _retry.gameObject.SetActive(true);
            UpdateHighScore(score);
        }

        private void OnRetry()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    
        private void UpdateHighScore(float score = 0)
        {
            var highScore = PlayerPrefs.GetFloat(HighScore, 0);

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetFloat(HighScore, highScore);
            }

            _highScore.text = Mathf.FloorToInt(highScore).ToString("D5");
        }
    }
}
