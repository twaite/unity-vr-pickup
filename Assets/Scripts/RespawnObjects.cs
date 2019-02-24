using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObjects : MonoBehaviour
{
    public Transform SphereSpawnPoint;
    public Transform CubeSpawnPoint;
    public GameObject SpherePrefab;
    public GameObject CubePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.gameObject.name == "Sphere")
        {
            SpawnSphere(other);
        }

        if (other.isTrigger && other.gameObject.name == "Cube")
        {
            SpawnCube(other);
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
}
