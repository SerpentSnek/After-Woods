using UnityEngine;

public class Invincibility : MonoBehaviour
{

    void Update()
    {
        var lc = GameManager.Instance.Player.GetComponent<PlayerLogicController>();
        if (lc.CurrentHp < 0.2f * lc.TotalHp)
        {
            lc.CurrentHp = 0.5f * lc.TotalHp;
        }

        if (lc.CurrentRadiation > 0.6f * lc.TotalRadiation)
        {
            lc.CurrentRadiation = 0.1f * lc.TotalRadiation;
        }
    }

}