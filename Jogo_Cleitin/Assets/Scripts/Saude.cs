using UnityEngine;
using UnityEngine.UI; // Necessário para interagir com a UI
using TMPro; // Use isso se estiver usando TextMeshPro (recomendado)
using UnityEngine.SceneManagement; // ESSENCIAL para trocar de fase
using System.Collections;

public class Saude : MonoBehaviour
{
	public int bossIndex; // Defina 0, 1, 2 ou 3 no Inspector
    public GameProgress progress;

    public int vidaMaxima = 100;
    public int vidaAtual;

    [Header("Configurações de UI")]
    public TextMeshProUGUI textoVida; // Se usar texto simples, use 'public Text textoVida'

    [Header("Configurações de Cena")]
    public string nomeDaCenaPrincipal = "Principal";


    private SpriteRenderer spriteRenderer;
    private Collider2D colisor;

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarInterface();
    }

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima); // Garante que não fique negativo

        AtualizarInterface();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void AtualizarInterface()
    {

        if (textoVida != null)
        {
            textoVida.text = vidaAtual.ToString() + " / " + vidaMaxima.ToString();
        }
    }


    void Morrer()
    {
        Debug.Log(gameObject.name + " morreu!");

        if (gameObject.CompareTag("Inimigo"))
        {
		progress.bossesDerrotados[bossIndex] = 1;
        Debug.Log($"Boss {bossIndex} derrotado!");
            // Inicia a contagem regressiva antes de mudar de cena
            if (spriteRenderer != null) spriteRenderer.enabled = false;

            // 2. Desativa o colisor para não levar mais tiros e o player passar por dentro
            if (colisor != null) colisor.enabled = false;


            StartCoroutine(EsperarECarregar());
        }
        else if (gameObject.CompareTag("Player"))
        {
            // Lógica opcional se o player morrer (ex: reiniciar fase)
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	    SceneManager.LoadScene(nomeDaCenaPrincipal);
        }
    }

    IEnumerator EsperarECarregar()
    {
        // Desativa o movimento/ataque do inimigo aqui se necessário
        // GetComponent<IAInimigo>().enabled = false; 

        yield return new WaitForSeconds(2f); // Espera 2 segundos

        SceneManager.LoadScene(nomeDaCenaPrincipal);
    }
}

