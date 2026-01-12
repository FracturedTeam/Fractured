using System.Collections.Generic;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private InputAction menuAction;
    [SerializeField] private GameObject menu;
    [SerializeField] private List<GameObject> panels;
    private bool _gameIsPaused;

    private void Awake()
    {
        menuAction.performed += OnEscapePressed;
        ReSet();
    }

    private void OnEnable()
    {
        menuAction.Enable();
    }

    private void OnDisable()
    {
        menuAction.Disable();
    }

    private void OnEscapePressed(InputAction.CallbackContext context)
    {
        ChangeState();
    }

    public void ChangeState()
    {
        _gameIsPaused = !_gameIsPaused;
        Time.timeScale = _gameIsPaused ? 0 : 1;
        menu.SetActive(_gameIsPaused);
        ReSet();
    }

    private void ReSet()
    {
        foreach (var panel in panels)
            panel.SetActive(false);
    }

    public void LoadMenu() {
        Time.timeScale = 1;
        GameSceneLoaderSystem.Instance.LoadMenu();
    }
        
}
