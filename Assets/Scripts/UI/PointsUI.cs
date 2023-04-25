using UnityEngine;
using TMPro;


public class PointsUI : MonoBehaviour
{
    public TextMeshProUGUI pointCountText;

    void Update()
    {
        if (PlayerManager.instance == null) return;
        pointCountText.text = PlayerManager.instance.GetPlayerStats().GetPoints().ToString();

    }
}
