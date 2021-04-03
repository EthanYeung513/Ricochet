using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NormEnemy : MonoBehaviour
{
     [SerializeField] private float lookRadius = 10f;
    public float distance;
    Transform target;
    NavMeshAgent agent;

    public int health = 100; //Set health to 100

    public int bulletHit = 0;

    public Slider currentSlider;
    public EnemyHealthBar currentSliderScript;

    public GameObject player;
    public GameObject currentBullet; //Game object of current bullet that hit enemy
    public int currentBounces; //count of bounces of current bullet

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //Set player variable to instance of player
        target = player.transform; //Set target to player transform
        agent = GetComponent<NavMeshAgent>();
        
        currentSlider = gameObject.transform.GetChild(1).gameObject.GetComponent<Canvas>().transform.GetChild(0).gameObject.GetComponent<Slider>();
        currentSliderScript = currentSlider.GetComponent<EnemyHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
      
            agent.SetDestination(target.position); //Move enemy towards target(player)
        

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player")) //If it collided withplayer
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Restart scene
        }
        else if (other.CompareTag("Bullet")) //If it collided with bullet
        {
            if (currentBullet == other.gameObject) //Make sure the same bullet doesn't damage 2 times
            {
                return;
            }
            currentBullet = other.gameObject; //Set currentBullet
            
            bulletBehaviour currentBulletScript = currentBullet.GetComponent<bulletBehaviour>(); //Get script of current Bullet
            currentBounces = currentBulletScript.bounceCount; //Get bounce count
            Destroy(currentBullet); //Destroy this bullet
            calculateDamage(currentBounces); //Call function to calculate and deal damage
            Debug.Log("CALLED");
           
        }
    }

    void calculateDamage(int currentBounces)
    {
        int damageToDeal = 10 + (currentBounces * 50);
        Debug.Log(currentBounces + "Bounces");
        Debug.Log(damageToDeal);
        bulletHit += 1;
        Debug.Log(bulletHit);

        health -= damageToDeal;  //Take away from health

        if (health <= 0) 
        {
            Destroy(this.gameObject); //Health less than or = to 0, so destroy
        }
        else
        {
            currentSliderScript.changeHealthBarValue();
        }

    }
}
