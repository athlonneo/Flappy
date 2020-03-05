using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipeUp, pipeDown;
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] public float holeSize = 1f;
    //[SerializeField] private float maxMinOffset = 1;
    [SerializeField] private Point point;

    private Coroutine CR_Spawn;

    private void Start()
    {
        StartSpawn();
    }

    void StartSpawn()
    {
        if (CR_Spawn == null)
        {
            CR_Spawn = StartCoroutine(IeSpawn());
        }
    }


    void StopSpawn()
    {
        if (CR_Spawn != null)
        {
            StopCoroutine(CR_Spawn);
        }
    }

    void SpawnPipe()
    {
        Pipe newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(180, 0, 0));
        newPipeUp.gameObject.SetActive(true);

        Pipe newPipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);
        newPipeDown.gameObject.SetActive(true);

        newPipeUp.transform.position += Vector3.up * (holeSize / 2);
        newPipeDown.transform.position += Vector3.down * (holeSize / 2);

        //float y = maxMinOffset * Mathf.Sin(Time.time);
        float y = Random.Range(-1.0f, 1.0f);
        newPipeUp.transform.position += Vector3.up * y;
        newPipeDown.transform.position += Vector3.up * y;

        Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
        newPoint.gameObject.SetActive(true);
        //newPoint.SetSize(holeSize);
        //newPoint.transform.position += Vector3.up * y;
    }

    IEnumerator IeSpawn()
    {
        while (true)
        {
            if (bird.IsDead())
            {
                StopSpawn();
            }
            SpawnPipe();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}