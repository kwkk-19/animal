using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 20f; // ボールのスピード
    public Transform ballSpawnPoint; // ボールが発射される位置
    public GameObject ballPrefab; // ボールのプレハブ

    private GameObject currentBall; // 現在のボール

    void Update()
    {
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

    void ThrowBall()
    {
        if (TargetWall.targetPosition != Vector3.zero)
        {
            // ボールを生成
            currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

            // Rigidbody で速度を設定
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (TargetWall.targetPosition - ballSpawnPoint.position).normalized;
                rb.linearVelocity = direction * speed; // クリック位置に向かって発射
            }
        }
        else
        {
            Debug.LogWarning("ターゲットの位置が設定されていません！");
        }
    }

    void ResetBall()
    {
        // 現在のボールを削除
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        Debug.Log("ボールをリセットしました！");
    }
}
