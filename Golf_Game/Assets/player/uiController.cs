using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class uiController : MonoBehaviour
{
    private playerMain myPlayer;
    [SerializeField]
    TextMeshProUGUI strokeText;
    [SerializeField]
    TextMeshProUGUI levelName;
    [SerializeField]
    TextMeshProUGUI parText;
    [SerializeField]
    GameObject loseCase;
    [SerializeField]
    GameObject winCase;

    public TextMeshProUGUI StrokeText { get => strokeText; set => strokeText = value; }
    public TextMeshProUGUI LevelName { get => levelName; set => levelName = value; }
    public TextMeshProUGUI ParText { get => parText; set => parText = value; }

    private void Awake() {
        myPlayer = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<playerMain>();
    }

    private void LateUpdate() {
        strokeText.text = "Strokes: " + myPlayer.Strokes; //will fix later
        ParText.text = "Par: " + myPlayer.strokeMaxForLevel;
    }

    public void showFailCase() {
        loseCase.SetActive(true);
    }

    public void showWinCase() {
        winCase.SetActive(true);
    }

    public void reloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void nextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame() {
        Application.Quit();
    }
}
