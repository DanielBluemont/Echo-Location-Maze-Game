using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazeGame.GameFlow
{
    public class ChangeScene : MonoBehaviour
    {
        public void SwitchScene(int id)
        {
            SceneManager.LoadScene(id);
        }
    }
}
