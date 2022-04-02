using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Respons�vel pelo gerenciamento dos tiles(cartas), revelando a carta ao detectar um click sobre a mesma e escondendo as cartas no inicio de jogo e quando o jogador erra o par.
/// </summary>
public class Tile : MonoBehaviour
{
    // private bool tileRevelada = false;              // indicador de carta virada ou n�o
    public Sprite originalCarta;                    // Sprite da carta desejada
    public Sprite backCarta1;                        // Sprite do verso da carta
    public Sprite backCarta2;
    public int linha;
    public int coluna;

    /*Respons�vel por detectar o click sobre um tile (carta).*/
    public void OnMouseDown()
    {
        print("Voc� pressionou num Tile");
        /*if (tileRevelada)
            EscondeCarta();
        else
            RevelaCarta();*/        // N�o guarda n�mero de cartas

        GameObject.Find("gameManager").GetComponent<ManageCards>().CartaSelecionada(gameObject);
    }

    /*Respons�vel por esconder as cartas ao iniciar o jogo e ap�s erros do jogador.*/
    public void EscondeCarta(int i, int j)
    {
        // para cada modo de jogo os versos das cartas respeitam os cr�t�rios necess�rios
        switch (ManageCards.Mode)
        {
            case 1:
                // no jogo 1 o verso da carta � indiferente
                GetComponent<SpriteRenderer>().sprite = backCarta1;
                // tileRevelada = false;
                break;

            case 2:
                // no jogo 2 a 1� linha necessariamente � de uma cor e a 2� linha de outra
                if(i == 0)
                    GetComponent<SpriteRenderer>().sprite = backCarta1;
                else
                    GetComponent<SpriteRenderer>().sprite = backCarta2;

                break;

            default:
                // no jogo 3 a 1� coluna � de uma cor e a 2� coluna de outra
                if (j == 0)
                    GetComponent<SpriteRenderer>().sprite = backCarta1;
                else
                    GetComponent<SpriteRenderer>().sprite = backCarta2;

                break;
        }
    }

    /*Respons�vel por revelar a carta ap�s sele��o da mesma*/
    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        // tileRevelada = true;
    }

    /*respons�vel por fazer a mudan�a de cartas do jogo.*/
    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }
}
