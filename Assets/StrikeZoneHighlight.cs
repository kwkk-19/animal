using UnityEngine;

public class StrikeZoneHighlight : MonoBehaviour
{
    public BoxCollider strikeZoneCollider; // ストライクゾーンのコライダー
    public SpriteRenderer ballOutline;    // 表示するボールの縁
    public float outlineSize = 1.0f;      // 縁のサイズ

    void Start()
    {
        if (strikeZoneCollider == null)
        {
            Debug.LogError("StrikeZoneCollider が設定されていません！");
        }

        if (ballOutline != null)
        {
            ballOutline.enabled = false; // 初期状態では非表示
        }
        else
        {
            Debug.LogError("BallOutline が設定されていません！");
        }
    }

    void Update()
    {
        DetectMousePosition();
    }

    void DetectMousePosition()
    {
        if (strikeZoneCollider == null || ballOutline == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (strikeZoneCollider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log($"マウス座標: {hit.point}"); // デバッグログで座標を確認

            // ボールの縁を表示し、位置を更新
            ballOutline.enabled = true;
            ballOutline.transform.position = hit.point;
            ballOutline.transform.localScale = Vector3.one * outlineSize;
        }
        else
        {
            ballOutline.enabled = false; // ストライクゾーン外では非表示
        }
    }
}
