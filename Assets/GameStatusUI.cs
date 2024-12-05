using UnityEngine;

public class GameStatusUI : MonoBehaviour
{
    public GameObject[] strikeIndicators; // ストライク用インジケーター
    public GameObject[] ballIndicators;   // ボール用インジケーター
    public GameObject[] outIndicators;    // アウト用インジケーター

    private int strikeCount = 0; // 現在のストライク数
    private int ballCount = 0;   // 現在のボール数
    private int outCount = 0;    // 現在のアウト数

    /// <summary>
    /// ストライクを追加
    /// </summary>
    public void AddStrike()
    {
        Debug.Log($"現在のストライクカウント: {strikeCount}");

        if (strikeCount < strikeIndicators.Length)
        {
            strikeIndicators[strikeCount].SetActive(true); // 該当インジケーターを表示
            Debug.Log($"Strike Indicator {strikeCount + 1} が表示されました！");
            strikeCount++;
        }

        if (strikeCount == strikeIndicators.Length) // ストライク2でアウト処理を実行
        {
            Debug.Log("ストライク2！アウトになります。");
            ResetStrikeIndicators();
            AddOut();
        }
    }

    /// <summary>
    /// ボールを追加
    /// </summary>
    public void AddBall()
    {
        Debug.Log($"現在のボールカウント: {ballCount}");

        if (ballCount < ballIndicators.Length)
        {
            ballIndicators[ballCount].SetActive(true); // 該当インジケーターを表示
            Debug.Log($"Ball Indicator {ballCount + 1} が表示されました！");
            ballCount++;
        }

        if (ballCount == ballIndicators.Length) // ボール2でリセット
        {
            Debug.Log("ボール2！ストライクとボールをリセットします。");
            ResetStrikeAndBall();
        }
    }

    /// <summary>
    /// アウトを追加
    /// </summary>
    public void AddOut()
    {
        Debug.Log($"現在のアウトカウント: {outCount}");

        if (outCount < outIndicators.Length)
        {
            outIndicators[outCount].SetActive(true); // 該当インジケーターを表示
            Debug.Log($"Out Indicator {outCount + 1} が表示されました！");
            outCount++;
        }

        if (outCount == outIndicators.Length) // アウト2で全てリセット
        {
            Debug.Log("アウト2！チェンジです！");
            ResetAll();
        }
    }

    /// <summary>
    /// ストライクとボールのインジケーターをリセット
    /// </summary>
    public void ResetStrikeAndBall()
    {
        ResetStrikeIndicators();

        foreach (GameObject indicator in ballIndicators)
        {
            indicator?.SetActive(false); // ボールインジケーターを非表示
        }

        ballCount = 0;
        Debug.Log("ストライクとボールのインジケーターをリセットしました。");
    }

    /// <summary>
    /// ストライクインジケーターをリセット
    /// </summary>
    public void ResetStrikeIndicators()
    {
        foreach (GameObject indicator in strikeIndicators)
        {
            indicator?.SetActive(false); // ストライクインジケーターを非表示
        }

        strikeCount = 0;
        Debug.Log("ストライクインジケーターをリセットしました。");
    }

    /// <summary>
    /// 全てのインジケーターをリセット
    /// </summary>
    public void ResetAll()
    {
        ResetStrikeAndBall();

        foreach (GameObject indicator in outIndicators)
        {
            indicator?.SetActive(false); // アウトインジケーターを非表示
        }

        outCount = 0;
        Debug.Log("全てのインジケーターをリセットしました。");
    }
}
