using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletDamage = 10f;

    [Header("Components")] private Rigidbody rb;
    private EnemyGun enemyGun;

    private LayerMask playerLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyGun = FindObjectOfType<EnemyGun>();
        playerLayerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        Destroy(gameObject, 3f);
        if (enemyGun != null)
        {
            this.rb.velocity = (this.transform.forward * -enemyGun.transform.localPosition.z) * bulletSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == playerLayerMask)
        {
            other.gameObject.GetComponent<Health>().Damage(bulletDamage);
            other.gameObject.GetComponent<HealthDisplay>().a_healthCalc();
            Destroy(this.gameObject);
        }
    }
}