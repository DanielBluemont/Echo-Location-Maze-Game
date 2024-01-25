using MazeGame.EnemyAI;
using System.Collections;
using UnityEngine;

namespace MazeGame.Player
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Echo : MonoBehaviour
    {
        const float SLOPE = 0.277f;
        private float loud, time;
        ParticleSystem ps;
        ParticleSystemRenderer psRenderer;
       
        public void SetSize(float _loud)
        {
            loud = 3 * _loud;
            loud = Mathf.Clamp(loud, 1, 40);
            time = SLOPE * loud;
        }
        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
            ps.trigger.AddCollider(FindObjectOfType<MonsterListener>());
            psRenderer = GetComponent<ParticleSystemRenderer>();
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
            //CreateSound();
            Destroy(gameObject, time + 1);
        }
       

        /*private void OnParticleTrigger()
        {
            
            Debug.Log("Yeah");
            StartCoroutine(ColorFlash());
        }*/
        /*private IEnumerator ColorFlash()
        {
            psRenderer.material.SetColor("Color", detColor);
            yield return new WaitForSeconds(0.5f);
            psRenderer.material.SetColor("Color", origColor);
        }*/
    }
}
