using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public GameLogic gameLogic;

    private VisualElement root;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        Button buttonStartGame = root.Q<Button>("ButtonStartGame");
        Button buttonConfig = root.Q<Button>("ButtonConfig");
        Button buttonExit = root.Q<Button>("ButtonExit");

        buttonStartGame.clicked += () => StartGame();
        buttonExit.clicked += () => ExitGame();
    }

    private void StartGame()
    {
        Debug.Log("UIController: ButtonStartGame clicked");
        gameLogic.Init();
        Invoke("HideUI", 0.0f);
    }

    private void ExitGame()
    {
        Debug.Log("UIController: ButtonExit clicked");
        gameLogic.ExitGame();
    }

    private void HideUI()
    {
        root.Q<VisualElement>("MainMenuContainer").style.display = DisplayStyle.None;
    }
}
