using UnityEngine;

public class BaseballGameController : MonoBehaviour
{
    public AudioClip swingSound; // スイング時の効果音
    public AudioSource audioSource; // 効果音再生用AudioSource
    public GameObject ball; // ボールの参照
    public float ballHitForce = 500f; // ボールに与える力

    private bool ballHit = false;

    public void OnBatSwing()
    {
        Debug.Log("OnBatSwingが呼び出されました！");

        // 効果音を再生
        if (swingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(swingSound);
        }

        // ボールをヒットさせる
        if (ball != null && !ballHit)
        {
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(Vector3.forward * ballHitForce);
                ballHit = true; // ヒット状態を更新
                Debug.Log("ボールを打ちました！");
            }
        }
    }

    public void ResetGame()
    {
        // ボールの位置と状態をリセット
        if (ball != null)
        {
            ball.transform.position = new Vector3(0, 1, 0); // 初期位置
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.linearVelocity = Vector3.zero;
                ballRigidbody.angularVelocity = Vector3.zero;
            }
        }
        ballHit = false;
        Debug.Log("ゲームをリセットしました。");
    }
}
