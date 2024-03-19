using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyPatrol : MonoBehaviour
{
    public Transform target;
    public float activateDistance;
    public float speed;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private Rigidbody2D rb;
    public Transform currentpoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentpoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Debug.Log(currentpoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(!TargetInDistance())
        {
            Patrol();
        }        
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentpoint.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, currentpoint.position) < 0.5f)
        {
            currentpoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        changedirection();
    }

    private void changedirection()
    {
        if (rb.velocity.x > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        else if (rb.velocity.x < -0.01f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
    }


    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

}
