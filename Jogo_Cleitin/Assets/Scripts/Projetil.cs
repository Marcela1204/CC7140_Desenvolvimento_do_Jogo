using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    public float tempoDeVida = 3f;

    void Start()
    {
        // Destrói o projétil após alguns segundos para não pesar no jogo
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        // Move o projétil para frente
        transform.Translate(Vector2.right * velocidade * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lógica de dano aqui (ex: collision.GetComponent<Saude>().TomarDano(10))
        Saude sistemaSaude = collision.GetComponent<Saude>();

        if (sistemaSaude != null)
        {
            sistemaSaude.TomarDano(10); // Define quanto de dano cada tiro tira
        }

        Destroy(gameObject); // O projétil some ao bater
    }
}
