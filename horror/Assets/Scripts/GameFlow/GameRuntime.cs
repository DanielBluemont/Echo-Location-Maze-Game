using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazeGame.GameFlow
{
    public class GameRuntime : MonoBehaviour
    {
        public void StartGame(int scenesNum)
        {
            SceneManager.LoadScene(scenesNum);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
