using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ScreenLoader : SingletonPersitent<ScreenLoader>
{
    [SerializeField]
    private VideoClip _videoClip;

    [SerializeField]
    private RenderTexture _videoOutput;
    private VideoPlayer _videoPlayer;
    private GameObject videoHolder;

    private Dictionary<string, Action<WWWForm>> loader;

    private void Start()
    {
        videoHolder = transform.GetChild(0).gameObject;
        GameObject camera = Camera.main.gameObject;
        // Debug.Log(camera.gameObject.name);

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        _videoPlayer = camera.AddComponent<VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        _videoPlayer.playOnAwake = false;

        // By default, Video Players added to a camera will use the far plane.
        // Let's target the near plane instead.
        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;

        _videoPlayer.targetTexture = _videoOutput;
        videoHolder.SetActive(false);

        // This will cause our Scene to be visible through the video being played.
        _videoPlayer.targetCameraAlpha = 1F;
        _videoPlayer.clip = _videoClip;
        // Skip the first 100 frames.
        // _videoPlayer.frame = 100;

        // Restart from beginning when done.
        _videoPlayer.isLooping = false;

        // // Each time we reach the end, we slow down the playback by a factor of 10.
        // videoPlayer.loopPointReached += EndReached;

        // Start playback. This means the Video Player may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
    }

    public void Load(string screenName)
    {
        videoHolder.SetActive(true);
        string activeSceneName = SceneManager.GetActiveScene().name;
        if (activeSceneName == screenName)
            return;
        SceneManager.UnloadSceneAsync(activeSceneName);
        _videoPlayer.Play();
        StartCoroutine(SimulateLoadData(screenName));
    }

    private IEnumerator SimulateLoadData(string screenName)
    {
        yield return new WaitForSeconds(2.5f);
        // _videoPlayer.Stop();
        videoHolder.SetActive(false);

        SceneManager.LoadScene(screenName);
    }
}
