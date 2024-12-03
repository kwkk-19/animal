using UnityEngine;

public class TargetWall : MonoBehaviour
{
    public static Vector3 targetPosition; // クリックした座標を保持

    void Start()
    {
        targetPosition = Vector3.zero; // 初期化
    }

    void OnMouseDown()
    {
        // マウスでクリックした位置を取得
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPosition = hit.point; // クリック位置を記録
            Debug.Log($"クリック位置: {targetPosition}");
        }
    }
}
