using UnityEngine;

public class PersegueBolinha : MonoBehaviour
{
    // O objeto que a câmera vai seguir (arrasta a bolinha aqui no Inspector).
    [SerializeField]
    private Transform bolinha;

    // A distância (offset) entre a bolinha e a câmera.
    // É calculada no Start, então a câmera mantém sempre o
    // mesmo "afastamento" que tinha quando o jogo começou.
    [SerializeField]
    private Vector3 dist;

    // Velocidade da suavização. Quanto MAIOR, mais rápido a câmera
    // alcança a bolinha. Quanto menor, mais "preguiçosa" e suave ela fica.
    [SerializeField]
    private float lerpVal;

    // pos = posição atual da câmera.
    // targetPos = posição onde a câmera DEVERIA estar pra manter a distância.
    // (Estão serializadas só pra você poder ver os valores no Inspector.)
    [SerializeField]
    private Vector3 pos, targetPos;

    void Start()
    {
        // Calcula e guarda a distância inicial entre a bolinha e a câmera.
        // Como é feita só uma vez, a posição inicial que você montou
        // na cena vira o "enquadramento" que será mantido.
        dist = bolinha.position - transform.position;
    }

    void Update()
    {

    }

    // LateUpdate roda DEPOIS de todos os Update do frame.
    // Pra câmera é o lugar ideal: primeiro a bolinha se mexe,
    // depois a câmera vai atrás. Evita aquele tremor/atraso de um frame.
    //Coloquei um if pra caso der game over a camera parar de seguir.
    void LateUpdate()
    {
        if (!ControladorBola.gameOver)
        {
            PersegueFunc();
        }
    }

    void PersegueFunc()
    {
        // 1. Pega a posição atual da câmera.
        pos = transform.position;

        // 2. Calcula onde a câmera deveria estar: a posição da bolinha
        //    menos a distância guardada. Isso mantém o mesmo ângulo e afastamento.
        targetPos = bolinha.position - dist;

        // 3. Lerp = "interpolação linear": em vez de pular direto pro destino,
        //    anda uma fração do caminho entre a posição atual e o alvo.
        //    O resultado é um movimento suave de "perseguição".
        pos = Vector3.Lerp(pos, targetPos, lerpVal);

        // 4. Aplica a nova posição na câmera.
        transform.position = pos;
    }
}