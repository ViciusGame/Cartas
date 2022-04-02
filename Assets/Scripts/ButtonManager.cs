using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerancia os botões utilizados, menu, para iniciar os modos de jogo, para os créditos, reseta os recordes.
/// </summary>
public class ButtonManager : MonoBehaviour
{
    /*Botão responsável por carregar a cena de menu.*/
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /*Botão responsável por carregar a cena de um dos modos de jogo */
    public void ToGame_X(int input)
    {
        ManageCards.Mode = input;
        SceneManager.LoadScene("Game_" + input);
    }

    /*Botão responsável por carregar a cena de créditos.*/
    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    /*Botão responsável por resetar os recordes e valor do último jogo.*/
    public void Reset(int input)
    {
        PlayerPrefs.SetInt("Recorde" + input, 999);
        PlayerPrefs.SetInt("Jogadas" + input, 0);
    }

    /*Botão responsável por voltar a cena de game anteriormente jogado */
    public void Retry()
    {
        SceneManager.LoadScene("Game_" + ManageCards.Mode);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
