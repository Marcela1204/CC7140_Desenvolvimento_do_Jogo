using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    public Transform player;
    public float velocidade = 3f;
    public float distanciaDetecao = 5f;
    
    private Animator anim;
    private SpriteRenderer sprite;

    void Start() {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia < distanciaDetecao) {
            SeguirPlayer();
        } else {
            Parar();
        }
    }

    void SeguirPlayer() {
        // Calcula a direção para o player
        Vector2 direcao = (player.position - transform.position).normalized;

        // Move o inimigo
        transform.position = Vector2.MoveTowards(transform.position, player.position, velocidade * Time.deltaTime);

        // ENVIANDO OS VALORES PARA A BLEND TREE
        // Usamos a direção para simular o Input que o jogador usaria
        anim.SetFloat("Horizontal", direcao.x);
        anim.SetFloat("Vertical", direcao.y);

        // Reutiliza sua lógica de FlipX
        sprite.flipX = direcao.x < 0;
    }

    void Parar() {
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
    }
}
