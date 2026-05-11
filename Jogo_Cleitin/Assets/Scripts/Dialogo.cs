using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    public GameObject painelDialogo;
    public TextMeshProUGUI textoDialogo;

    [TextArea(2,5)]
    public string[] falas;

    private int indice = 0;
    private bool jogadorPerto = false;
    private bool dialogoAtivo = false;

    void Start()
    {
        painelDialogo.SetActive(false);
    }

    void Update()
    {
        if (painelDialogo == null || textoDialogo == null)
            return;

        if(jogadorPerto && dialogoAtivo && Input.GetKeyDown(KeyCode.E))
        {
            ProximaFala();
        }
    }

    void IniciarDialogo()
    {
        dialogoAtivo = true;
        painelDialogo.SetActive(true);

        indice = 0;
        textoDialogo.text = falas[indice];
    }

    void ProximaFala()
    {
        indice++;

        if(indice < falas.Length)
        {
            textoDialogo.text = falas[indice];
        }
        else
        {
            EncerrarDialogo();
        }
    }

    void EncerrarDialogo()
    {
        dialogoAtivo = false;

        if(painelDialogo != null)
        {
            painelDialogo.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            jogadorPerto = true;
            IniciarDialogo();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            jogadorPerto = false;
            EncerrarDialogo();
        }
    }
}