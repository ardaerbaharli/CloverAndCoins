using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesPanel : MonoBehaviour
{
    public void PlayButton(string gameName)
    {
        SoundManager.instance.PauseMenuSong();
        SceneManager.LoadScene(gameName);
    }
}