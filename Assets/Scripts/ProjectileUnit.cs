using UnityEngine;

public class ProjectileUnit : MonoBehaviour
{
    public int ProjectileDamage;
    public int SplashDamage;
    public float SpashRadius;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyUnit = collision.gameObject.GetComponent<EnemyUnit>();
            enemyUnit.Hit(ProjectileDamage);
        }
        else
        {
            //Check if enemies in splash range.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, SpashRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Enemy"))
                {
                    EnemyUnit enemyUnit = hitCollider.gameObject.GetComponent<EnemyUnit>();
                    enemyUnit.Hit(ProjectileDamage);
                }
            }
        }
        Destroy(gameObject);
        MainManager.Instance.StartDestroyAnimation(transform.position);
    }
}
