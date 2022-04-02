using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsável pelo gerenciamento dos tiles(cartas), revelando a carta ao detectar um click sobre a mesma e escondendo as cartas no inicio de jogo e quando o jogador erra o par.
/// </summary>
public class Tile : MonoBehaviour
{
    // private bool tileRevelada = false;              // indicador de carta virada ou não
    public Sprite originalCarta;                    // Sprite da carta desejada
    public Sprite backCarta1;                        // Sprite do verso da carta
    public Sprite backCarta2;
    public int linha;
    public int coluna;

    /*Responsável por detectar o click sobre um tile (carta).*/
    public void OnMouseDown()
    {
        print("Você pressionou num Tile");
        /*if (tileRevelada)
            EscondeCarta();
        else
            RevelaCarta();*/        // Não guarda número de cartas

        GameObject.Find("gameManager").GetComponent<ManageCards>().CartaSelecionada(gameObject);
    }

    /*Responsável por esconder as cartas ao iniciar o jogo e após erros do jogador.*/
    public void EscondeCarta(int i, int j)
    {
        // para cada modo de jogo os versos das cartas respeitam os crítérios necessários
        switch (ManageCards.Mode)
        {
            case 1:
                // no jogo 1 o verso da carta é indiferente
                GetComponent<SpriteRenderer>().sprite = backCarta1;
                // tileRevelada = false;
                break;

            case 2:
                // no jogo 2 a 1ª linha necessariamente é de uma cor e a 2ª linha de outra
                if(i == 0)
                    GetComponent<SpriteRenderer>().sprite = backCarta1;
                else
                    GetComponent<SpriteRenderer>().sprite = backCarta2;

                break;

            default:
                // no jogo 3 a 1ª coluna é de uma cor e a 2ª coluna de outra
                if (j == 0)
                    GetComponent<SpriteRenderer>().sprite = backCarta1;
                else
                    GetComponent<SpriteRenderer>().sprite = backCarta2;

                break;
        }
    }

    /*Responsável por revelar a carta após seleção da mesma*/
    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        // tileRevelada = true;
    }

    /*responsável por fazer a mudança de cartas do jogo.*/
    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }
}
