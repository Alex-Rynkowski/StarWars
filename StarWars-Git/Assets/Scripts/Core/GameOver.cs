using System.Collections;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour, IDeactivateScripts
{
    [Tooltip("Attach the GO with scripts whom you want to deactivate (deactivates all scripts on that GO)")]
    [SerializeField]
    private ParticleSystem explosionParticles;

    [SerializeField] private TextMeshProUGUI scoreText;

    private SpaceShipController spaceShipController;
    private CanvasGroup canvasGroup;
    private DeactivateScripts deactivateScripts;
    private Asteroid[] asteroids;
    private MeshRenderer[] meshRenderers;
    private ParticleSystem[] particleSystems;
    private Container container;

    private void Awake()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        particleSystems = spaceShipController.GetComponentsInChildren<ParticleSystem>();
        meshRenderers = spaceShipController.GetComponentsInChildren<MeshRenderer>();
        asteroids = FindObjectsOfType<Asteroid>();
        canvasGroup = GetComponent<CanvasGroup>();
        container = FindObjectOfType<Container>();
    }

    private void OnEnable()
    {
        scoreText.text = $"Your score: {container.score}";

        Cursor.visible = true;
        StartCoroutine(PlayEverything());
    }


    IEnumerator PlayEverything()
    {
        FindObjectOfType<DeactivateScripts>().Deactivate(this);
        yield return SpinOutOfControl();
        yield return PlayExplosion();
        yield return DestroyPlayer();
        yield return DestroyEverythingElse();
        yield return DestroyEnemies();
        yield return ShowText();
    }

    IEnumerator SpinOutOfControl()
    {
        var timer = 0f;
        if (container.wingTag == "Right Wing")
        {
            InstantiateExplosion();
            Destroy(GameObject.FindWithTag("Right Wing"));
            spaceShipController.GetComponent<Rigidbody>().velocity = Vector3.forward * 50;

            while (timer < 2f)
            {
                print("spin to right" + container.wingTag);
                spaceShipController.transform.rotation =
                    Quaternion.Euler(0, 0, spaceShipController.transform.eulerAngles.z + 10);
                yield return null;
                timer += Time.deltaTime;
            }

            FindObjectOfType<CameraFollow>().enabled = false;
        }

        if (container.wingTag == "Left Wing")
        {
            InstantiateExplosion();
            Destroy(GameObject.FindWithTag("Left Wing"));
            spaceShipController.GetComponent<Rigidbody>().velocity = Vector3.forward * 50;
            while (timer < 2f)
            {
                print("spin to right");
                spaceShipController.transform.rotation =
                    Quaternion.Euler(0, 0, spaceShipController.transform.eulerAngles.z + 10);
                yield return null;
                timer += Time.deltaTime;
            }
        }
    }

    private void InstantiateExplosion()
    {
        var explosion = Instantiate(explosionParticles, GameObject.FindWithTag(container.wingTag).transform.position,
            Quaternion.identity);
        var main = explosion.main;
        main.startSize = 10;
        explosion.Play();
        Destroy(explosion, 1f);
    }

    IEnumerator PlayExplosion()
    {
        spaceShipController.GetComponent<Rigidbody>().velocity = Vector3.zero;
        explosionParticles.Play();
        var explosion = explosionParticles.main;
        yield return new WaitForSeconds(explosion.startLifetimeMultiplier - .3f);
    }

    IEnumerator ShowText()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DestroyEnemies()
    {
        foreach (Asteroid asteroid in asteroids)
        {
            Destroy(asteroid.gameObject);
        }

        yield return null;
    }

    IEnumerator DestroyPlayer()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }

        foreach (ParticleSystem particle in particleSystems)
        {
            particle.Stop();
        }

        yield return null;
    }

    public bool DeactivateScripts()
    {
        return true;
    }

    IEnumerator DestroyEverythingElse()
    {
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            Destroy(bullet.gameObject);
        }

        foreach (var bugs in FindObjectsOfType<Enemy>())
        {
            Destroy(bugs.gameObject);
        }

        yield return null;
    }
}