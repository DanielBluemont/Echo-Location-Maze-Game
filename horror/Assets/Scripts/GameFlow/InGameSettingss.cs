using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazeGame.GameFlow
{
    public class InGameSettingss : MonoBehaviour
    {
        public void ChangeScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}
