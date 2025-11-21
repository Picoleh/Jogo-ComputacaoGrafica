using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameOverType {
    Won,
    LostByBattery
}

public class GameOverMenu : MenuBase
{
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button mainMenuButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }


    public override void OpenMenu(GameOverType type, float battery, float maxBattery) {
        if (type == GameOverType.Won)
            statusText.text = "VOCÊ GANHOU!";
        else
            statusText.text = "VOCÊ PERDEU!";

        switch (type) {
            case GameOverType.Won:
                descriptionText.text = "Através de muita luta e trabalho rápido você conseguiu ajudar a todos os sobreviventes! BOA!";
                break;
            case GameOverType.LostByBattery:
                descriptionText.text = "Você estava dando o seu melhor, porém não conseguiu ser rápido o suficiente!";
                break;
        }

        scoreText.text = "Sua pontuação final é: " + ((int)battery).ToString() + "/" + ((int)maxBattery).ToString();

        base.OpenMenu();
    }

    private void OnMainMenuClick() {
        SceneManager.LoadScene("MainMenu");
        MenuManager.instance.OpenMenu(MenuType.Main);
    }
}
