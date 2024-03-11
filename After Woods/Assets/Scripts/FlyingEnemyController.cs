using UnityEngine;

public class FlyingEnemyController: MonoBehaviour, IDamage
{
    [SerializeField] private float chaseRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float damage;
    [SerializeField] private GameObject target;

    public float Damage()
    {
        return damage;
    }

    void Start()
    {
        // target = GameManager.Instance.Player;
    }

    void Update()
    {
        if (IsInRange())
        {
            Chase();
        }
    }

    private bool IsInRange()
    {
        float distanceToTarget = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        if (distanceToTarget <= chaseRange)
        {
            return true;
        }
        return false;
    }

    private void Chase()
    {
        Vector2 direction = (target.transform.position - transform.position);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
    }
}