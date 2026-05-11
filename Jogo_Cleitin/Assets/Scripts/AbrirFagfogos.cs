using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirFagfogos : MonoBehaviour
{
    public string nomeDaCena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Fagfogos");
        }
    }
}