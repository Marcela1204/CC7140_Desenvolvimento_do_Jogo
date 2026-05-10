

using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float velocidade = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    // Removido o SpriteRenderer daqui se não for mais usar o flipX especificamente

    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;

    public float velocidadeCaminhada = 5f;
    public float velocidadeCorrida = 10f;
    private float velocidadeAtual;

    Vector2 input;
    private bool olhandoParaDireita = true; // Variável para controlar o estado da face

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Lógica de Flip usando Escala (Inverte o objeto inteiro)
        if (input.x < 0 && olhandoParaDireita)
        {
            Flip();
        }
        else if (input.x > 0 && !olhandoParaDireita)
        {
            Flip();
        }

        // Atualiza Animator
        animator.SetFloat("Horizontal", Mathf.Abs(input.x));
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);

        // Velocidade de Corrida
        velocidadeAtual = (Input.GetKey(KeyCode.LeftShift)) ? velocidadeCorrida : velocidadeCaminhada;

        if (Input.GetButtonDown("Fire1"))
        {
            Atirar();
        }
    }

    void Flip()
    {
        olhandoParaDireita = !olhandoParaDireita;

        // Inverte o eixo X da escala local do Player
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void Atirar()
    {
        // Agora o pontoDeDisparo.rotation sempre estará correto pois o Player girou
        // Instantiate(projetilPrefab, pontoDeDisparo.position, pontoDeDisparo.rotation);
        // Instancia o projétil
        GameObject bala = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.identity);

        // Verifica para onde o player está olhando através da escala
        if (transform.localScale.x < 0)
        {
            // Se a escala for negativa, o player está para a esquerda. 
            // Giramos o projétil em 180 graus no eixo Z.
            bala.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            // Se for positiva, ele está para a direita (rotação zero).
            bala.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input.normalized * velocidadeAtual * Time.fixedDeltaTime);
    }
}
