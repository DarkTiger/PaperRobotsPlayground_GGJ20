using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour
{
    [SerializeField] GameObject[] envObjects = null;
    [SerializeField] int count = 100;

    private void Start()
    {
        StartCoroutine(SpawnEnvObjects());
    }

    IEnumerator SpawnEnvObjects()
    {
        float currentCount = 0;

        while (currentCount < count)
        {
            GameObject envObject = envObjects[Random.Range(0, envObjects.Length)];
            Vector3 position = transform.position + new Vector3(Random.Range(-400, 400), Random.Range(-400, 400), Random.Range(-400, 400));
            Vector3 direction = (transform.position - position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(position, direction, out hit, LayerMask.GetMask("Planet")))
            {
                Transform envObjectInstantiated = Instantiate(envObject, hit.point, Quaternion.identity).transform;
                envObjectInstantiated.up = (envObjectInstantiated.position - transform.position).normalized;
                currentCount++;
            }
            yield return null;
        }
    }
}
