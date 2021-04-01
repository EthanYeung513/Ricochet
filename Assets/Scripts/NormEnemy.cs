using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NormEnemy : MonoBehaviour
{
     [SerializeField] private float lookRadius = 10f;
    public float distance;
    Transform target;
    NavMeshAgent agent;

    public int health = 100; //Set health to 100

    public GameObject player;
    public GameObject currentBullet; //Game object of current bullet that hit enemy
    public int currentBounces; //count of bounces of current bullet

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //Set player variable to instance of player
        target = player.transform; //Set target to player transform
        agent = GetComponent<NavMeshAgent>(); 
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
            currentBullet = other.gameObject; //Set currentBullet
            bulletBehaviour currentBulletScript = currentBullet.GetComponent<bulletBehaviour>(); //Get script of current Bullet
            currentBounces = currentBulletScript.bounceCount; //Get bounce count
            calculateDamage(currentBounces); //Call function to calculate and deal damage
            Destroy(currentBullet); //Destroy this bullet
           
        }
    }

    void calculateDamage(int currentBounces)
    {
        int damageToDeal = 50 + (currentBounces * 50);
        Debug.Log(damageToDeal);

        health -= damageToDeal;  //Take away from health

        if (health <= 0) 
        {
            Destroy(this.gameObject); //Health less than or = to 0, so destroy
        }

    }
}
