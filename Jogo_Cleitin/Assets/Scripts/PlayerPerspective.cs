using UnityEngine;

public class PlayerPerspective : MonoBehaviour
{
    [Header("Configurações de Limite (Mundo)")]
    public float yMinimo; // Ponto mais baixo da tela (Personagem Grande)
    public float yMaximo; // Ponto mais alto da caminhada (Personagem Pequeno)

    [Header("Configurações de Escala")]
    public float escalaNoMinimo = 1.0f;
    public float escalaNoMaximo = 0.5f;

    void Update()
    {
        // 1. Calcula onde o player está entre o Min e o Max (retorna de 0 a 1)
        float t = Mathf.InverseLerp(yMinimo, yMaximo, transform.position.y);

        // 2. Calcula a nova escala baseada nesse percentual
        float novaEscala = Mathf.Lerp(escalaNoMinimo, escalaNoMaximo, t);

        // 3. Aplica a escala mantendo o sinal do FlipX (se você usa LocalScale para o flip)
        float direcaoX = Mathf.Sign(transform.localScale.x);
        transform.localScale = new Vector3(novaEscala * direcaoX, novaEscala, 1f);
    }

    // Desenha linhas no editor para facilitar o ajuste
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-10, yMinimo, 0), new Vector3(10, yMinimo, 0));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(-10, yMaximo, 0), new Vector3(10, yMaximo, 0));
    }
}
