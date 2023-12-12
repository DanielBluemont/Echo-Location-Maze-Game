using MazeGame.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MazeGame.GameFlow
{
    public class Exit : MonoBehaviour
    {
        bool gotKey = false;
        [SerializeField] private GameObject _barrier;
        private void OnEnable()
        {
            UIprompt.OnFinishingGame += UnlockDoor;
        }
        private void OnDisable()
        {
            UIprompt.OnFinishingGame -= UnlockDoor;
        }
        private void UnlockDoor()
        {
            gotKey = true;
            Destroy(_barrier );
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Ha");
            if (other.gameObject.CompareTag("Player") && gotKey)
            {
                SceneManager.LoadScene(2);  
            }
        }
    }
}
