using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public Transform batBase;   // バットの基底（回転の中心）
    public float swingSpeed = 300f; // スイング速度
    public float swingAngle = 360f; // スイングする角度
    public Camera mainCamera;  // メインカメラ
    public LayerMask strikeZoneLayer; // ストライクゾーンのレイヤー

    private Quaternion initialRotation; // バットの初期回転
    private Quaternion targetRotation;  // バットの目標回転
    private bool isSwinging = false;    // スイング中かどうか
    private float currentSwingAngle = 0f; // 現在のスイング角度

    void Start()
    {
        // 初期回転を記録
        initialRotation = batBase.rotation;
    }

    void Update()
    {
        // ストライクゾーンのクリックを検出
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            DetectStrikeZoneClick();
        }

        // スイング中であれば処理を進める
        if (isSwinging)
        {
            PerformSwing();
        }

        // エンターキーでバットをリセット
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetBatPosition();
        }
    }

    private void DetectStrikeZoneClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, strikeZoneLayer))
        {
            // クリックした座標を計算し、バットの目標回転を設定
            Vector3 directionToTarget = (hit.point - batBase.position).normalized;
            targetRotation = Quaternion.LookRotation(directionToTarget);

            // スイングを開始
            isSwinging = true;
            currentSwingAngle = 0f;
        }
    }

    private void PerformSwing()
    {
        // バットを目標方向に向けて傾ける
        batBase.rotation = Quaternion.RotateTowards(batBase.rotation, targetRotation, swingSpeed * Time.deltaTime);

        // 現在の回転角度を増加
        currentSwingAngle += swingSpeed * Time.deltaTime;

        // 1回転（360度）を完了したらスイング終了
        if (currentSwingAngle >= swingAngle)
        {
            isSwinging = false;
        }
    }

    private void ResetBatPosition()
    {
        // 初期位置に戻す
        batBase.rotation = initialRotation;
        isSwinging = false;
        currentSwingAngle = 0f;
    }
}
