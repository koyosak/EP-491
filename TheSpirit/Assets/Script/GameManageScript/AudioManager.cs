using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    [SerializeField] private AudioClip titleMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip gameClearMusic;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusicForCurrentScene();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }

    private void PlayMusicForCurrentScene()
{
    if (_audioSource == null) return;

    AudioClip clipToPlay = null;

    switch (SceneManager.GetActiveScene().name)
    {
        case "TitleScene":
            clipToPlay = titleMusic;
            break;
        case "GameScene":
            clipToPlay = gameMusic;
            break;
        case "GameClearScene":
            clipToPlay = gameClearMusic;
            break;
    }

    if (clipToPlay != null && _audioSource.clip != clipToPlay)
    {
        StartCoroutine(FadeMusic(clipToPlay));
    }
}

private IEnumerator FadeMusic(AudioClip newClip)
{
    if (_audioSource.isPlaying)
    {
        // Fade out
        while (_audioSource.volume > 0.1f)
        {
            _audioSource.volume -= Time.deltaTime / 1f; // Adjust fade speed
            yield return null;
        }
        _audioSource.Stop();
        _audioSource.volume = 1f; // Reset volume
    }

    _audioSource.clip = newClip;
    _audioSource.loop = newClip != gameClearMusic;
    _audioSource.Play();
}

}

