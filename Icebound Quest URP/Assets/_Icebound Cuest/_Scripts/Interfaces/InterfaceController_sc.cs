using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceController_sc : MonoBehaviour
{
    MouseLock_sc mouseLock;
    bool _isPaused;
    bool turorial;
    [SerializeField] bool _isMenu;
    [SerializeField] GameObject _pauseInterface;
    [SerializeField] GameObject _settingsInterface;
    [SerializeField] GameObject _controlsInterface;
    [SerializeField] GameObject _audioSettingsInterface;
    [SerializeField] GameObject _keyboardInterface;
    [SerializeField] GameObject _mouseInterface;

    PlayerMovement_sc playerMovement;
    SceneLoadManager_sc sceneLoadManager;

    public bool Turorial { get => turorial; set => turorial = value; }

    void Start()
    {
        mouseLock = FindObjectOfType<MouseLock_sc>();
        playerMovement = FindObjectOfType<PlayerMovement_sc>();
        sceneLoadManager = FindObjectOfType<SceneLoadManager_sc>();
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_isMenu && !Turorial)
        {
            if (_isPaused) { Resume(); CloseInterfaces(); } else { Pause(); PauseInterface(); }

        }
    }
    public void ChangeScene(int Scene)
    {
        sceneLoadManager.SceneLoad(Scene);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        _isPaused = true;
        playerMovement.JumpForce = 0;

        Time.timeScale = 0f;
        MouseUnLock();
    }
    public void PauseInterface()
    {
        _pauseInterface.SetActive(true);
    }
    public void Resume()
    {
        _isPaused = false;
        playerMovement.JumpForce = 3;

        Time.timeScale = 1f;
        MouseLock();
    }
    public void CloseInterfaces()
    {
        _pauseInterface.SetActive(false);
        _settingsInterface.SetActive(false);
        _controlsInterface.SetActive(false);
        //_keyboardInterface.SetActive(false);
        _mouseInterface.SetActive(false);
        _audioSettingsInterface.SetActive(false);
    }
    public void Restart()
    {
        sceneLoadManager.SceneLoad(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void Settings()
    {
        _settingsInterface.SetActive(true);
    }
    #region controls
    public void Controls()
    {
        _controlsInterface.SetActive(true);
    }
    public void Keyboard()
    {
        _keyboardInterface.SetActive(true);
    }
    public void Mouse()
    {
        _mouseInterface.SetActive(true);
    }
    #endregion
    public void AudioSettings()
    {
        _audioSettingsInterface.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ClickSound(AudioSource audioSource)
    {
        audioSource.Play();
    }
    public void SelectSound(AudioSource audioSource)
    {
        audioSource.Play();
    }
    public void MouseLock()
    {
        if (mouseLock != null) mouseLock.Mouse = false;
    }
    public void MouseUnLock()
    {
        if (mouseLock != null) mouseLock.Mouse = true;
    }
}
