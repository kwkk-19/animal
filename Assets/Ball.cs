using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speedReductionFactor = 0.7f; // 減速係数（1未満の値）

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 初期力をZ軸方向に加える
        Vector3 initialForce = new Vector3(0, 2, -140); // Z軸方向の初期速度
        rb.AddForce(initialForce, ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        // 現在の速度を取得
        Vector3 currentVelocity = rb.linearVelocity;

        // Z軸の速度を減少させる
        currentVelocity.z *= speedReductionFactor;

        // X軸とY軸の速度はそのまま維持
        rb.linearVelocity = currentVelocity;
    }
}