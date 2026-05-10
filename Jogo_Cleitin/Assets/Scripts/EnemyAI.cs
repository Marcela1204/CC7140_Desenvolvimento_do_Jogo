using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    public Transform player;
    public float velocidade = 3f;
    public float distanciaDetecao = 5f;

    private Animator anim;
    private SpriteRenderer sprite;

    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;
    public float intervaloAtaque = 1.5f;
    private float tempoUltimoAtaque;

    public float distanciaParaAtirar = 5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia < distanciaDetecao)
        {
            SeguirPlayer();
        }
        else
        {
            Parar();
        }
        float distanciashoot = Vector2.Distance(transform.position, player.position);

        if (distanciashoot <= distanciaParaAtirar && Time.time >= tempoUltimoAtaque + intervaloAtaque)
        {
            Atirar();
            tempoUltimoAtaque = Time.time;
        }
    }

    void Atirar()
    {
        // Faz o inimigo "olhar" para o player antes de atirar (opcional, dependendo do seu setup)
        Vector2 direcao = (player.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        pontoDeDisparo.rotation = Quaternion.Euler(0, 0, angulo);

        Instantiate(projetilPrefab, pontoDeDisparo.position, pontoDeDisparo.rotation);
    }

    void SeguirPlayer()
    {
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

    void Parar()
    {
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
    }
}
