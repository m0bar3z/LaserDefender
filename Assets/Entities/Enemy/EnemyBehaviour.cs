using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Health = 150f,ProjectileSpeed = 10f;
    public GameObject Projectile;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scorekeeper;
    
    void Start()
    {
       scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void Update()
    {
        float Probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < Probability)
        {
            Fire();
        }
    }

    public void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0f, -1f, 0f);
        GameObject Missle = Instantiate(Projectile, startPosition, Quaternion.identity) as GameObject;
        Missle.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -ProjectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
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
                AudioSource.PlayClipAtPoint(deathSound,transform.position);
                Destroy(gameObject);
                scorekeeper.addScore(scoreValue);
            }
        }
        
    }
}
