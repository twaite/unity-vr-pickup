using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnObjects : MonoBehaviour
{
    public Transform SphereSpawnPoint;
    public Transform CubeSpawnPoint;
    public GameObject SpherePrefab;
    public GameObject CubePrefab;

    private int ballCount;
    private int pinCount;
    private TextMesh ballsRemaining;
    private TextMesh score;
    private TextMesh winText;
    private TextMesh loseText;

    private void Start()
    {
        pinCount = 0;
        ballCount = 3;

        ballsRemaining = (TextMesh)GameObject.Find("BallsRemaining").GetComponent<TextMesh>();
        UpdateBallsRemaining();

        score = (TextMesh)GameObject.Find("Score").GetComponent<TextMesh>();
        UpdateScore();

        winText = (TextMesh)GameObject.Find("WinText").GetComponent<TextMesh>();
        winText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.gameObject.name == "Sphere")
        {
            ballCount = ballCount - 1;
            UpdateBallsRemaining();
            if (ballCount > 0)
            {
                SpawnSphere(other);
            }
            else if (pinCount != 6)
            {
                winText.text = "You Lose...";
            }

        }

        if (other.isTrigger && other.gameObject.name == "Cube")
        {
            SpawnCube(other);
        }
        if (other.gameObject.CompareTag("Scorable"))
        {
            pinCount = pinCount + 1;
            UpdateScore();
            if (pinCount == 6)
            {
                winText.text = "You Win!!!";
            }
        }
    }

    private void SpawnSphere(Collider sphere)
    {
        Destroy(sphere.gameObject);
        sphere.gameObject.SetActive(false);
        GameObject clone;
        clone = Instantiate(SpherePrefab, SphereSpawnPoint.position, SphereSpawnPoint.rotation);
        clone.gameObject.name = "Sphere";
    }

    private void SpawnCube(Collider cube)
    {
        Destroy(cube.gameObject);
        cube.gameObject.SetActive(false);
        GameObject clone;
        clone = Instantiate(CubePrefab, CubeSpawnPoint.position, CubeSpawnPoint.rotation);
        clone.gameObject.name = "Cube";
    }

    private void UpdateBallsRemaining()
    {
        ballsRemaining.text = "Balls Remaining: " + ballCount.ToString();
    }

    private void UpdateScore()
    {
        score.text = "Score: " + pinCount.ToString();
    }
}
