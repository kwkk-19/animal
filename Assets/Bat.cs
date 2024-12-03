using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public float swingSpeed = 200.0f;   // バットの振る速さ
    public float swingAngle = 90.0f;    // バットが振れる角度
    public float hitForce = 500.0f;     // ボールに与える力
    public Vector3 initialPosition;    // バットの初期位置
    public Quaternion initialRotation; // バットの初期回転

    private bool isSwinging = false;    // バットが振っているかどうかを判定
    private float currentAngle = 0.5f;  // 現在の角度
    private bool swingDirection = true; // 振る方向（true: 前方、false: 戻る）

    void Start()
    {
        // 初期位置と回転を記録
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // スペースキーを押したときにバットを振る
        if (Input.GetKeyDown(KeyCode.Space) && !isSwinging)
        {
            isSwinging = true;
            swingDirection = true; // 振り始める方向を設定
        }

        // バットが振られている場合
        if (isSwinging)
        {
            float angleChange = swingSpeed * Time.deltaTime;

            if (swingDirection)
            {
                currentAngle += angleChange;
                if (currentAngle >= swingAngle)
                {
                    swingDirection = false; // 振り戻す
                }
            }
            else
            {
                currentAngle -= angleChange;
                if (currentAngle <= 0)
                {
                    currentAngle = 0;
                    isSwinging = false; // 振り動作を終了

                    // バットの位置と回転を元に戻す
                    transform.position = initialPosition;
                    transform.rotation = initialRotation;
                }
            }

            // バットの角度を更新（Y軸回転）
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }
    }

    // バットとボールが衝突したときに呼ばれる
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトがボールかどうかを判定
        if (collision.gameObject.CompareTag("Ball"))
        {
            // ボールのRigidbodyを取得
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // バットの前方向にボールに力を加える
                Vector3 hitDirection = transform.right; // バットの右方向に飛ばす
                ballRigidbody.AddForce(hitDirection * hitForce);
            }
        }
    }
}
