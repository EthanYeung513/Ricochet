using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject lastBullet; //Game object of last bullet that left player

    private void OnTriggerEnter(Collider other)
    {
       
       if (other.CompareTag("Bullet")) //If it collided with bullet
        {
            bulletBehaviour lastBulletScript = lastBullet.GetComponent<bulletBehaviour>(); //Get script of current Bullet
            int currentBounces = lastBulletScript.bounceCount; //Get bounce count
            if (currentBounces == 0) //So the bullet doesnt insta kill own player, because it must bounce once
            {
                return;
            }
            lastBullet = other.gameObject; //Set currentBullet
            Debug.Log("Hit self");
            Destroy(gameObject); //Destroy self
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Restart scene


        }
    }
}
