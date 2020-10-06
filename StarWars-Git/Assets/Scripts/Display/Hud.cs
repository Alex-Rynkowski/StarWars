using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] SpaceShipController spaceShipController;
    public GameObject gameoverScreen; //activates through the Health script

    [Header("Buttons")] [SerializeField] Button startGameBtn;
    [SerializeField] GameObject pauseMenu;

    [Header("Components")] Container container;

    private void Start()
    {
        container = FindObjectOfType<Container>();
        spaceShipController.gameObject.SetActive(false);
        startGameBtn.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        gameoverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !pauseMenu.activeInHierarchy &&
            !startGameBtn.gameObject.activeInHierarchy)
        {
            EnemyActivation(false);
            FreezeBullets(true);
            Cursor.visible = true;
            spaceShipController.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            container.inPlayMode = false;
            pauseMenu.SetActive(true);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape) && pauseMenu.activeInHierarchy &&
            !startGameBtn.gameObject.activeInHierarchy)
        {
            ResumeGame();
            return;
        }

        scoreText.text = string.Format("Score: {0:0}", container.score);

        if (container.paused)
        {
            spaceShipController.SetVelocity(new Vector3(0, 0, 0));
        }
    }

    public void StartGame()
    {
        startGameBtn.gameObject.SetActive(false);
        container.inPlayMode = true;
        spaceShipController.gameObject.SetActive(true);
        Cursor.visible = false;
    }

    public void ActivateWindow(GameObject windowToActivate)
    {
        windowToActivate.SetActive(true);
    }

    public void DeActivateWindow(GameObject windowToActivate)
    {
        windowToActivate.SetActive(false);
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        spaceShipController.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        container.inPlayMode = true;
        pauseMenu.SetActive(false);
        EnemyActivation(true);
        FreezeBullets(false);
    }

    private void EnemyActivation(bool shouldActivateEnemies)
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            if (shouldActivateEnemies)
            {
                enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            else
            {
                enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            foreach (var script in enemy.GetComponentsInChildren<MonoBehaviour>())
            {
                script.enabled = shouldActivateEnemies;
            }
        }
    }

    private void FreezeBullets(bool shouldFreezeBullets)
    {
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            if (shouldFreezeBullets)
            {
                bullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                bullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}