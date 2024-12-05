using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public Transform batBase; // バットの基底（回転の中心）
    public Transform batTip; // バットの先端
    public float swingSpeed = 300f; // スイング速度
    public float maxSwingAngle = 90f; // スイングする最大角度
    public BaseballGameController gameController; // BaseballGameController の参照
    public Camera mainCamera; // メインカメラ

    private bool isSwinging = false; // スイング中フラグ
    private float currentSwingAngle = 0f;
    private Quaternion initialRotation; // 初期回転
    private Quaternion targetRotation; // ターゲット回転

    void Start()
    {
        // 初期回転を記録
        if (batBase == null)
        {
            Debug.LogError("BatBase が設定されていません！");
            return;
        }
        initialRotation = batBase.rotation;

        // MainCamera が設定されていない場合、自動取得
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (gameController == null)
        {
            Debug.LogError("BaseballGameController が設定されていません！");
        }
    }

    void Update()
    {
        // 左クリックでスイング開始
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            SetBatTargetAndSwing();
        }

        // エンターキーでリセット
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetBatPosition();
        }

        // スイング動作中
        if (isSwinging)
        {
            PerformSwing();
        }
    }

    /// <summary>
    /// バットのターゲットを設定してスイング開始
    /// </summary>
    void SetBatTargetAndSwing()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"クリックされた位置: {hit.point}");

            // バットの先端がクリック位置を向くように回転を設定
            Vector3 direction = (hit.point - batTip.position).normalized;
            targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // スイング開始
            isSwinging = true;
            currentSwingAngle = 0f;

            // BaseballGameController の OnBatSwing を呼び出し
            gameController?.OnBatSwing();
        }
        else
        {
            Debug.LogWarning("クリック位置がストライクゾーン外です！");
        }
    }

    /// <summary>
    /// スイング処理
    /// </summary>
    void PerformSwing()
    {
        // スイング動作を実行
        float swingStep = swingSpeed * Time.deltaTime;
        currentSwingAngle += swingStep;

        if (currentSwingAngle <= maxSwingAngle)
        {
            // バットをターゲット回転に向けて徐々に回転
            batBase.rotation = Quaternion.Lerp(batBase.rotation, targetRotation, swingStep / maxSwingAngle);
        }
        else
        {
            // スイング完了
            isSwinging = false;
            Debug.Log("スイング完了");
        }
    }

    /// <summary>
    /// バットの位置をリセット
    /// </summary>
    void ResetBatPosition()
    {
        if (batBase == null) return;

        // 初期回転に戻す
        batBase.rotation = initialRotation;
        isSwinging = false;
        currentSwingAngle = 0f;

        Debug.Log("バットの位置をリセットしました！");
    }
}
