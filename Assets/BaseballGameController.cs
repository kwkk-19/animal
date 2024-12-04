using UnityEngine;

public class BaseballGameController : MonoBehaviour
{
    private bool isBallInStrikeZone = false;
    private bool ballPassedThrough = false;
    private bool batSwingOccurred = false;

    public void OnBallEnterStrikeZone()
    {
        isBallInStrikeZone = true;
        Debug.Log("ボールがストライクゾーンに入りました。");
    }

    public void OnBallExitStrikeZone(bool passedThrough)
    {
        isBallInStrikeZone = false;
        ballPassedThrough = passedThrough;

        if (!batSwingOccurred)
        {
            if (ballPassedThrough)
            {
                Debug.Log("ストライク！");
            }
            else
            {
                Debug.Log("ボール！");
            }
        }
    }

    public void OnBatSwing(bool ballHit)
    {
        batSwingOccurred = true;

        if (!ballHit)
        {
            Debug.Log("空振り: ストライク！");
        }
        else
        {
            Debug.Log("バットがボールに当たりました！");
        }

        ResetState();
    }

    private void ResetState()
    {
        isBallInStrikeZone = false;
        ballPassedThrough = false;
        batSwingOccurred = false;
    }
}
