using UnityEngine;

public class BatSwing : MonoBehaviour
{
    [Header("Swing Settings")]
    public float swingSpeed = 300f;  // 度/秒
    public float batLength = 1.0f;   // batBaseからbatTipまでの距離
    [Tooltip("スイング角度に対する係数。1より大きくするとスイング角度が拡大する")]
    public float angleMultiplier = 1.5f; // 少し多めに回転させるための係数例

    [Header("References")]
    public Transform batBase;        // 持ち手位置（Pivot）
    public Transform batTip;         // バット先端位置
    public Camera mainCamera;
    public Collider strikeZoneCollider;
    public BaseballGameController gameController;

    private bool isSwinging = false;
    private float currentAngle = 0f;
    private float maxSwingAngle;
    private Quaternion initialRotation;
    private Quaternion finalRotation;
    private Quaternion startRotation;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (strikeZoneCollider == null)
            Debug.LogError("StrikeZoneCollider が設定されていません！");

        if (batBase == null || batTip == null)
            Debug.LogError("batBaseとbatTipが設定されていません！");

        // バットの初期回転を記憶
        startRotation = batBase.rotation;
    }

    void Update()
    {
        // 左クリックでスイング開始
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            TrySetTargetAndSwing();
        }

        // スイング中
        if (isSwinging)
        {
            PerformSwing();
        }

        // Enterでリセット
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetBatPosition();
        }
    }

    void TrySetTargetAndSwing()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (strikeZoneCollider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log($"ストライクゾーン内クリック: {hit.point}");

            Vector3 diff = hit.point - batBase.position;
            float dist = diff.magnitude;
            Vector3 direction = diff.normalized;

            if (Mathf.Abs(dist - batLength) > 0.01f)
            {
                Debug.LogWarning($"クリック位置とbatBaseの距離({dist:F2})がbatLength({batLength:F2})と違います。先端通過の誤差が出る可能性があります。");
            }

            // up方向をdirectionへ向ける回転
            Quaternion lookRotation = Quaternion.FromToRotation(Vector3.up, direction);

            // 元のスイング角度を計算
            float angle = Vector3.Angle(batBase.up, direction);
            // ここで係数をかけてスイング角度を増やす
            maxSwingAngle = angle * angleMultiplier;

            float halfAngle = maxSwingAngle / 2f;
            initialRotation = lookRotation * Quaternion.AngleAxis(-halfAngle, Vector3.right);
            finalRotation = lookRotation * Quaternion.AngleAxis(halfAngle, Vector3.right);

            isSwinging = true;
            currentAngle = 0f;
            gameController?.OnBatSwing();
        }
        else
        {
            Debug.LogWarning("ストライクゾーン外をクリックしました。");
        }
    }

    void PerformSwing()
    {
        float step = swingSpeed * Time.deltaTime;
        currentAngle += step;

        float t = Mathf.Clamp01(currentAngle / maxSwingAngle);
        batBase.rotation = Quaternion.Slerp(initialRotation, finalRotation, t);

        if (t >= 1f)
        {
            Debug.Log("スイング完了、元の位置へ戻します");
            ResetBatPosition();
        }
    }

    void ResetBatPosition()
    {
        batBase.rotation = startRotation;
        isSwinging = false;
        currentAngle = 0f;
        Debug.Log("バット位置リセット");
    }
}
