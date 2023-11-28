using UnityEngine;
using UnityEngine.AI;
using StateMachine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterListener : MonoBehaviour, IHear
{
    [SerializeField] private SkinnedMeshRenderer mat;
    [SerializeField] private AnimationCurve curve;
    Material m;
    LayerMask playerLayer;
    NavMeshAgent agent;
    public bool path;
    private int size;
    IState currentState;
    Player player;
    AudioSource asMon;
    Color emColor = Color.red;

    private Coroutine currentCoroutine;


    float duration = 0.5f, stay = 0.5f;
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
        player = FindObjectOfType<Player>();
        playerLayer = LayerMask.GetMask("Player");
    }
    
    private void Start()
    {
        asMon = GetComponent<AudioSource>();
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
        return Physics.CheckSphere(transform.position, 5, playerLayer);
    }

    public void RespondToSound(Sound sound)
    {
        if (Physics.CheckSphere(transform.position, 5, playerLayer))
        {
            currentState = StateControl.States[State.STATE_CHASING];
        }
        else
        {
            agent.SetDestination(sound.pos);
            asMon.Stop();
        }
        if (currentCoroutine != null)
        {
            StartCoroutine(Reload());
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(Glow());
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
