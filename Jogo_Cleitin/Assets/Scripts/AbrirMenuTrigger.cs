using UnityEngine;

public class InteracaoMenuPartes : MonoBehaviour
{
    [Header("Referências")]
    public GameObject painelMenu; // O menu que contém as 5 partes
    public GameObject avisoInteracao; // O texto "Pressione E"

    private bool jogadorEstaNoTrigger = false;

    void Update()
    {
        // 1. Verificamos se o jogador está na área
        // 2. Verificamos se a tecla E foi pressionada
        if (jogadorEstaNoTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        if (painelMenu != null)
        {
            // Inverte o estado atual (se aberto, fecha; se fechado, abre)
            bool estaAtivo = !painelMenu.activeSelf;
            painelMenu.SetActive(estaAtivo);

            // Opcional: Pausar o tempo ou liberar o mouse se o menu abrir
            // Cursor.lockState = estaAtivo ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorEstaNoTrigger = true;
            if (avisoInteracao != null) avisoInteracao.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorEstaNoTrigger = false;
            if (avisoInteracao != null) avisoInteracao.SetActive(false);
            
            // Fecha o menu automaticamente ao se afastar
            if (painelMenu != null) painelMenu.SetActive(false);
        }
    }
}
