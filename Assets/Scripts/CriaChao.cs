using System.Collections;
using UnityEngine;

public class CriaChao : MonoBehaviour
{
    // O objeto da plataforma que será clonado (arrasta aqui no Inspector).
    [SerializeField]
    private GameObject chao; // o PREFAB (pra clonar)
    [SerializeField]
    private Transform plataformaInicial; // o Cube (1) da CENA

    // Tamanho da plataforma no plano XZ. É o "passo" entre uma plataforma
    // e a próxima, pra elas ficarem encostadas, sem vão e sem sobrepor.
    [SerializeField]
    private float tamanhoXZ;

    // Posição da última plataforma criada. A próxima nasce a partir dela.
    [SerializeField]
    private Vector3 ultimaPos;

    void Start()
    {
        // Começa a contar a partir da posição do chão original da cena.
        ultimaPos = plataformaInicial.position;

        // Guarda o tamanho da plataforma (escala em X) pra usar como passo.
        tamanhoXZ = chao.transform.localScale.x;

        // Inicia a coroutine, uma função que pode "pausar" e continuar depois,
        // rodando em paralelo ao jogo sem travar nada.
        StartCoroutine(CriaChaoInGame());
    }

    // Coroutine: o tipo IEnumerator permite usar yield return pra esperar.
    IEnumerator CriaChaoInGame()
    {
        // Repete enquanto a bolinha NÃO tiver caído.
        while (ControladorBola.gameOver != true)
        {
            // Pausa esta função por 0.5s (o resto do jogo segue normal).
            yield return new WaitForSeconds(0.5f);

            // Passados os 0.5s, cria uma plataforma em X ou Z.
            criaChaoXZ();
        }
        // Quando gameOver vira true, o while termina e a coroutine acaba.
    }

    void Update()
    {

    }

    // Cria uma plataforma colada à anterior, no sentido do eixo X.
    void criaX()
    {
        Vector3 tempPos = ultimaPos;   // copia a última posição
        tempPos.x += tamanhoXZ;        // anda um "passo" em X
        ultimaPos = tempPos;           // atualiza a última posição
        Instantiate(chao, tempPos, Quaternion.identity); // cria o clone sem rotação
    }

    // Igual ao criaX, mas no eixo Z.
    void criaZ()
    {
        Vector3 tempPos = ultimaPos;
        tempPos.z += tamanhoXZ;
        ultimaPos = tempPos;
        Instantiate(chao, tempPos, Quaternion.identity);
    }

    // Sorteia a direção da próxima plataforma (50% X, 50% Z).
    void criaChaoXZ()
    {
        // Random.Range com int: sorteia de 0 a 9 (o 10 não entra).
        int temp = Random.Range(0, 10);

        if (temp < 5)
        {
            criaX();
        }
        else if (temp >= 5)
        {
            criaZ();
        }
    }
}