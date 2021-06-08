using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBody : MonoBehaviour
{
    public static void Create(Transform prefab, Vector3 spawnPosition, Vector3 flyDirection)
    {
        Transform flyingBodyTransform = Instantiate(prefab, spawnPosition, Quaternion.identity);
        FlyingBody flyingBody = flyingBodyTransform.gameObject.AddComponent<FlyingBody>();
        flyingBody.Setup(flyDirection);
    }

    private Vector3 flyDirection;
    private float timer;
    private void Setup(Vector3 flyDirection)
    {
        this.flyDirection = flyDirection;
 //       transform.localScale = Vector3.one * 2f;
    }

    private void Update()
    {
        float flySpeed = 10f;
        transform.position += flyDirection * flySpeed * Time.deltaTime;
        float scaleSpeed = 2f;
        transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        if(timer >= 0.5f)
        {
            Destroy(gameObject);
        }
    }
        
}
