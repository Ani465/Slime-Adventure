using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas uiCanvas;
    public Canvas gameCanvas;
    public GameObject gameOverImage;
    public GameObject gameWinImage;
    public GameObject customizeWindow;

    private CameraFollow _cameraFollow;
    private CoinSpawner _coinSpawner;
    private EnemySpawner _enemySpawner;
    private DeathZone _deathZone;
    private GameObject _player;

    private bool _isGameActive;
    private bool _canCheckEnemies;

    private void Awake()
    {
        if (Camera.main != null)
            _cameraFollow = Camera.main.GetComponent<CameraFollow>();
        _cameraFollow.enabled = false;

        gameCanvas.enabled = false;
        gameOverImage.SetActive(false);
        gameWinImage.SetActive(false);

        _coinSpawner = FindObjectOfType<CoinSpawner>();
        _coinSpawner.enabled = false;

        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _enemySpawner.enabled = false;

        _deathZone = FindObjectOfType<DeathZone>();
        _deathZone.enabled = false;
    }

    private void Update()
    {
        if (!_isGameActive || !_canCheckEnemies) return;

        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            gameOverImage.SetActive(true);
            _isGameActive = false;
        }

        if (EnemySpawner.activeEnemies <= 0)
        {
            gameWinImage.SetActive(true);
            _isGameActive = false;
        }
    }

    public void PlayGame()
    {
        uiCanvas.enabled = false;
        gameCanvas.enabled = true;

        _cameraFollow.enabled = true;
        _coinSpawner.enabled = true;
        _enemySpawner.enabled = true;
        _deathZone.enabled = true;

        _isGameActive = true;
        StartCoroutine(CheckForEnemy());
    }

    private System.Collections.IEnumerator CheckForEnemy()
    {
        yield return new WaitForSeconds(2f);
        _canCheckEnemies = true;
    }

    public void OpenCustomizeWindow()
    {
        customizeWindow.SetActive(true);
    }

    public void CloseCustomizeWindow()
    {
        customizeWindow.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
