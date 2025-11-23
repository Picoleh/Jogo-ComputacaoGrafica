using System.Collections.Generic;
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

    [Header("Mesma ordem enums")]
    [SerializeField] List<Sprite> endScreens;

    private GameOverType gameOver;

    private void Awake() {
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }


    public override void OpenMenu(GameOverType type, float battery, float maxBattery) {
        gameOver = type;
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

        int score = (int)((battery * 100f) / maxBattery);
        scoreText.text = "Sua pontuação final é: " + score.ToString() + "/100";

        base.OpenMenu();
    }

    public override Sprite GetImage() {
        return endScreens[(int)gameOver];
    }

    private void OnMainMenuClick() {
        SceneManager.LoadScene("MainMenu");
        MenuManager.instance.OpenMenu(MenuType.Main);
    }
}
