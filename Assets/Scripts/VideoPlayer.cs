using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        //videoPlayer.isLooping = true;  // Å©ÉãÅ[ÉvON
        videoPlayer.Play();            // Å©çƒê∂
    }
}