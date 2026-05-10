using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool pausado = false;

    public GameObject textoPausado;

    void Start()
    {
        Time.timeScale = 1f;
        textoPausado.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
            {
                VoltarJogo();
            }
            else
            {
                PausarJogo();
            }
        }
    }

    void PausarJogo()
    {
        Time.timeScale = 0f;
        pausado = true;

        textoPausado.SetActive(true);
    }

    void VoltarJogo()
    {
        Time.timeScale = 1f;
        pausado = false;

        textoPausado.SetActive(false);
    }
}