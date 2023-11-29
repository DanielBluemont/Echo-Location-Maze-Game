using UnityEngine;

namespace MazeGame.EnemyAI
{
    public class Sound
    {
        public readonly Vector3 pos;
        public readonly float range;
    
        public Sound(Vector3 _pos, float _range)
        {
            pos = _pos;
            range = _range *2;
        }
    }
}
