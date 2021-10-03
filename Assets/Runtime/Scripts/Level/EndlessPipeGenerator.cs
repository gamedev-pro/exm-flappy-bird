using System.Collections.Generic;
using UnityEngine;

public class EndlessPipeGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private Ground[] grounds;

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

    public void StartPipeSpawn()
    {
        SpawnPipe(player.transform.position + Vector3.right * initialDistanceWithoutPipes);
        SpawnPipes(minPipesInFrontOfPlayer - 1);
    }

    private void Update()
    {
        UpdatePipes();
        UpdateGround();
    }

    private void UpdateGround()
    {
        int lastIndex = grounds.Length - 1;
        for (int i = lastIndex; i >= 0; i--)
        {
            Ground ground = grounds[i];

            if (player.transform.position.x > ground.Sprite.bounds.min.x && !IsBoxVisibleXOnly(ground.Sprite.bounds.center, ground.Sprite.bounds.size.x))
            {
                Ground lastGround = grounds[lastIndex];
                ground.transform.position = lastGround.transform.position + Vector3.right * ground.Sprite.bounds.size.x;
                grounds[i] = lastGround;
                grounds[lastIndex] = ground;
            }
        }
    }

    private void UpdatePipes()
    {
        if (pipes.Count > 0)
        {
            PipeCoupleSpawner lastPipe = pipes[pipes.Count - 1];
            if (IsPipeVisible(lastPipe))
            {
                SpawnPipes(minPipesInFrontOfPlayer);
            }

            int lastIndexToRemove = FindLastPipeIndexToRemove();
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

    private int FindLastPipeIndexToRemove()
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
        return IsBoxVisibleXOnly(pipe.transform.position, pipe.Width);
    }

    private bool IsBoxVisibleXOnly(Vector3 center, float width)
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
            Gizmos.DrawWireCube(ground.transform.position, ground.Sprite.bounds.size);
        }
    }
}
