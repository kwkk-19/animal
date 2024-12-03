using UnityEngine;

public class Baseball : MonoBehaviour
{
    public float initialSpeed = 50f; // ボールの初速
    public float airResistanceFactor = 0.01f; // 空気抵抗係数
    public float verticalAngle = 10f; // ボールの投球角度（度単位）

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ボールの初速度を計算（方向を逆に設定）
        float radians = Mathf.Deg2Rad * verticalAngle; // 角度をラジアンに変換
        Vector3 initialVelocity = new Vector3(0, Mathf.Sin(radians), -Mathf.Cos(radians)) * initialSpeed; // Z軸方向を逆転

        // 初速度を設定
        rb.linearVelocity = initialVelocity;

        // 空気抵抗などのシミュレーションのため、重力を有効化
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        // 空気抵抗を計算
        Vector3 velocity = rb.linearVelocity;
        Vector3 airResistance = -velocity.normalized * airResistanceFactor * velocity.sqrMagnitude;

        // 空気抵抗を適用
        rb.AddForce(airResistance, ForceMode.Acceleration);
    }
}
