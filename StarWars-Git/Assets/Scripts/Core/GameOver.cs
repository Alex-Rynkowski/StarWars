using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour, IDeactivateScripts
{
    [Tooltip("Attach the GO with scripts whom you want to deactivate (deactivates all scripts on that GO)")]
    [SerializeField] private ParticleSystem explosionParticles;

    private SpaceShipController spaceShipController;
    private CanvasGroup canvasGroup;
    private DeactivateScripts deactivateScripts;
    private Asteroid[] asteroids;
    private MeshRenderer meshRenderer;
    private ParticleSystem[] particleSystems;

    private void Start()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        particleSystems = spaceShipController.GetComponentsInChildren<ParticleSystem>();
        meshRenderer = spaceShipController.GetComponent<MeshRenderer>();
        asteroids = FindObjectsOfType<Asteroid>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void OnEnable()
    {
        FindObjectOfType<DeactivateScripts>().Deactivate(this);
        Cursor.visible = true;
        StartCoroutine(PlayEverything());
    }

    IEnumerator PlayEverything()
    {
        yield return PlayExplosion();
        yield return DestroyPlayer();
        yield return DestroyEnemies();
        yield return ShowText();
    }

    IEnumerator PlayExplosion()
    {
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
        foreach (Asteroid enemy in asteroids)
        {
            if (enemy != null)
            {
                enemy.enabled = false;
                Destroy(enemy.gameObject);                
            }
            
        }

        yield return null;
    }

    IEnumerator DestroyPlayer()
    {
        meshRenderer.enabled = false;
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
}