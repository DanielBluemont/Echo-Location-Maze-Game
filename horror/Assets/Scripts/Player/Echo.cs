using MazeGame.EnemyAI;
using UnityEngine;

namespace MazeGame.Player
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Echo : MonoBehaviour
    {
        private float loud, time;
        ParticleSystem ps;
        const float SLOPE = 0.277f;
        public void SetSize(float _loud)
        {
            loud = 3 * _loud;
            loud = Mathf.Clamp(loud, 1, 40);
            time = SLOPE * loud;
        }
        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }
        void Start()
        {   
            if (ps != null)
            {
                var main = ps.main;
                main.startLifetime = time;
                main.startSize = loud;
            }
            //Debug.Log($"{loud} | {time}");
            CreateSound();
            Destroy(gameObject, time + 1);
        }
        void CreateSound()
        {
            var sound = new global::MazeGame.EnemyAI.Sound(this.transform.position, loud);
            Sounds.MakeSound(sound);
        }

    


    }
}
