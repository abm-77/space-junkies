using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WebGLVideoHandler : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, $"{SceneManager.GetActiveScene().name}.mp4");
    }
}