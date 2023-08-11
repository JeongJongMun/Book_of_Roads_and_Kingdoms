using UnityEngine;

public class KoranController : SkillController
{
    public override int _weaponType { get { return (int)Define.Skills.Koran; } }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().OnDamaged(_damage, _force);
            Debug.LogFormat("{0} {1}",_damage, _force);
        }
    }
}
