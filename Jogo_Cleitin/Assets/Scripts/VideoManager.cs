using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string proximaCena;

    void Start()
    {
        videoPlayer.loopPointReached += VideoTerminou;
    }

    void VideoTerminou(VideoPlayer vp)
    {
        SceneManager.LoadScene("Principal");
    }
}