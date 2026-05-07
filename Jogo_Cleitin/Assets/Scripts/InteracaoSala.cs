using UnityEngine;

public class InteracaoSala : MonoBehaviour
{
    [Header("Configurações de UI")]
    public GameObject imagemNoite; // Arraste a 'ImagemNoite' da UI para cá

    private bool jogadorEstaNoTrigger = false;
    private bool noiteAtivada = false;

    void Update()
    {
        // Verifica se o jogador está na área E apertou a tecla E
        if (jogadorEstaNoTrigger && Input.GetKeyDown(KeyCode.E))
        {
            AlternarNoite();
        }
    }

    void AlternarNoite()
    {
        noiteAtivada = !noiteAtivada; // Inverte o valor da variável (true/false)
        
        if (imagemNoite != null)
        {
            imagemNoite.SetActive(noiteAtivada); // Liga ou desliga a imagem
        }

        Debug.Log("Estado da noite: " + noiteAtivada);
    }

    // Detecta quando o Player entra na área
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorEstaNoTrigger = true;
        }
    }

    // Detecta quando o Player sai da área
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorEstaNoTrigger = false;
            
            // Opcional: Desativar a imagem automaticamente ao sair da frente da sala
            // noiteAtivada = false;
            // imagemNoite.SetActive(false);
        }
    }
}
