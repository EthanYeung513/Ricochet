using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] GameObject currentEnemy;
    NormEnemy currentEnemyScript;
    [SerializeField] GameObject player;
    [SerializeField] Slider thisSlider;
    [SerializeField] float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        thisSlider = gameObject.GetComponent<Slider>();

        player = GameObject.Find("Player"); //Set player variable to instance of player
        currentEnemy = gameObject.transform.parent.gameObject.transform.parent.gameObject; //Set the enemy as the parent 
        currentEnemyScript = currentEnemy.GetComponent<NormEnemy>(); //Get script of current Bullet
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void changeHealthBarValue()
    {
        currentHealth = currentEnemyScript.health; //Set health
        thisSlider.value = currentHealth / 100;
        Debug.Log(thisSlider.value);
    }
}
