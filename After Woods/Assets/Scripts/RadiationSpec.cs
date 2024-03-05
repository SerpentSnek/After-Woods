using UnityEngine;

public class RadiationSpec : MonoBehaviour
{
    [SerializeField] private float radiationDamage;

    public RadiationSpec()
    {
        this.radiationDamage = 0.01f;
    }
    public float RadiationDamage
    {
        get => radiationDamage;
        set => radiationDamage = value;
    }
}
