using UnityEngine;

public class StrikeZoneTrigger : MonoBehaviour
{
    public GameStatusUI gameStatusUI; // ゲームステータス管理スクリプト
    public GameObject targetWall;    // ターゲットウォールの参照

    private Collider currentBall;    // 現在処理中のボール
    private bool passedStrikeZone = false; // ストライクゾーンを通過したか
    private bool passedTargetWall = false; // ターゲットウォールを通過したか

    void OnTriggerEnter(Collider other)
    {
        // ボールがストライクゾーンに入った場合
        if (other.CompareTag("Ball") && currentBall == null)
        {
            currentBall = other; // ボールを記録
            Debug.Log("ボールがストライクゾーンに入りました！");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ボールがストライクゾーンを通過した場合
        if (other.CompareTag("Ball") && currentBall == other)
        {
            passedStrikeZone = true; // ストライクゾーンを通過したことを記録
            Debug.Log("ボールがストライクゾーンを通過しました！（ストライク判定）");
            gameStatusUI.AddStrike(); // ストライクをカウント
            ResetBallState(); // 状態をリセット
        }
    }

    void Update()
    {
        // ボールがターゲットウォールを通過したかを確認
        if (currentBall != null && currentBall.transform.position.z > targetWall.transform.position.z)
        {
            if (!passedTargetWall) // 初回通過時のみログ出力
            {
                passedTargetWall = true; // ターゲットウォールを通過
                Debug.Log("ボールがターゲットウォールを通過しました！");
            }
        }

        // ボールがターゲットウォールを通過し、ストライクゾーンを通過していない場合にボール判定
        if (passedTargetWall && !passedStrikeZone && currentBall != null)
        {
            Debug.Log("ボールがストライクゾーンを通過せず、ターゲットウォールを通過しました！（ボール判定）");
            gameStatusUI.AddBall(); // ボールをカウント
            ResetBallState(); // 状態をリセット
        }
    }

    private void ResetBallState()
    {
        // 状態リセット
        currentBall = null;
        passedStrikeZone = false;
        passedTargetWall = false;
    }
}
