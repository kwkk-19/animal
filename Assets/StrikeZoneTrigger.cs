using UnityEngine;

public class StrikeZoneTrigger : MonoBehaviour
{
    private bool ballPassedThrough = false; // ボールがストライクゾーンを通過したかどうか

    // ボールがストライクゾーンに入った場合
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballPassedThrough = true; // ストライクゾーンにボールが入った
            Debug.Log("ボールがストライクゾーンに入りました。");
        }
    }

    // ボールがストライクゾーンから出た場合
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (ballPassedThrough)
            {
                Debug.Log("ストライク！");
            }
            else
            {
                Debug.Log("ボール！");
            }

            ballPassedThrough = false; // 状態をリセット
        }
    }
}
