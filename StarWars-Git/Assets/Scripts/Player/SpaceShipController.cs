using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [Header("Raycast adjustments")] [SerializeField]
    float rayAdjustmentYPosition;

    [Header("Space ship adjustments")] [SerializeField]
    float forceMagnitude = 1;
    [SerializeField] float maxYRotation;
    [SerializeField] float rotationMagnitudeZAxis;

    [Header("Components")] 
    Container container;
    Rigidbody rb;
    Camera camera;

    [Header("Other variables")]
    float rotationCounter;
    float currentZRot;
    [HideInInspector] public Vector3 rayEndPoint;

    void Start()
    {
        container = FindObjectOfType<Container>();
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    void Update()
    {
        if (!container.inPlayMode) return;

        if (rotationCounter >= 360 || rotationCounter <= -360)
        {
            rotationCounter %= 360;
        }

        FollowMousePosition();
    }

    #region Ray casting

    void FollowMousePosition()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        rayEndPoint = ray.origin + ray.direction * 70; //used in wave spawner script

        Quaternion lookRot =
            Quaternion.LookRotation(
                new Vector3(ray.direction.x, ray.direction.y + rayAdjustmentYPosition, ray.direction.z),
                Vector3.up);

        KeepZAxisRotationStedy(lookRot);
        SetYRotationLimit(lookRot, ray);
        RotateInZAxis();

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, this.transform.eulerAngles.y,
            currentZRot);
    }

    void KeepZAxisRotationStedy(Quaternion lookRot)
    {
        this.transform.rotation = Quaternion.AngleAxis(0, Vector3.up * forceMagnitude) * lookRot;
    }

    void SetYRotationLimit(Quaternion lookRot, Ray ray)
    {
        if (Quaternion.Angle(Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z), lookRot) >
            maxYRotation && lookRot.y > 0)
        {
            ray = camera.ScreenPointToRay(
                (new Vector3(maxYRotation, Input.mousePosition.y, Input.mousePosition.z)));

            SetVelocity(new Vector3(maxYRotation / 100, ray.direction.y + this.rayAdjustmentYPosition,
                ray.direction.z * 1.3f) * this.forceMagnitude);
            this.transform.rotation = Quaternion.Euler(transform.eulerAngles.x * 1, maxYRotation,
                transform.eulerAngles.z * 1 * rotationCounter);
        }

        else if (Quaternion.Angle(Quaternion.Euler(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z),
                lookRot) >
            this.maxYRotation && lookRot.y < 0)
        {
            ray = camera.ScreenPointToRay((new Vector3(-.1f, Input.mousePosition.y, Input.mousePosition.z)));

            SetVelocity(new Vector3(-this.maxYRotation / 100, ray.direction.y + this.rayAdjustmentYPosition,
                ray.direction.z * 1.3f) * forceMagnitude);

            this.transform.rotation =
                Quaternion.Euler(transform.eulerAngles.x * 1, -maxYRotation, transform.eulerAngles.z * 1);
        }
        else
        {
            SetVelocity(new Vector3(ray.direction.x, ray.direction.y + this.rayAdjustmentYPosition,
                ray.direction.z) * this.forceMagnitude);
        }

    }

    public void SetVelocity(Vector3 velocity)
    {
        this.rb.velocity = velocity;
    }

    private float RotateInZAxis()
    {
        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            this.rotationCounter += this.rotationMagnitudeZAxis * Time.deltaTime;
            this.transform.rotation =
                Quaternion.Euler(this.transform.eulerAngles.x * 1, this.transform.eulerAngles.y * 1,
                    this.transform.eulerAngles.z + this.rotationCounter);

            this.currentZRot = this.transform.eulerAngles.z;
        }

        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            this.rotationCounter -= this.rotationMagnitudeZAxis * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x * 1,
                this.transform.eulerAngles.y * 1, this.transform.eulerAngles.z * 1 + this.rotationCounter);

            this.currentZRot = transform.eulerAngles.z;
        }

        return currentZRot;
    }

    #endregion
}