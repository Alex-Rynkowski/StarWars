using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Tooltip("Set timer before changing position")] [SerializeField]
    private float changePositionTimer = 5;


    private float positionTimer;
    private float XPos;
    private float YPos;

    [Header("Components")] private SpaceShipController spaceShipController;
    private Container container;

    private void Start()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        container = FindObjectOfType<Container>();
        positionTimer = changePositionTimer;
    }

    private void Update()
    {
        container.score += 10 * Time.deltaTime;
        positionTimer += Time.deltaTime;

        if (positionTimer >= changePositionTimer)
        {
            var position = spaceShipController.transform.position;
            XPos = Random.Range(position.x - 20, position.x + 20);
            YPos = Random.Range(position.y - 20, position.y + 20);
            positionTimer = 0;
        }


        FollowPlayerMovement();
        MoveToPosition();
    }

    private void FollowPlayerMovement()
    {
        this.transform.LookAt(spaceShipController.transform.position);
    }

    private Vector3 MoveToPosition()
    {
        var position = this.transform.position;
        return this.transform.position = new Vector3(
            Mathf.MoveTowards(position.x, XPos, 10f * Time.deltaTime),
            Mathf.MoveTowards(position.y, YPos, 10f * Time.deltaTime),
            spaceShipController.transform.position.z + 40);
    }
}