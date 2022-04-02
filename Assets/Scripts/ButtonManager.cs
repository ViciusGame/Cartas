using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerancia os bot�es utilizados, menu, para iniciar os modos de jogo, para os cr�ditos, reseta os recordes.
/// </summary>
public class ButtonManager : MonoBehaviour
{
    /*Bot�o respons�vel por carregar a cena de menu.*/
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /*Bot�o respons�vel por carregar a cena de um dos modos de jogo */
    public void ToGame_X(int input)
    {
        ManageCards.Mode = input;
        SceneManager.LoadScene("Game_" + input);
    }

    /*Bot�o respons�vel por carregar a cena de cr�ditos.*/
    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    /*Bot�o respons�vel por resetar os recordes e valor do �ltimo jogo.*/
    public void Reset(int input)
    {
        PlayerPrefs.SetInt("Recorde" + input, 999);
        PlayerPrefs.SetInt("Jogadas" + input, 0);
    }

    /*Bot�o respons�vel por voltar a cena de game anteriormente jogado */
    public void Retry()
    {
        SceneManager.LoadScene("Game_" + ManageCards.Mode);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
