using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 20f; // ボールのスピード
    public float gravityScale = 1f; // 通常の重力スケール
    public Transform ballSpawnPoint; // ボールの発射位置
    public GameObject ballPrefab; // ボールのプレハブ
    public GameObject targetMarkerPrefab; // ターゲット位置を示すマーカー
    public Collider strikeZoneCollider; // ストライクゾーンのコライダー（必要に応じて使用）

    private GameObject currentBall;
    private GameObject currentMarker;
    private Vector3 targetPosition;

    void Update()
    {
        // 左クリックでターゲット位置設定
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPositionFromMouse();
        }

        // スペースでボールを投げる
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBall();
        }

        // エンターでリセット
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetBall();
        }

        // Eキーでストライクゾーン内ランダム位置へ投げる
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetRandomTargetInStrikeZone();
            ThrowBall();
        }
    }

    void SetTargetPositionFromMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPosition = hit.point;
            Debug.Log($"ターゲット位置: {targetPosition}");
        }
    }

    void SetRandomTargetInStrikeZone()
    {
        if (strikeZoneCollider == null)
        {
            Debug.LogError("StrikeZoneCollider が設定されていません！");
            return;
        }

        Bounds bounds = strikeZoneCollider.bounds;
        float randX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float randY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float randZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        targetPosition = bounds.center + new Vector3(randX, randY, randZ);
        Debug.Log($"ランダムターゲット位置: {targetPosition}");
    }

    void ThrowBall()
    {
        if (targetPosition != Vector3.zero)
        {
            currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (targetPosition - ballSpawnPoint.position).normalized;
                rb.linearVelocity = direction * speed; 
                
                // 無重力で投げる
                rb.useGravity = false; 
                Debug.Log($"ボール発射: {direction} (無重力で飛行中)");
            }

            if (currentMarker == null)
            {
                currentMarker = Instantiate(targetMarkerPrefab);
                currentMarker.GetComponent<Collider>().enabled = false;
            }
            currentMarker.transform.position = targetPosition;
            currentMarker.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ターゲット位置が設定されていません！");
        }
    }

    void ResetBall()
    {
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        if (currentMarker != null)
        {
            currentMarker.SetActive(false);
        }

        targetPosition = Vector3.zero;
        Debug.Log("ボールとターゲットマーカーをリセットしました！");
    }

    void OnCollisionEnter(Collision collision)
    {
        // バットとの衝突後に重力を適用
        if (collision.gameObject.CompareTag("Bat") && currentBall != null)
        {
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // 重力を適用
                Debug.Log("バットと接触。重力を適用しました。");
            }
        }
    }
}
