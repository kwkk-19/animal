using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public Transform batBase;       // バットの基底（回転中心）
    public Transform batTip;        // バットの先端
    public float swingSpeed = 300f; // スイング速度
    public float swingAngle = 90f;  // スイングする最大回転角度
    public Camera mainCamera;       // メインカメラ
    public LayerMask strikeZoneLayer; // ストライクゾーンのレイヤー

    private bool isSwinging = false;    // スイング中かどうか
    private Quaternion initialRotation; // バットの初期回転
    private Quaternion targetRotation;  // バットの目標回転
    private float currentSwingAngle = 0f; // 現在のスイング角度

    void Start()
    {
        // 初期回転を記録
        initialRotation = batBase.rotation;

        // デバッグ用確認
        if (mainCamera == null)
        {
            Debug.LogError("メインカメラが設定されていません。");
        }
    }

    void Update()
    {
        // 左クリックでスイング開始
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            DetectClickAndSetTarget();
        }

        // エンターキーでリセット
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetBatPosition();
        }

        // スイング処理
        if (isSwinging)
        {
            PerformSwing();
        }
    }

    void DetectClickAndSetTarget()
    {
        // カメラからクリック位置のレイを飛ばす
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, strikeZoneLayer))
        {
            Debug.Log($"クリック位置: {hit.point}");

            // クリックした位置への方向を計算
            Vector3 directionToTarget = (hit.point - batBase.position).normalized;

            // バットの基底から目標方向を向く回転を計算
            targetRotation = Quaternion.LookRotation(directionToTarget);

            // スイングを開始
            isSwinging = true;
            currentSwingAngle = 0f;
        }
    }

    void PerformSwing()
    {
        float swingStep = swingSpeed * Time.deltaTime;

        // Y軸回転でスイング
        batBase.Rotate(Vector3.up, swingStep);
        currentSwingAngle += swingStep;

        // スイング完了チェック
        if (currentSwingAngle >= swingAngle)
        {
            isSwinging = false;
            Debug.Log("スイング完了");
        }
    }

    void ResetBatPosition()
    {
        // バットを初期位置に戻す
        batBase.rotation = initialRotation;
        isSwinging = false;
        currentSwingAngle = 0f;
        Debug.Log("バットの位置をリセットしました。");
    }
}
