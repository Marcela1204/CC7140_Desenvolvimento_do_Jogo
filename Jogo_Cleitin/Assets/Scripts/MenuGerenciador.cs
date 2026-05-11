using UnityEngine;
using UnityEngine.UI;

public class MenuGerenciador : MonoBehaviour
{
    public GameProgress dados; // Arraste seu ScriptableObject aqui
    public Image[] partesImagens; // Arraste as 5 imagens da UI aqui

    public Color corBloqueada = Color.gray;
    public Color corDesbloqueada = Color.white;

    // Chamado sempre que o menu abrir
    void OnEnable()
    {
        AtualizarVisualizacao();
    }

    public void AtualizarVisualizacao()
    {
        for (int i = 0; i < partesImagens.Length; i++)
        {
            if (dados.bossesDerrotados[i] == 1)
            {
                partesImagens[i].color = corDesbloqueada;
            }
            else
            {
                partesImagens[i].color = corBloqueada;
            }
        }
    }
}
