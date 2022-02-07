using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameOverScreen gameOverScreen;

    public void GameOver()
    {
        if (! gameHasEnded) {
            gameHasEnded = true;
            gameOverScreen.Setup();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
