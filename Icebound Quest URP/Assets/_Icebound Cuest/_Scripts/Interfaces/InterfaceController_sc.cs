using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class InterfaceController_sc : MonoBehaviour
{
    MouseLock_sc mouseLock;
    bool _isPaused;
    bool turorial;
    int _language;
    [SerializeField] bool _isMenu;
    [SerializeField] GameObject _pauseInterface;
    [SerializeField] GameObject _settingsInterface;
    [SerializeField] GameObject _controlsInterface;
    [SerializeField] GameObject _audioSettingsInterface;
    [SerializeField] GameObject _keyboardInterface;
    [SerializeField] GameObject _mouseInterface;

    PlayerMovement_sc playerMovement;
    PlayerMovementSea_sc playerMovementSea;
    SceneLoadManager_sc sceneLoadManager;
    AudioController_sc audioController;
    public bool Turorial { get => turorial; set => turorial = value; }

    void Start()
    {
        mouseLock = FindObjectOfType<MouseLock_sc>();
        audioController = FindObjectOfType<AudioController_sc>();
        playerMovement = FindObjectOfType<PlayerMovement_sc>();
        playerMovementSea = FindObjectOfType<PlayerMovementSea_sc>();
        sceneLoadManager = FindObjectOfType<SceneLoadManager_sc>();
        Time.timeScale = 1;
        LoadData();
        Invoke(nameof(LocalSave), 0.1f);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_isMenu && !Turorial)
        {
            if (_isPaused) 
            { 
                Resume(); 
                CloseInterfaces(); 
            } 
            else 
            { 
                Pause(); 
                PauseInterface(); 
            }

        }
    }
    public void ChangeScene(int Scene)
    {
        Time.timeScale = 1;
        sceneLoadManager.SceneLoad(Scene);
    }

    public void Pause()
    {
        audioController.PlaySelectedSounds();

        _isPaused = true;
        if(playerMovement!=null)playerMovement.JumpForce = 0;
        if(playerMovementSea!=null)playerMovementSea.BoostForce = 0;

        Time.timeScale = 0f;
        MouseUnLock();
    }
    public void PauseInterface()
    {
        _pauseInterface.SetActive(true);
    }
    public void Resume()
    {
        audioController.ResumePausedSounds();

        _isPaused = false;
        if (playerMovement != null) playerMovement.JumpForce = 3;
        if (playerMovementSea != null) playerMovementSea.BoostForce = 7;

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
        Time.timeScale = 1f;
        sceneLoadManager.SceneLoad(SceneManager.GetActiveScene().buildIndex);
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
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
    public void ChangeLanguage(int languageNum)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageNum];
        _language=languageNum;
        SaveData();
    }
    private void LocalSave()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_language];
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("language",_language);
    }
    private void LoadData()
    {
        _language=PlayerPrefs.GetInt("language");
    }
}
