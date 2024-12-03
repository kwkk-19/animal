using System.Collections;
using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public GameObject strikeZone;  // ストライクゾーンオブジェクト
    public Transform batTip;       // バットの先端
    public Transform batBase;      // バットの基底（グリップ部分）
    public float swingSpeed = 2f;  // スイング速度
    public Camera mainCamera;      // カメラ
    public float returnAngleThreshold = 300f;  // 初期位置に戻る前に回転する角度

    private bool isSwinging = false;  // スイング中かどうか
    private Quaternion initialRotation;  // バットの初期回転
    private Vector3 targetPoint;  // クリックされた打点位置
    private Quaternion targetRotation;  // バットのスイング方向

    void Start()
    {
        initialRotation = transform.rotation;  // 初期回転を記録
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)  // 左クリックでスイング開始
        {
            SetTargetAndSwing();
        }

        if (Input.GetKeyDown(KeyCode.Return))  // エンターキーでリセット
        {
            StopAllCoroutines();  // スイング動作を停止
            ResetBatPosition();
        }
    }

    void SetTargetAndSwing()
    {
        // カメラからクリック位置を取得
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("StrikeZone")))
        {
            targetPoint = hit.point;

            // バットの基底からクリック位置への方向を計算
            Vector3 directionToTarget = (targetPoint - batBase.position).normalized;

            // バットの基底から先端への現在の方向
            Vector3 batDirection = (batTip.position - batBase.position).normalized;

            // 回転軸を計算（バットの方向とターゲット方向の外積）
            Vector3 rotationAxis = Vector3.Cross(batDirection, directionToTarget);

            // 回転角度を計算（バット方向をターゲット方向に合わせる角度）
            float angle = Vector3.SignedAngle(batDirection, directionToTarget, rotationAxis);

            // バットを目標角度に傾ける
            targetRotation = Quaternion.AngleAxis(angle, rotationAxis) * transform.rotation;

            StartCoroutine(PrepareAndSwing());
        }
    }

    IEnumerator PrepareAndSwing()
    {
        isSwinging = true;

        // バットをクリック位置の方向に向ける
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, swingSpeed * Time.deltaTime * 360f);
            yield return null;
        }

        yield return SwingBat();
    }

    IEnumerator SwingBat()
    {
        float currentAngle = 0f;
        float step = swingSpeed * Time.deltaTime * 360f;  // 1フレームごとの回転角度

        while (currentAngle < returnAngleThreshold)
        {
            // バットの基底を中心にY軸で回転
            transform.RotateAround(batBase.position, Vector3.up, -step);
            currentAngle += step;

            yield return null;
        }

        StartCoroutine(ReturnToInitialPosition());
    }

    IEnumerator ReturnToInitialPosition()
    {
        // 初期位置にスムーズに戻す
        while (Quaternion.Angle(transform.rotation, initialRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, swingSpeed * Time.deltaTime * 360f);
            yield return null;
        }

        isSwinging = false;  // スイング終了
    }

    void ResetBatPosition()
    {
        // バットを初期位置に即座に戻す
        transform.rotation = initialRotation;
        isSwinging = false;
    }
}