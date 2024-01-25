using MazeGame.EnemyAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGame.EchoParticleSystem
{
    public class ParticleManager : MonoBehaviour
    {
        public const float SLOPE = 0.277f;
        Color origColor = new Color(2.512648f, 5.079918f, 10.43295f, 1);
        Color detColor = new Color(11.69494f, 0.1544609f, 0.2747274f, 1);

        public Color colorOr
        {
            get { return origColor; }
        }
        public Color colorDet
        {
            get { return detColor; }
        }
        
        public static ParticleManager Instance;
        
        private ParticleSystem _ps;
        private ParticleSystemRenderer psRenderer;
        private MonsterListener _ml;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            _ps = GetComponent<ParticleSystem>();
            psRenderer = GetComponent<ParticleSystemRenderer>();
            _ml = FindObjectOfType<MonsterListener>();
            _ps.trigger.AddCollider(_ml.GetComponent<Collider>());
        }

        public void CreateSound(Vector3 pos, float loud)
        {
            var sound = new Sound(pos, loud, NoiseType.Microphone);
            Sounds.MakeSound(sound);
        }
        public void EmitHere(Vector3 pos, float loud)
        {
            loud = Mathf.Clamp(loud * 3, 1, 40);
            float time = SLOPE * loud;
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startLifetime = time;
            emitParams.startSize = loud;
            emitParams.position = pos;
            _ps.Emit(emitParams, 1);
        }
        private void OnParticleTrigger()
        {
            StartCoroutine(ColorFlash());
        }
        private IEnumerator ColorFlash()
        {
            psRenderer.material.SetColor("Color", detColor);
            yield return new WaitForSeconds(0.5f);
            psRenderer.material.SetColor("Color", origColor);
        }
    }
}
