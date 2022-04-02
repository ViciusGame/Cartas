using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
/// Esta classe é o principal controlador do jogo, responsável pelos parâmetros.
///</summary>
public class ManageCards : MonoBehaviour
{
    public static int Mode;

    public GameObject carta;                // Carta a ser descartada
    private bool primeiraCartaSelecionada, segundaCartaSelecionada; // indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2;      // GameObject da 1ª e 2ª carta selecionada
    private string linhaCarta1, linhaCarta2; // linha da carta selecionada

    bool timerPausado, timerAcionado;       // indicador de pausa no Timer ou Start do Timer
    float timer;                            // Variável de tempo

    int numTentativas = 0;                  // número de tentativas na rodada
    int numAcertos = 0;                     // número de pares acertados
    AudioSource somOK;                      // som do acerto

    int ultimoJogo = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("mode").GetComponent<Text>().text = "MODO: " + Mode;

        GameObject.Find("recorde").GetComponent<Text>().text = "RECORDE: " + PlayerPrefs.GetInt("Recorde" + Mode, 999);
        ultimoJogo = PlayerPrefs.GetInt("Jogadas" + Mode, 0);
        
        MostraCartas();
        UpdateTentativas();
        somOK = GetComponent<AudioSource>();

        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Jogo Anterior = " + ultimoJogo;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            print(timer);
            if (timer > 1)
            {
                timerPausado = true;
                timerAcionado = false;

                // Para cada modo de jogo é aplicado os devidos critérios
                switch (Mode)
                {
                    case 1:
                        //valor igual e a linha diferente para formar um par
                        if(carta1.tag == carta2.tag && linhaCarta1 != linhaCarta2)
                        {
                            Destroy(carta1);
                            Destroy(carta2);
                            numAcertos++;
                            somOK.Play();
                    
                            if(numAcertos == 13)
                            {
                                PlayerPrefs.SetInt("Jogadas1", numTentativas);

                                // se o número de tentativas for menor que o recorde, vira o novo recorde
                                if (numTentativas < PlayerPrefs.GetInt("Recorde1", 0))
                                    PlayerPrefs.SetInt("Recorde1", numTentativas);
                                
                                // para vencer o jogo basta terminar com menos tentativas que a partida anterior
                                if(numTentativas <= ultimoJogo || ultimoJogo < 13)
                                    SceneManager.LoadScene("WinScreen");
                                else
                                    SceneManager.LoadScene("LoseScreen");
                            }
                        }
                        else
                        {
                            //Esconde as cartas caso não fosse um par
                            carta1.GetComponent<Tile>().EscondeCarta(carta1.GetComponent<Tile>().linha, carta1.GetComponent<Tile>().coluna);
                            carta2.GetComponent<Tile>().EscondeCarta(carta2.GetComponent<Tile>().linha, carta2.GetComponent<Tile>().coluna);
                        }
                        break;

                    case 2:
                        //sprites iguais e linhas diferentes para serem um par
                        if (carta1.GetComponent<Tile>().originalCarta == carta2.GetComponent<Tile>().originalCarta && linhaCarta1 != linhaCarta2)
                        {
                            Destroy(carta1);
                            Destroy(carta2);
                            numAcertos++;
                            somOK.Play();

                            if (numAcertos == 13)
                            {
                                PlayerPrefs.SetInt("Jogadas2", numTentativas);

                                if (numTentativas < PlayerPrefs.GetInt("Recorde2", 0))
                                    PlayerPrefs.SetInt("Recorde2", numTentativas);

                                if (numTentativas <= ultimoJogo || ultimoJogo < 13)
                                    SceneManager.LoadScene("WinScreen");
                                else
                                    SceneManager.LoadScene("LoseScreen");
                            }
                        }
                        else
                        {
                            carta1.GetComponent<Tile>().EscondeCarta(carta1.GetComponent<Tile>().linha, carta1.GetComponent<Tile>().coluna);
                            carta2.GetComponent<Tile>().EscondeCarta(carta2.GetComponent<Tile>().linha, carta2.GetComponent<Tile>().coluna);
                        }
                        break;

                    case 3:
                        //sprites iguais e linhas diferentes para serem um par (Nesse modo as linhas estão numeradas de 0 a 7 dividida em duas colunas)
                        if (carta1.GetComponent<Tile>().originalCarta == carta2.GetComponent<Tile>().originalCarta && linhaCarta1 != linhaCarta2)
                        {
                            Destroy(carta1);
                            Destroy(carta2);
                            numAcertos++;
                            somOK.Play();

                            if (numAcertos == 16)
                            {
                                PlayerPrefs.SetInt("Jogadas3", numTentativas);

                                if (numTentativas < PlayerPrefs.GetInt("Recorde3", 0))
                                    PlayerPrefs.SetInt("Recorde3", numTentativas);

                                if (numTentativas <= ultimoJogo || ultimoJogo < 13)
                                    SceneManager.LoadScene("WinScreen");
                                else
                                    SceneManager.LoadScene("LoseScreen");
                            }
                        }
                        else
                        {
                            carta1.GetComponent<Tile>().EscondeCarta(carta1.GetComponent<Tile>().linha, carta1.GetComponent<Tile>().coluna);
                            carta2.GetComponent<Tile>().EscondeCarta(carta2.GetComponent<Tile>().linha, carta2.GetComponent<Tile>().coluna);
                        }
                        break;
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                timer = 0;
            }
        }
    }

    /*Responsável por criar a matriz de cartas a ser usada no jogo.*/
    void MostraCartas()
    {
        if(Mode == 1 || Mode == 2)
        {
            int[] arrayEmbaralhado = criaArrayEmbaralhado();
            int[] arrayEmbaralhado2 = criaArrayEmbaralhado();

            for (int i = 0; i < 13; i++)
            {
                AddUmaCarta(0, i, arrayEmbaralhado[i], 0);
                AddUmaCarta(1, i, arrayEmbaralhado2[i], 0);
            }
        }
        else
        {
            int[] arrayEmbaralhado = criaArrayEmbaralhado();
            int[] arrayEmbaralhado2 = criaArrayEmbaralhado();
            int[] arrayEmbaralhado3 = criaArrayEmbaralhado();
            int[] arrayEmbaralhado4 = criaArrayEmbaralhado();

            int[] arrayEmbaralhado5 = criaArrayEmbaralhado();
            int[] arrayEmbaralhado6 = criaArrayEmbaralhado();
            int[] arrayEmbaralhado7 = criaArrayEmbaralhado();
            int[] arrayEmbaralhado8 = criaArrayEmbaralhado();

            for (int i = 0; i < 4; i++)
            {
                AddUmaCarta(0, i, arrayEmbaralhado[i], 0);
                AddUmaCarta(1, i, arrayEmbaralhado2[i], 0);
                AddUmaCarta(2, i, arrayEmbaralhado3[i], 0);
                AddUmaCarta(3, i, arrayEmbaralhado4[i], 0);
                
                AddUmaCarta(4, i, arrayEmbaralhado5[i], 1);
                AddUmaCarta(5, i, arrayEmbaralhado6[i], 1);
                AddUmaCarta(6, i, arrayEmbaralhado7[i], 1);
                AddUmaCarta(7, i, arrayEmbaralhado8[i], 1);
            }
        }
    }

    /*Responsável por adicionar cartas à matriz de cartas do jogo*/
    void AddUmaCarta(int linha, int rank, int valor, int coluna)
    {
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorDeEscalaX = (275 * escalaCartaOriginal) / 110.0f;
        float fatorDeEscalaY = (400 * escalaCartaOriginal) / 110.0f;

        // posição da carta na cena
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2 + coluna*9) * fatorDeEscalaX), centro.transform.position.y + ((-(linha%4) + 1) * fatorDeEscalaY), centro.transform.position.z);
        
        // para cada carta é criado um gameobject e é salvo algumas de suas características 
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.tag = "" + (valor);
        c.name = "" + linha + " " + valor;
        c.GetComponent<Tile>().linha = linha;
        c.GetComponent<Tile>().coluna = coluna;

        string nomeDaCarta = "";
        string numeroCarta = "";

        // atribuição dos nomes das cartas de valores especiais
        if (valor == 0)
            numeroCarta = "ace";
        else if (valor == 10)
            numeroCarta = "jack";
        else if (valor == 11)
            numeroCarta = "queen";
        else if (valor == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (valor + 1);

        if(Mode == 1 || Mode == 3)
        {
            // escolhe o nipe das cartas utilizando a var linha 
            switch (linha%4)
            {
                case 0:
                    nomeDaCarta = numeroCarta + "_of_clubs";
                    break;

                case 1:
                    nomeDaCarta = numeroCarta + "_of_hearts";
                    break;

                case 2:
                    nomeDaCarta = numeroCarta + "_of_spades";
                    break;

                default:
                    nomeDaCarta = numeroCarta + "_of_diamonds";
                    break;
            }
        }else if(Mode == 2)
        {
            nomeDaCarta = numeroCarta + "_of_clubs";
        }
       
        // atribui a sprite correspondente ao nome da carta
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);

        c.GetComponent<Tile>().EscondeCarta(linha, coluna);

        GameObject.Find("" + linha + " " + valor).GetComponent<Tile>().setCartaOriginal(s1);
    }

    /*Responsavel por criar o array já embaralhado de cartas para futura adição ao método que forma
    * a matriz de cartas.*/
    public int[] criaArrayEmbaralhado()
    {
        if(Mode == 1 || Mode == 2)
        {
            int[] novoArray = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            int temp;
            for(int t = 0; t < 13; t++)
            {
                temp = novoArray[t];
                int r = Random.Range(t, 13);
                novoArray[t] = novoArray[r];
                novoArray[r] = temp;
            }
            return novoArray;
        }
        else
        {
            // o jogo 3 utiliza apenas 4 valores de carta
            int[] novoArray = new int[] {0, 10, 11, 12};
            int temp;
            for (int t = 0; t < 4; t++)
            {
                temp = novoArray[t];
                int r = Random.Range(t, 4);
                novoArray[t] = novoArray[r];
                novoArray[r] = temp;
            }
            return novoArray;
        }
    }

    /*Responsável por chamar o método que revela a carta e por decidir se a segunda carta selecionada
    * é igual ou não à primeira.*/
    public void CartaSelecionada(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;
            carta1.GetComponent<Tile>().RevelaCarta();

        }
        else if (primeiraCartaSelecionada && !segundaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            primeiraCartaSelecionada = true;
            carta2 = carta;
            carta2.GetComponent<Tile>().RevelaCarta();
            VerificaCartas();
        }     
    }

    /*Responsável por iniciar o timer e pelo contador de tentativas.*/
    public void VerificaCartas()
    {
        DisparaTimer();
        numTentativas++;
        UpdateTentativas();
    }

    /*Responsável pelo timer do jogo.*/
    public void DisparaTimer()
    {
        timerPausado = false;
        timerAcionado = true;
    }

    /*Responsável por administrar o número de tentativas que o jogador faz para finalizar o jogo.*/
    void UpdateTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
