using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float velocidade = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float velocidadeCaminhada = 5f;
	public float velocidadeCorrida = 10f;
	private float velocidadeAtual;

    Vector2 input;

    void Update()
    {
        // Pega as teclas (Setas ou WASD)
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Configura o Flip (Espelhamento) para a Esquerda
        if (input.x < 0) spriteRenderer.flipX = true;
        else if (input.x > 0) spriteRenderer.flipX = false;

        // Atualiza os parâmetros do Animator
        // Usamos Abs(input.x) para que, mesmo indo para a esquerda (-1), 
        // o Animator entenda que deve tocar a animação da direita (1) espelhada.
        animator.SetFloat("Horizontal", Mathf.Abs(input.x)); 
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);

	if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
    {
        velocidadeAtual = velocidadeCorrida;
    }
    else
    {
        velocidadeAtual = velocidadeCaminhada;
    }

    // Aplica o movimento usando a velocidadeAtual
    float moveX = Input.GetAxisRaw("Horizontal");
    float moveY = Input.GetAxisRaw("Vertical");
    Vector2 movimento = new Vector2(moveX, moveY).normalized;

    transform.Translate(movimento * velocidadeAtual * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Move o personagem fisicamente
        rb.MovePosition(rb.position + input.normalized * velocidade * Time.fixedDeltaTime);
	float velocidadeAtual = input.sqrMagnitude; 
	
	animator.SetFloat("Speed", velocidadeAtual);
    }
}
