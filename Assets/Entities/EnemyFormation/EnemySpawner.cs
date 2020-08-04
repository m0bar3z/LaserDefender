using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float Height = 10f;
    public float Width = 10f;
    public float Speed = 10f;
    public float SpawnDelay = 0.5f;
    
    private bool movingRight = true;
    private float Xmax, Xmin;
    void Start()
    {
        float CameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,CameraDistance));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, CameraDistance));
        Xmax = rightEdge.x;
        Xmin = leftEdge.x;
        SpawnUntilFull();
    }

    void Update()
    {
        MoveEnemyFormation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0f,1.5f,0f),new Vector3(Width,Height));
    }

    public void CreateEnemy()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;    
        }
        
    }

    public void SpawnUntilFull()
    {
        Transform FreePosition = NextFreePosition();
        if (FreePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, FreePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = FreePosition;
        }

        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", SpawnDelay);
        }
    }

    public void MoveEnemyFormation()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        } else {
            
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (Width * 0.5f);
        float leftEdgeOfFormation = transform.position.x -  (Width * 0.5f);

        if (leftEdgeOfFormation < Xmin || rightEdgeOfFormation > Xmax)
        {
            movingRight = !movingRight;
        }

        if (AllMembersDead())
        {
         SpawnUntilFull();
        }  
    }

    Transform NextFreePosition()
    {
        foreach (Transform ChildTransform in transform)
        {
            if (ChildTransform.childCount == 0)
            {
                return ChildTransform;
            }
        }

        return null;
    }
    
    public bool AllMembersDead()
    {
        foreach (Transform ChildTransform in transform)
        {
            if (ChildTransform.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }
}
