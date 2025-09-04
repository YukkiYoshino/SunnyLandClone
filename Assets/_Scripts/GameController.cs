using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int pontos;
    public TMP_Text txtPontos;

    private int qtdEstrelas;
    public TMP_Text txtEstrelas;

    public Sprite[] imagensVidas;
    public Image imagemVida;
   

    public GameObject painelPause;
    private bool pausado = false;
    private bool fimJogo = false;
    void Start()
    {
        painelPause.SetActive(false); // começa desligado
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !fimJogo)
        {
            PausarOuDespausar();
        }
    }
    public void SofrerDano(int qtdVidas)
    {
        
        switch (qtdVidas)
        {
            case 0:
                imagemVida.sprite = imagensVidas[3];
                break;
            case 1:
                imagemVida.sprite = imagensVidas[2];
                break;
            case 2:
                imagemVida.sprite = imagensVidas[1];
                break;
            case 3:
                imagemVida.sprite = imagensVidas[0];
                break;
        }

    }
    
    public void FimJogo()
    {
        Time.timeScale = 0f; // congela o jogo
    }
         
    public void Pontuacao(int qtdPontos)
    {
        pontos += qtdPontos;
        txtPontos.text = pontos.ToString();
    }
    public void ColetaItem(string nomeItem)
    {
        switch (nomeItem)
        {
            case "ESTRELA":
                qtdEstrelas++;
                txtEstrelas.text = qtdEstrelas.ToString();
                break;
        }
    }

    public void CarregarCenaPorNome(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void FecharJogo()
    {
        Application.Quit();
    }

    public void PausarOuDespausar()
    {
        pausado = !pausado;

        if (pausado)
        {
            Time.timeScale = 0f; // congela o jogo
            painelPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // volta o tempo ao normal
            painelPause.SetActive(false);
        }
    }
}
