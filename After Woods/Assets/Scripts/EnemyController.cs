using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { Fly, Ground }
    public EnemyType enemyType;

    private GameObject player;
    private float chaseRange; 
    private float chaseSpeed = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AdjustChaseRange();
    }

    void Update()
    {
        Check_Chase();
    }

    void AdjustChaseRange()
    {
        if (enemyType == EnemyType.Fly)
        {
            chaseRange = 3f; 
        }
        else if (enemyType == EnemyType.Ground)
        {
            chaseRange = 5f;
        }
    }

    void Check_Chase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.transform.position - transform.position);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }
}