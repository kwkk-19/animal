using UnityEngine;

public class TransparentZone : MonoBehaviour
{
    public Color transparentColor = new Color(1f, 0f, 0f, 0.5f); // 半透明の赤色

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;

            // 色と透明度を設定
            material.color = transparentColor;
        }
        else
        {
            Debug.LogError("Rendererが見つかりません。正しいオブジェクトにスクリプトをアタッチしてください。");
        }
    }
}
