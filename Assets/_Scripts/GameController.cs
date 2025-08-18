using TMPro;
using UnityEngine;
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
}
