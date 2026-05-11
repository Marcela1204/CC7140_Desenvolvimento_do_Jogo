using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalFinal : MonoBehaviour
{
    public string nomeDaCena;
    public GameProgress dados;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int paginasColetadas = ContarPaginas();

            if (paginasColetadas == 4)
            {
                SceneManager.LoadScene("Boss");
            }
            else
            {
                Debug.Log("Faltam páginas!");
            }
        }
    }

    int ContarPaginas()
    {
        int total = 0;

        for (int i = 1; i < dados.bossesDerrotados.Length; i++)
        {
            if (dados.bossesDerrotados[i] == 1)
            {
                total++;
            }
        }

        return total;
    }
}