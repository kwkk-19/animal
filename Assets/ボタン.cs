using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // シーンを切り替える関数
    public void SwitchScene(string SampleScene)
    {
        SceneManager.LoadScene(SampleScene);
    }
}