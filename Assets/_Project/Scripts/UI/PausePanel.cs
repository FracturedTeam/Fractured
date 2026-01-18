using System.Collections.Generic;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private InputAction menuAction;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private List<GameObject> panels;
    private bool gameIsPaused;

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
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0 : 1;
        menu.SetActive(gameIsPaused);
        ReSet();
    }

    private void ReSet()
    {
        mainMenuPanel.SetActive(gameIsPaused);
        foreach (var panel in panels)
            panel.SetActive(false);
    }

    public void LoadMenu() {
        Time.timeScale = 1;
        GameSceneLoaderSystem.Instance.LoadMenu();
    }
        
}
