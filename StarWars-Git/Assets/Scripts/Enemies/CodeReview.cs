using UnityEngine;

public class CoreRev : MonoBehaviour
{
    // I like to remove unnecessary private keywords.
    // This highlights public keywords further.
    // If not done, then public float and private float
    // looks almost the same
    [Header("Raycast adjustments")] [SerializeField]
    private float rayAdjustmentYPosition;

    [Header("Space ship adjustments")] [SerializeField]
    private float forceMagnitude = 1;
    [SerializeField] private float maxYRotation;
    [SerializeField] private float rotationMagnitudeZAxis;

    [Header("Components")] 
    private GameManager gameManager;
    private Rigidbody rb;
    // the name camera hides a public base member, which is also called camera.
    // It is not recommended to do this. If you still want to - and here, it
    // would kinda mak sense, use the new keyword: new Camera camera;
    private new Camera camera;
    [Header("Other variables")] private Ray ray;
    float rotationCounter;
    float currentZRot;
    [HideInInspector] public Vector3 rayEndPoint;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (rotationCounter >= 360 || rotationCounter <= -360)
        {
            // I assume, that the right thing to do here, would be to use
            // the rotationCounter %= 360, or just add 360 or subtract 360 from it
            // I think that setting it to ZERO cuts off some of the input uninten-
            // tionally, like: before: 365, then: 0 instead of then: 5
            rotationCounter %= 360;
        }

        FollowMousePosition();
    }

    // If you need regions in your code, think about making anotheer class out of this
    #region Ray casting

    // Return value is not being used, can be omitted
    private void FollowMousePosition()
    {
        // The ray is assigned to freshly every frame
        // Which means, that it does not have to be a field of the class
        // Just work with var ray = ...
        // Then pass the ray into the other methods
        // Only variables, that you want to read in the next update, or
        // from somewhere else, make sense to be fields of the class
        this.ray = camera.ScreenPointToRay(Input.mousePosition);
        rayEndPoint = ray.origin + ray.direction * 70; //used in wave spawner script

        // I recommend to use var here.
        var lookRot =
            // Use ray.direction + new Vector3(0f, rayAdjustmentYPosition, 0f)
            // or even better: ray.direction + rayAdjustment (which is a Vector3)
            Quaternion.LookRotation(
                new Vector3(0, rayAdjustmentYPosition, 0),
                Vector3.up);

        // This looks very complicated, might be correct, I don't even know, what's happening here, exactly. Maybe, create a method with a name to describe what's happening?
        // Apparently, you're hardly setting the rotation, without use of physical force.
        // You're rotating 0 around a scaled axis. Doesn't really make sense, I think.
        this.transform.rotation = Quaternion.AngleAxis(0,
            new Vector3(transform.eulerAngles.x * 1, transform.eulerAngles.y * forceMagnitude,
                transform.eulerAngles.z * 1)) * lookRot;

        // Good to use methods here!
        SetYRotationLimit(lookRot);
        RotateInZAxis();

        // now you set the transform's rotation again? You can remove all the multiplications with one.
        // Maybe, say this here:
        // var currentRotation = transform.eulerAngles;
        // currentRotation.z = currentZRot;
        // transform.rotation = currentRotation;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x * 1, this.transform.eulerAngles.y * 1,
            currentZRot);
    }

    private Ray SetYRotationLimit(Quaternion lookRot)
    {
        // simplify the ifs:
        // instead of if(A && B){} else if (A && C){} else{}
        // do: if(A){if(B){} else if(C){}} else {}
        if (Quaternion.Angle(Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z), lookRot) >
            maxYRotation && lookRot.y > 0)
        {
            this.ray = camera.ScreenPointToRay(
                (new Vector3(maxYRotation, Input.mousePosition.y, Input.mousePosition.z)));

            SpaceShipVelocity(new Vector3(maxYRotation / 100, this.ray.direction.y + this.rayAdjustmentYPosition,
                this.ray.direction.z * 1.3f) * this.forceMagnitude);
            this.transform.rotation = Quaternion.Euler(transform.eulerAngles.x * 1, maxYRotation,
                transform.eulerAngles.z * 1 * rotationCounter);
        }

        else if (Quaternion.Angle(Quaternion.Euler(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z),
                lookRot) >
            this.maxYRotation && lookRot.y < 0)
        {
            this.ray = camera.ScreenPointToRay((new Vector3(-.1f, Input.mousePosition.y, Input.mousePosition.z)));

            SpaceShipVelocity(new Vector3(-this.maxYRotation / 100, this.ray.direction.y + this.rayAdjustmentYPosition,
                this.ray.direction.z * 1.3f) * forceMagnitude);

            this.transform.rotation =
                Quaternion.Euler(transform.eulerAngles.x * 1, -maxYRotation, transform.eulerAngles.z * 1);
        }
        else
        {
            SpaceShipVelocity(new Vector3(this.ray.direction.x, this.ray.direction.y + this.rayAdjustmentYPosition,
                this.ray.direction.z) * this.forceMagnitude);
        }

        return ray;
    }

    // This method should be called SetVelocity()
    public void SpaceShipVelocity(Vector3 velocity)
    {
        this.rb.velocity = velocity;
    }

    // The return value is not being used and can be omitted
    private float RotateInZAxis()
    {
        if (Input.GetKey(KeyCode.E))
        {
            this.rotationCounter += this.rotationMagnitudeZAxis * Time.deltaTime;
            this.transform.rotation =
                Quaternion.Euler(this.transform.eulerAngles.x * 1, this.transform.eulerAngles.y * 1,
                    this.transform.eulerAngles.z + this.rotationCounter);

            this.currentZRot = this.transform.eulerAngles.z;
        }

        if (Input.GetKey(KeyCode.Q))
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