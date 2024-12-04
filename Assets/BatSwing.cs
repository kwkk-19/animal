using UnityEngine;

public class BatSwing : MonoBehaviour
{
    public BaseballGameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // ボールに接触した場合、ゲームコントローラーに通知
            gameController.OnBatSwing(true);
        }
    }

    public void OnSwingCompleted()
    {
        // スイング完了時にボールに当たらなかった場合を通知
        gameController.OnBatSwing(false);
    }
}
