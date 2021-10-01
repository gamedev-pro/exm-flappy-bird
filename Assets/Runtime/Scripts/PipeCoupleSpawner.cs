using UnityEngine;

public class PipeCoupleSpawner : MonoBehaviour
{
    [SerializeField] private Pipe bottomPipePrefab;
    [SerializeField] private Pipe topPipePrefab;
    [SerializeField] private float minGapSize = 2.5f;
    [SerializeField] private float maxGapSize = 5;

    [SerializeField] private float maxGapCenterDelta = 10;

    private Pipe bottomPipe;
    private Pipe topPipe;

    public float Width => bottomPipe.Width;

    public void SpawnPipes()
    {
        float gapPosY = transform.position.y + Random.Range(-maxGapCenterDelta, maxGapCenterDelta);
        float gapSize = Random.Range(minGapSize, maxGapSize);

        bottomPipe = Instantiate(bottomPipePrefab, transform.position, Quaternion.identity, transform);
        Vector3 bottomPipePos = bottomPipe.transform.position;
        bottomPipePos.y = (gapPosY - gapSize * 0.5f) - (bottomPipe.Head.y - bottomPipe.transform.position.y);
        bottomPipe.transform.position = bottomPipePos;

        topPipe = Instantiate(topPipePrefab, transform.position, Quaternion.identity, transform);
        Vector3 topPipePos = topPipe.transform.position;
        topPipePos.y = (gapPosY + gapSize * 0.5f) - (topPipe.Head.y - topPipe.transform.position.y);
        topPipe.transform.position = topPipePos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        DrawGap(transform.position + Vector3.up * maxGapCenterDelta);
        DrawGap(transform.position - Vector3.up * maxGapCenterDelta);
    }

    private void DrawGap(Vector3 position)
    {
        Gizmos.DrawCube(position, Vector3.one * 0.5f);
        Gizmos.DrawLine(position - Vector3.up * maxGapSize * 0.5f, position + Vector3.up * maxGapSize * 0.5f);
    }
}
