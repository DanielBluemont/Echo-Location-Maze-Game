using MazeGame.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace MazeGame.EnemyAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MonsterListener : MonoBehaviour, IHear
    {

        [SerializeField] private SkinnedMeshRenderer mat;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private AudioSource asMon;
        Material m;
        LayerMask playerLayer;
        NavMeshAgent agent;
        public bool path;
        private int size;
        IState currentState;
        Player.Player player;
        Color emColor = Color.red;

        private Coroutine currentCoroutine;


        float duration = 0.5f, stay = 0.5f;

        private void OnEnable()
        {
            UIprompt.OnPauseSwitch += HandleAudio;
        }

        private void OnDisable()
        {
            UIprompt.OnPauseSwitch -= HandleAudio;
        }
        private void HandleAudio(bool val)
        {
            if (val) asMon.Pause();
            else asMon.UnPause();
        }
        private IEnumerator Glow()
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float frac = elapsedTime / duration;
                float value = curve.Evaluate(frac);
                float emissiveIntensity =  Mathf.Lerp(0f, 1f, value);
                m.SetColor("_EmissionColor", emColor *emissiveIntensity);
                yield return null;
            }

            yield return new WaitForSeconds(stay);
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float frac = elapsedTime / duration;
                float value = curve.Evaluate(frac);
                float emissiveIntensity=  Mathf.Lerp(1f, 0f, value);
                m.SetColor("_EmissionColor", emColor * emissiveIntensity);
                yield return null;
            }

        }

        private void Update() 
        {
        
            currentState = currentState.DoAction(this);
        }
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            player = FindObjectOfType<Player.Player>();
            playerLayer = LayerMask.GetMask("Player");
        }
    
        private void Start()
        {
            m = mat.material;
            m.SetFloat("_EmissionIntensity", 1);
            currentState = StateControl.States[State.STATE_SEARCHING];
            WalkToRandomPoint();
        }
    
        public void TracePlayerLocation()
        {
            agent.SetDestination(player.transform.position);
        }
        public bool IsPathCompleted()
        {
            return !agent.pathPending && !agent.hasPath;
        }
        public void AttackPlayerNearby()
        {
            if (Physics.CheckSphere(transform.position, 1, playerLayer))
            {
                SceneManager.LoadScene(3);
            }
        }
        public bool isPlayerInZone()
        {
            return Physics.CheckSphere(transform.position, 15, playerLayer);
        }

        public void RespondToSound(Sound sound)
        {
            if (isPlayerInZone() && sound.type == NoiseType.Microphone)
            {
                currentState = StateControl.States[State.STATE_CHASING];
                if (!asMon.isPlaying) asMon.Play();
            }
            else
            {
                agent.SetDestination(sound.pos);
                StartCoroutine(FadeAudio());
            }
            if (currentCoroutine != null)
            {
                StartCoroutine(Reload());
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(Glow());
        }

        private IEnumerator FadeAudio()
        {
            float currentTime = 0;
            float startVolume = asMon.volume;
            while (currentTime < 3)
            {
                currentTime += Time.deltaTime;
                asMon.volume = Mathf.Lerp(startVolume, 0, currentTime / 3);
                yield return null;
            }
            asMon.Stop();
        }

        private Vector3 FindPointToReach()
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);
            return new Vector3((x*5) + 2.5f,0, (x*5) + 2.5f);
        }
        public void WalkToRandomPoint()
        {
            agent.SetDestination(FindPointToReach());
        }
        public void InformAboutMapSize(int _size)
        {
            size = _size - 1;
        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(0.3f);
        }
    }
}
