using UnityEngine.UI;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameLogic gameLogic;

    public Button confirmButton;
    public Button cancelButton;

    private void Start()
    {
        confirmButton.onClick.AddListener(onConfirm);
        cancelButton.onClick.AddListener(onCancel);
    }

    private void onConfirm()
    {
        gameLogic.ResetLevel();
    }

    private void onCancel()
    {
        gameLogic.ShowScreen(this.gameObject, false);
    }
}
