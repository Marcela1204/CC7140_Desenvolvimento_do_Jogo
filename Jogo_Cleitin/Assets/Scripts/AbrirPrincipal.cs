using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirPrincipal : MonoBehaviour
{
    public string nomeDaCena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Principal");
        }
    }
}