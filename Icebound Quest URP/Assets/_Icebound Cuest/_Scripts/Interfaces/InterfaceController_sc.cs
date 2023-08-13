using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceController_sc : MonoBehaviour
{
    MouseLock_sc mouseLock;
    bool _isPaused;
    [SerializeField] bool _isMenu;
    [SerializeField] GameObject _pauseInterface;
    [SerializeField] GameObject _settingsInterface;
    [SerializeField] GameObject _controlsInterface;
    [SerializeField] GameObject _keyboardInterface;
    [SerializeField] GameObject _mouseInterface;

    PlayerMovement_sc playerMovement;

    void Start()
    {
        mouseLock=FindObjectOfType<MouseLock_sc>();    
        playerMovement=FindObjectOfType<PlayerMovement_sc>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)&&!_isMenu)
        {
            if (_isPaused) Resume(); else Pause();
            
        }
    }
    public void ChangeScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        _isPaused = true;
        playerMovement.JumpForce = 0;

        Time.timeScale = 0f;
        MouseLock();
        _pauseInterface.SetActive(true);
    }
    public void Resume()
    {
        _isPaused = false;
        playerMovement.JumpForce = 3;

        Time.timeScale = 1f;
        MouseUnLock();
        _pauseInterface.SetActive(false);
        _settingsInterface.SetActive(false);
        _controlsInterface.SetActive(false);
        _keyboardInterface.SetActive(false);
        _mouseInterface.SetActive(false);

    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Settings()
    {
        _settingsInterface.SetActive(true);
    }
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
    public void MouseUnLock()
    {
        if (mouseLock != null) mouseLock.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void MouseLock()
    {
        if (mouseLock != null) mouseLock.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
