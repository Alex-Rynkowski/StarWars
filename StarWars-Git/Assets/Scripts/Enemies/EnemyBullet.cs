using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletDamage = 10f;
    
    [Header("Components")]
    private Rigidbody rb;
    private EnemyGun enemyGun;
    
    private LayerMask playerLayerMask;
    private Vector3 moveForward;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyGun = FindObjectOfType<EnemyGun>();
        playerLayerMask = LayerMask.GetMask("Player");
        

        moveForward = -enemyGun.transform.forward;
    }

    private void Update()
    {
        Destroy(gameObject, 3f);
        rb.velocity = moveForward * bulletSpeed;    
        
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