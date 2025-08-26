using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    private int pontos;
    public TMP_Text txtPontos;

    public void Pontuacao(int qtdPontos)
    {
        pontos += qtdPontos;
        txtPontos.text = pontos.ToString();
    }

    public void CarregarCenaPorNome(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void FecharJogo()
    {
        Application.Quit();
    }


    public GameObject painelPause; 
    private bool pausado = false;

    void Start()
    {
        painelPause.SetActive(false); // começa desligado
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PausarOuDespausar();
        }
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
