using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 15.0f;
    public float Health = 210f;
    public float ProjectileSpeed;
    public float fireRate;
    public GameObject Projectile;
    public AudioClip fireSound; 
   
    
    float Xmin ;
    float Xmax ;
    void Start()
    {
        float Distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 LeftMost = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,Distance));
        Vector3 RightMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, Distance));
        Xmin = LeftMost.x;
        Xmax = RightMost.x;
    }

    
    void Update()
    {
        /*
        float mPosition = Input.mousePosition.x;
        print("mPosition: " + mPosition);
        float transMposition = Camera.main.ViewportToWorldPoint(new Vector3(1f,1f,0f)).x;
        print("transMposition: " + transMposition);
        */
     Move();
     Shooting();
    }

    

    public void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           // transform.position += new Vector3(-Speed * Time.deltaTime,0f,0f);
            transform.position += Vector3.left * Speed * Time.deltaTime;
        } 
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(Speed * Time.deltaTime, 0f, 0f); 
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x,Xmin,Xmax);
        transform.position = new Vector3(newX,transform.position.y,transform.position.z);
    }

    public void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           InvokeRepeating("Fire",0.000001f,fireRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    public void Fire()
    {
        Vector3 startPosition = new Vector3(0f,1f,0f);
        GameObject Beam = Instantiate(Projectile, transform.position + startPosition, Quaternion.identity) as GameObject;
        Beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, ProjectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound,transform.position);
        
    }

    public void Die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Win Screen");
        Destroy(gameObject);   
    }
    
    private void OnTriggerEnter2D(Collider2D Colid)
    {
        Projectile Missle = Colid.gameObject.GetComponent<Projectile>();
        
        if (Missle)
        {
            Health -= Missle.GetDamage();
            Missle.Hit();
            if (Health <= 0)
            {
               Die();
            }
        }
        
    }
    
}



