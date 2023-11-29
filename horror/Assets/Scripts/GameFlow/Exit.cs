using UnityEngine;

namespace MazeGame.GameFlow
{
    public class Exit : MonoBehaviour
    {
        public void UnlockDoor()
        {
            ExitDoor door = GetComponentInChildren<ExitDoor>();
            door.Unlock();
        }
    }
}
