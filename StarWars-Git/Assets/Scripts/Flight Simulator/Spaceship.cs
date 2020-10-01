using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public List<Thruster> thrusters;
    List<Thruster> backThrusters = new List<Thruster>();
    List<Thruster> fronstThrusters = new List<Thruster>();
    List<Thruster> yawRightThrusters = new List<Thruster>();
    List<Thruster> yawleftThruster = new List<Thruster>();
    List<Thruster> barrelRollLeft = new List<Thruster>();
    List<Thruster> barrelRollRight = new List<Thruster>();
    List<Thruster> topFrontThrusters = new List<Thruster>();
    List<Thruster> buttomBackThrusters = new List<Thruster>();
    List<Thruster> leftThrusters = new List<Thruster>();
    List<Thruster> rightThrusters = new List<Thruster>();
    List<Thruster> topThrusters = new List<Thruster>();
    List<Thruster> buttomThrusters = new List<Thruster>();

    void Start()
    {
        foreach (var thruster in FindObjectsOfType<Thruster>())
        {
            thruster.enabled = true;

            if (thruster.transform.localPosition.y > 0 && thruster.transform.localPosition.z == 0)
            {
                this.fronstThrusters.Add(thruster);
            }

            if (thruster.transform.localPosition.y < 0 && thruster.transform.localPosition.z == 0)
            {
                this.backThrusters.Add(thruster);
            }

            if (thruster.transform.localPosition.x > 0 &&
                thruster.transform.localPosition.y > 0 && thruster.transform.localPosition.z == 0)
            {
                this.yawleftThruster.Add(thruster);
            }

            if (thruster.transform.localPosition.x < 0 &&
                thruster.transform.localPosition.y < 0 && thruster.transform.localPosition.z == 0)
            {
                this.yawleftThruster.Add(thruster);
            }

            if (thruster.transform.localPosition.x < 0 &&
                thruster.transform.localPosition.y > 0 && thruster.transform.localPosition.z == 0)
            {
                this.yawRightThrusters.Add(thruster);
            }

            if (thruster.transform.localPosition.x > 0 &&
                thruster.transform.localPosition.y < 0 && thruster.transform.localPosition.z == 0)
            {
                this.yawRightThrusters.Add(thruster);
            }

            if ((thruster.transform.localPosition.x < 0 && thruster.transform.localPosition.z > 0) ||
                (thruster.transform.localPosition.x > 0 && thruster.transform.localPosition.z < 0))
            {
                this.barrelRollLeft.Add(thruster);
            }

            if ((thruster.transform.localPosition.x < 0 && thruster.transform.localPosition.z < 0) ||
                (thruster.transform.localPosition.x > 0 && thruster.transform.localPosition.z > 0))
            {
                this.barrelRollRight.Add(thruster);
            }

            if (thruster.transform.localPosition.z < 0 && thruster.transform.localPosition.y > 0)
            {
                this.topFrontThrusters.Add(thruster);
            }

            if (thruster.transform.localPosition.z > 0 && thruster.transform.localPosition.y > 0)
            {
                this.buttomBackThrusters.Add(thruster);
            }

            if (thruster.transform.localPosition.z == 0 && thruster.transform.localPosition.x > 0)
            {
                this.rightThrusters.Add(thruster);
            }

            if (thruster.transform.localPosition.z == 0 && thruster.transform.localPosition.x < 0)
            {
                this.leftThrusters.Add(thruster);
            }

            if (thruster.transform.localPosition.z < 0)
            {
                topThrusters.Add(thruster);
            }
            if (thruster.transform.localPosition.z > 0)
            {
                buttomThrusters.Add(thruster);
            }
        }
    }

    void Update()
    {
        ResetThrusters();
        ActivateThrusters(KeyCode.W, this.backThrusters);
        ActivateThrusters(KeyCode.S, this.fronstThrusters);
        ActivateThrusters(KeyCode.A, this.yawleftThruster);
        ActivateThrusters(KeyCode.D, this.yawRightThrusters);
        ActivateThrusters(KeyCode.Q, this.barrelRollLeft);
        ActivateThrusters(KeyCode.E, this.barrelRollRight);
        ActivateThrusters(KeyCode.DownArrow, this.topFrontThrusters);
        ActivateThrusters(KeyCode.UpArrow, this.buttomBackThrusters);
        ActivateThrusters(KeyCode.Z, this.leftThrusters);
        ActivateThrusters(KeyCode.X, this.rightThrusters);
        ActivateThrusters(KeyCode.T, this.buttomThrusters);
        ActivateThrusters(KeyCode.G, this.topThrusters);

        if (Input.GetKey(KeyCode.Space))
        {
            DecayAngularVelocity();
            DecayVelocity();

            // TODO 3
            // don't apply velocity directly, instead increase forces of thrusters depending on angular velocity etc.
            var localVelocity = this.transform.InverseTransformVector(GetComponent<Rigidbody2D>().velocity);
        }
    }

    void ResetThrusters()
    {
        foreach (var thruster in this.thrusters)
        {
            thruster.forceMagnitude = 0f;
        }
    }

    void DecayVelocity()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity *= 0.95f;
        }
    }

    void DecayAngularVelocity()
    {
        if (Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity) < 10f)
        {
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().angularVelocity *= 0.95f;
        }
    }

    void ActivateThrusters(KeyCode keyCode, List<Thruster> thrusters)
    {
        if (Input.GetKey(keyCode))
        {
            foreach (var thruster in thrusters)
            {
                thruster.forceMagnitude += 1f;
            }
        }
    }
}