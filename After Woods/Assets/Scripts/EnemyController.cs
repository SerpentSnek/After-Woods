using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float damageOutput;

    public float DamageOutput
    {
        get => damageOutput;
        set => damageOutput = value;
    }

}
