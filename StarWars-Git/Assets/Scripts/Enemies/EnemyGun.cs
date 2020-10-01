using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float minFireWaitTime = .5f;
    [SerializeField] private float maxFireWaitTime = 1.5f;
    private float waitTime;
    
    private SpaceShipController spaceShipController;

    private float fireCounter;

    public Vector3 moveTowards;
    private void Start()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        fireCounter = Random.Range(minFireWaitTime, maxFireWaitTime);
    }

    private void Update()
    {
        fireCounter += Time.deltaTime;
        if (fireCounter >= waitTime)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            waitTime = Random.Range(minFireWaitTime, maxFireWaitTime);
            fireCounter = 0;
        }

        var position = spaceShipController.transform.position;
        this.transform.LookAt(new Vector3(position.x, position.y, position.z + 20));
    }

}
