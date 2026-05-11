using UnityEngine;

[CreateAssetMenu(fileName = "GameProgress", menuName = "ScriptableObjects/Progress")]
public class GameProgress : ScriptableObject
{
    // Vetor de 4 posições (0 a 3)
    // 0 = Ainda não derrotado, 1 = Derrotado
    public int[] bossesDerrotados = new int[5];

    public void ResetProgress()
    {
        for (int i = 0; i < bossesDerrotados.Length; i++)
            bossesDerrotados[i] = 0;
    }
}
