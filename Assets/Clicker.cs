using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = System.Numerics.Vector3;

public class Clicker : MonoBehaviour {
    [SerializeField] private Material matDefault = null;
    [SerializeField] private Material matHit = null;
    private static int maxHealth = 1;
    private int health = maxHealth;

    private float resize;
    private float sizeOnClickTarget;
    private bool shrink = false;

    public void init(int setHealth)
    {
        health = maxHealth = setHealth;
    }

    public bool changeHealth(int change)
    {
        health += change;
        if (health < 1) {
            kill();
            return true; //object dead
        }
        
        //RUN!!!
        gameObject.GetComponent<Rigidbody>().AddTorque( Random.Range(-1000f,1000f), Random.Range(-1000f,1000f), Random.Range(-1000f,1000f) );
        gameObject.GetComponent<Rigidbody>().AddForce( Random.Range(-800f,800f), Random.Range(-800f,800f), 0 );
        
        sizeOnClickTarget = Mathf.Max( 0.75f, gameObject.transform.localScale.x * 0.75f );
        shrink = true;
        gameObject.GetComponent<MeshRenderer>().material = matHit;
        return false;
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        gameObject.transform.localScale *= Random.Range(0.8f, 1.5f);
        gameObject.GetComponent<Rigidbody>().AddTorque( Random.Range(-100f,100f), Random.Range(-100f,100f), Random.Range(-100f,100f) );
        gameObject.GetComponent<Rigidbody>().AddForce( Random.Range(-300f,300f), Random.Range(-300f,300f), 0 );
    }

    private void Update()
    {
        if (shrink)
            if (gameObject.transform.localScale.x > sizeOnClickTarget) {
                resize = Mathf.Lerp(1f, 0.5f, Time.deltaTime * 3f);
                gameObject.transform.localScale *= resize;
            } else {
                shrink = false;
                gameObject.GetComponent<MeshRenderer>().material = matDefault;
            }

        if ( gameObject.transform.localScale.x < 3f )
            gameObject.transform.localScale *= 1.002f;
    }
}
