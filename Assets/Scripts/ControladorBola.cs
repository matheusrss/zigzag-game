using UnityEngine;

public class ControladorBola : MonoBehaviour
{
    // [SerializeField] mostra a variável privada no Inspector,
    // então dá pra ajustar a velocidade sem mexer no código.
    [SerializeField]
    private float vel = 0.2f;

    // Referência ao Rigidbody da bola, que é o componente que
    // controla a física (movimento, gravidade, colisão).
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    public static bool gameOver = false;

    void Awake()
    {
        gameOver = false;
    }

    // Start roda UMA vez, quando o jogo começa.
    void Start()
    {
        // Pega o Rigidbody que está no mesmo objeto deste script
        // e guarda na variável rb. Se o objeto não tiver Rigidbody,
        // rb fica nulo e dá erro no Console.
        gameOver = false;
        rb = GetComponent<Rigidbody>();

        // Define a velocidade inicial: anda no eixo X (direita) com
        // velocidade "vel". Os valores são (X, Y, Z): Y e Z ficam em 0.
        rb.linearVelocity = new Vector3(vel, 0, 0);
    }

    // Update roda TODO frame, e é onde lemos o teclado.
    void Update()
    {
        // GetKeyDown é verdadeiro só no frame em que a tecla é apertada
        // (não fica repetindo enquanto você segura).
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BolaMov();
        }

        //Condicional pra saber se a balinha esta colidindo com algo. 
        //Se não ouver colisão = gameOver
        if(!Physics.Raycast(transform.position, Vector3.down, 1))
        {
            gameOver = true;
            rb.useGravity = true;
        }

        //Se caso der gameover console print
        if(gameOver)
        {
            print ("Caiu!");
        }
        Debug.DrawRay(transform.position, Vector3.down, Color.green);
    }

    // Alterna a direção da bola: X vira Z, Z vira X (movimento em zigue-zague).
    void BolaMov()
    {
        // Se está indo pra X (velocidade em X maior que 0), muda pra Z.
        if (rb.linearVelocity.x > 0)
        {
            rb.linearVelocity = new Vector3(0, 0, vel);
        }
        // Senão, se está indo pra Z, volta pra X.
        else if (rb.linearVelocity.z > 0)
        {
            rb.linearVelocity = new Vector3(vel, 0, 0);
        }
    }
}