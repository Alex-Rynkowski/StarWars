using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    private SpaceShipController spaceShipController;

    private float xRot;
    private float yRot;
    private float zRot;

    [SerializeField] private float minXScale = .5f;
    [SerializeField] private float minYScale = .5f;
    [SerializeField] private float minZScale = .5f;

    [SerializeField] private float maxXScale = 3f;
    [SerializeField] private float maxYScale = 3f;
    [SerializeField] private float maxZScale = 3f;

    Health health;
    Renderer renderer1;
    Container container;

    private void Start()
    {
        renderer1 = this.GetComponent<Renderer>();
        health = GetComponent<Health>();
        SetEularRotation();
        SetScale();
        CalculateHealth();
        spaceShipController = FindObjectOfType<SpaceShipController>();

        renderer1.material.color = new Color(1, 1, 1, 0);
        
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, spaceShipController.transform.position) < 150)
        {
            StartCoroutine(ChangeAlpha());
        }

        if (health.CurrentHealth() <= 0)
        {
            Destroy(gameObject);
        }

        if (this.transform.position.z + 30 < spaceShipController.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }

    private float CalculateHealth()
    {
        return health.maxHealth *= (maxXScale + maxYScale + maxZScale) / 3;
    }

    private void SetScale()
    {
        maxXScale = Random.Range(minXScale, maxXScale);
        maxYScale = Random.Range(minYScale, maxYScale);
        maxZScale = Random.Range(minZScale, maxZScale);
        this.transform.localScale = new Vector3(maxXScale, maxYScale, maxZScale);
    }

    private void SetEularRotation()
    {
        xRot = Random.Range(0, 360);
        yRot = Random.Range(0, 360);
        zRot = Random.Range(0, 360);
        this.transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    IEnumerator ChangeAlpha()
    {
        var a = renderer1.material.color.a;
        while (a < 1)
        {
            a += Time.deltaTime;
            renderer1.material.color = new Color(1, 1, 1, a);
        }

        yield return null;
    }
}