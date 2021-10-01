using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPipeGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private SpriteRenderer[] grounds;
    [SerializeField] private float groundCenterDistance = 9;

    [Space]
    [Header("Pipes")]

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

        int lastIndex = grounds.Length - 1;
        for (int i = lastIndex; i >= 0; i--)
        {
            SpriteRenderer ground = grounds[i];

            if (player.transform.position.x > ground.bounds.min.x && !IsBoxVisible(ground.bounds.center, ground.bounds.size.x))
            {
                SpriteRenderer lastGround = grounds[lastIndex];
                ground.transform.position = lastGround.transform.position + Vector3.right * ground.bounds.size.x;
                grounds[i] = lastGround;
                grounds[lastIndex] = ground;
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
        return IsBoxVisible(pipe.transform.position, pipe.Width);
    }

    private bool IsBoxVisible(Vector3 center, float width)
    {
        Vector3 left = center - Vector3.right * width * 0.5f;
        Vector3 right = center + Vector3.right * width * 0.5f;

        Vector3 leftClipPos = mainCamera.WorldToViewportPoint(left);
        Vector3 rightClipPos = mainCamera.WorldToViewportPoint(right);

        return !(leftClipPos.x > 1 || rightClipPos.x < 0);
    }

    private void OnDrawGizmos()
    {
        foreach (var ground in grounds)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(ground.transform.position, ground.bounds.size);
        }
    }
}
