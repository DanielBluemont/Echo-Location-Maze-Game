using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameSettingss : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
