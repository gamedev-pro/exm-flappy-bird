using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPipeGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private PipeCoupleSpawner pipeSpawnerPrefab;

    [SerializeField] private float initialDistanceWithoutPipes;

    [SerializeField] private int minPipesInFrontOfPlayer = 2;

    [Space]
    [Header("Random Parameters")]
    [SerializeField] private float minDistanceBetweenPipes;
    [SerializeField] private float maxDistanceBetweenPipes;

    private List<PipeCoupleSpawner> pipes = new List<PipeCoupleSpawner>();

    private void Start()
    {
        SpawnPipe(transform.position + Vector3.right * initialDistanceWithoutPipes);
        SpawnPipes(minPipesInFrontOfPlayer - 1);
    }

    private void Update()
    {
        if (pipes.Count > 0)
        {
            PipeCoupleSpawner lastPipe = pipes[pipes.Count - 1];
            if (IsPipeVisible(lastPipe))
            {
                SpawnPipes(minPipesInFrontOfPlayer);
            }

            int lastIndexToRemove = FinddLastPipeIndexToRemove();
            if (lastIndexToRemove >= 0)
            {
                for (int i = 0; i <= lastIndexToRemove; i++)
                {
                    Destroy(pipes[i].gameObject);
                }
                pipes.RemoveRange(0, lastIndexToRemove + 1);
            }
        }
    }

    public void SpawnPipes(int pipeCount)
    {
        //we assume there is at least one pipe to go from
        if (pipes.Count == 0)
        {
            Debug.LogError("Expected at least one pipe to start from");
        }

        PipeCoupleSpawner lastPipe = pipes[pipes.Count - 1];
        for (int i = 0; i < pipeCount; i++)
        {
            Vector2 newPipePos = lastPipe.transform.position + Vector3.right * Random.Range(minDistanceBetweenPipes, maxDistanceBetweenPipes);
            lastPipe = SpawnPipe(newPipePos);
        }
    }

    private PipeCoupleSpawner SpawnPipe(Vector2 position)
    {
        PipeCoupleSpawner pipe = Instantiate(pipeSpawnerPrefab, position, Quaternion.identity, transform);
        pipe.name = $"PipeSpawner {pipes.Count}";
        pipes.Add(pipe);
        pipe.SpawnPipes();
        return pipe;
    }

    private int FinddLastPipeIndexToRemove()
    {
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            PipeCoupleSpawner pipe = pipes[i];
            if (pipe.transform.position.x < player.transform.position.x && !IsPipeVisible(pipe))
            {
                return i;
            }
        }

        return -1;
    }

    private bool IsPipeVisible(PipeCoupleSpawner pipe)
    {
        Vector3 startPipe = pipe.transform.position - Vector3.right * pipe.Width * 0.5f;
        Vector3 endPipe = pipe.transform.position + Vector3.right * pipe.Width * 0.5f;

        Debug.Log($"{pipe.name}, {mainCamera.WorldToViewportPoint(startPipe)}, {mainCamera.WorldToViewportPoint(endPipe)}");
        return IsPointInCameraFrustum(startPipe) || IsPointInCameraFrustum(endPipe);
    }

    private bool IsPointInCameraFrustum(Vector3 point)
    {
        Vector3 clipPos = mainCamera.WorldToViewportPoint(point);
        return clipPos.z > 0 && clipPos.x >= 0 && clipPos.x <= 1;
    }
}
