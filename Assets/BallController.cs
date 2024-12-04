using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 20f; // ボールのスピード
    public float gravityScale = 1f; // 重力のスケール調整
    public Transform ballSpawnPoint; // ボールが発射される位置
    public GameObject ballPrefab; // ボールのプレハブ
    public GameObject targetMarkerPrefab; // ターゲット位置を示すマーカーのプレハブ

    private GameObject currentBall; // 現在のボール
    private GameObject currentMarker; // 現在のマーカー
    private Vector3 targetPosition; // ターゲット位置

    void Update()
    {
        // 左クリックでターゲット位置を選択
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        // スペースキーでボールを投げる
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBall();
        }

        // エンターキーでリセット
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetBall();
        }
    }

    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // ターゲット位置を保存
            targetPosition = hit.point;
            Debug.Log($"ターゲット位置: {targetPosition}");
        }
    }

    void ThrowBall()
    {
        if (targetPosition != Vector3.zero)
        {
            // ボールを生成
            currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

            // Rigidbody で速度を設定
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (targetPosition - ballSpawnPoint.position).normalized;
                rb.linearVelocity = direction * speed; // マーカー位置に向かって発射
                rb.useGravity = false; // 初期状態で重力無効
                Debug.Log($"ボールを発射: {direction}");
            }

            // ターゲットマーカーを生成
            if (currentMarker == null)
            {
                currentMarker = Instantiate(targetMarkerPrefab);
                currentMarker.GetComponent<Collider>().enabled = false; // 当たり判定を無効化
            }
            currentMarker.transform.position = targetPosition;
            currentMarker.SetActive(true); // マーカーを表示
        }
        else
        {
            Debug.LogWarning("ターゲット位置が設定されていません！");
        }
    }

    void ResetBall()
    {
        // 現在のボールを削除
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        // ターゲットマーカーを非表示
        if (currentMarker != null)
        {
            currentMarker.SetActive(false);
        }

        Debug.Log("ボールとターゲットマーカーをリセットしました！");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bat") && currentBall != null)
        {
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // バットとの接触後に重力を適用
                rb.mass = gravityScale; // 重力スケールを調整
                Debug.Log("バットに接触。重力を適用しました。");
            }
        }
    }



    
}
