//using System.Linq;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public Seat onTable;
    public float movementSpeed;
    private enum animationState {idle, walking, sit_idle, sit_pointing, sit_angry};
    public Animator anim;
    [SerializeField] private bool isAtSeat = false;
    [SerializeField] private bool isMoving; //this could be replaced for just a value greater than 0 on the movement vector
    [SerializeField] public bool hasOrdered = false;
    [SerializeField] public bool hasEaten = false;
    [SerializeField] public bool isAlive = true;
    [SerializeField] public float destructionDelay = 40.0f;
    [SerializeField] public float timer = 0.0f;
    [SerializeField] public bool timerStarted = false;
    private GameObject[] tableObjects;
    private Transform[] tables;

    public AudioSource source;
    public AudioClip Complete_bell;
    public AudioClip dying_npc;

    TimeController TimeController;
    ClienteAtendido ClienteAtendido;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        tableObjects = GameObject.FindGameObjectsWithTag("seat").Where(o => !o.GetComponent<Seat>().isOccupied).ToArray();

        TimeController = GameObject.FindGameObjectWithTag("Timer_Tag").GetComponent<TimeController>();
        ClienteAtendido = GameObject.FindGameObjectWithTag("Costumer_Tag").GetComponent<ClienteAtendido>();

        tables = new Transform[tableObjects.Length];
        //Collider[] colliders = tableObjects.SelectMany(t => t.GetComponentsInChildren<Collider>()).ToArray();

        

        for (int i = 0; i < tableObjects.Length; i++)
        {
            Debug.Log($"Mesa {i} esta {tableObjects[i].GetComponent<Seat>().isOccupied}");
            if (!tableObjects[i].GetComponent<Seat>().isOccupied)
            {
                tables[i] = tableObjects[i].transform;
            }
        }
        Transform emptyTable = tables.Last();
        onTable = emptyTable.GetComponent<Seat>();


        Debug.Log($"Mesas disponibles {tables.Length}");

        if (emptyTable != null )
        {
            Debug.Log("Dentro del empty table not null");
            target = emptyTable;
            agent.destination = target.position;
            agent.speed = movementSpeed;
        }

    }

    // Update is called once per frame
    void Update()
    {

         if (timerStarted)
        {
            timer += Time.deltaTime;
        }
        
        if (timer >= destructionDelay)
        {
            killNPC();
        }

        if(!isAtSeat)
        {
            agent.destination = target.position;
            isMoving = agent.velocity.magnitude > 0.1f; //Check if agent is moving
        }
        UpdateAnimationState();
        
    }


    private void UpdateAnimationState()
    {
        animationState state;
        state = animationState.idle;

        if (isMoving && !isAtSeat)
        {
            state = animationState.walking;
        }

        if (isAtSeat)
        {
            if (hasOrdered)
            {
                if (timer > 30f)
                {
                    state = animationState.sit_angry;
                    // Debug.Log("Animation: sit_angry");                 
                }
                state = animationState.sit_idle;
                //Debug.Log("Animation: sit_idle");                 
            }
            else
            {
                if (timer > 10f)
                {
                    state = animationState.sit_angry;
                    //Debug.Log("Animation: sit_angry");
                }
                else
                {
                    state = animationState.sit_pointing;
                    //Debug.Log("Animation: sit_poiting");    
                }
            }
        }
        anim.SetInteger("state",(int)state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("seat"))
        {
            isAtSeat = true;
            // StartCoroutine(logger("reached seat, starting counter"));
            timerStarted = true;                      
            Debug.Log("Reached seat, starting timer.");                 

        }
        if (other.CompareTag("Food"))
        {
            Debug.Log("Customer is in contact Food."); 
            clientServed();                
        }
    }

    public void clientServed()
    {
        TimeController.addTime(10); //We can change the amount of time that is add
        ClienteAtendido.atendido();//sum clientes atendidos 
        source.PlayOneShot(Complete_bell);
        killNPC();
    }

    public void clientNotServed()
{
        TimeController.restTime(10); //We can change the amount of time that is rest
        source.PlayOneShot(dying_npc);
        killNPC();

    }

    private void killNPC()
    {
        onTable.isOccupied = false;
        Destroy(gameObject);
    }

    public void setHasOrdered(bool state)
    {
        hasOrdered = state;
    }

    public void setHasEaten(bool state)
    {
        hasEaten = state;
    }

}
