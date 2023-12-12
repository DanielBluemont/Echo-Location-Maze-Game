using UnityEngine;

namespace MazeGame.EnemyAI
{
    public enum NoiseType
    {
        Microphone,
        Object
    }
    public static class Sounds
    {
        
        public static void MakeSound(Sound sound)
        {
            Collider[] colliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(sound.pos, sound.range, colliders, LayerMask.GetMask("Monster"));
            if (colliders.Length > 0 && colliders[0] != null && colliders[0].TryGetComponent<IHear>(out IHear agent))
            {
                agent.RespondToSound(sound);
            }
        }
    }
}
