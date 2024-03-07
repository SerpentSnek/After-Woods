using UnityEngine;

public class FlyEnemyController: MonoBehaviour
{
    public GameObject target;
    private float chaseRange = 3f; 
    private float chaseSpeed = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        Check_Chase();
    }

    void Check_Chase()
    {
        float distanceTotarget = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        if (distanceTotarget <= chaseRange)
        {
            Chasetarget();
        }
    }

    void Chasetarget()
    {
        Vector2 direction = (target.transform.position - transform.position);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
    }
}