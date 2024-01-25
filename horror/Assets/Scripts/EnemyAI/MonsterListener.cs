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

        float searchRange = 40f;

    

        private void OnEnable()
        {
            UIprompt.OnPauseSwitch += HandleAudio;
            UIprompt.OnFinishingGame += ChasePlayer;
        }

        private void OnDisable()
        {
            UIprompt.OnPauseSwitch -= HandleAudio;
            UIprompt.OnFinishingGame -= ChasePlayer;
        }
        private void ChasePlayer()
        {
            currentState = StateControl.States[State.STATE_CHASING];
        }
        private void HandleAudio(bool val)
        {
            if (val) asMon.Pause();
            else asMon.UnPause();
        }
        
        public void SetRef(Player.Player pl)
        {
            player = pl;
        }

        private void Update() 
        {
        
            currentState = currentState.DoAction(this);
        }
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
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
            if (Vector3.Distance(transform.position, sound.pos) <= searchRange)
            {
                agent.SetDestination(sound.pos);
            }
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
