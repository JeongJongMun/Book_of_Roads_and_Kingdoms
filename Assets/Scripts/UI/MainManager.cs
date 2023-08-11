using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("Playing");
    }
}
