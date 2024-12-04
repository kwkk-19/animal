using UnityEngine;

public class GameStatusUI : MonoBehaviour
{
    public GameObject strikeIndicator1, strikeIndicator2; // ストライク表示
    public GameObject ballIndicator1, ballIndicator2;     // ボール表示
    public GameObject outIndicator1, outIndicator2;       // アウト表示

    private int strikeCount = 0;
    private int ballCount = 0;
    private int outCount = 0;

    public void AddStrike()
    {
        if (strikeCount == 0)
        {
            strikeIndicator1.SetActive(true);
        }
        else if (strikeCount == 1)
        {
            strikeIndicator2.SetActive(true);
        }
        else if (strikeCount == 2)
        {
            ResetStrikeAndBall();
            AddOut();
        }
        strikeCount++;
    }

    public void AddBall()
    {
        if (ballCount == 0)
        {
            ballIndicator1.SetActive(true);
        }
        else if (ballCount == 1)
        {
            ballIndicator2.SetActive(true);
        }
        else if (ballCount == 2)
        {
            ResetStrikeAndBall();
            AddStrike(); // ボール3回でストライクに変換
        }
        ballCount++;
    }

    public void AddOut()
    {
        if (outCount == 0)
        {
            outIndicator1.SetActive(true);
        }
        else if (outCount == 1)
        {
            outIndicator2.SetActive(true);
        }
        else
        {
            Debug.Log("チェンジ！");
            ResetAll();
        }
        outCount++;
    }

    public void ResetStrikeAndBall()
    {
        strikeCount = 0;
        ballCount = 0;
        strikeIndicator1.SetActive(false);
        strikeIndicator2.SetActive(false);
        ballIndicator1.SetActive(false);
        ballIndicator2.SetActive(false);
    }

    public void ResetAll()
    {
        ResetStrikeAndBall();
        outCount = 0;
        outIndicator1.SetActive(false);
        outIndicator2.SetActive(false);
    }
}
