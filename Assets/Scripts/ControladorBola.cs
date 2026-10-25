using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ControladorBola : MonoBehaviour
{
    // [SerializeField] mostra a variável privada no Inspector,
    // então dá pra ajustar a velocidade sem mexer no código.
    [SerializeField]
    private float vel = 0.2f, limiteVel = 3;

    // Referência ao Rigidbody da bola, que é o componente que
    // controla a física (movimento, gravidade, colisão).
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    public static bool gameOver = false;

    [SerializeField]
    private int moedasNum = 0;

    [SerializeField]
    private Text txtMoedas;

    [SerializeField]
    private GameObject ParticulasMoedas;

    void Awake()
    {
        gameOver = false;
    }

    // Start roda UMA vez, quando o jogo começa.
    void Start()
    {
        txtMoedas.text = moedasNum.ToString();
        // Pega o Rigidbody que está no mesmo objeto deste script
        // e guarda na variável rb. Se o objeto não tiver Rigidbody,
        // rb fica nulo e dá erro no Console.
        gameOver = false;
        rb = GetComponent<Rigidbody>();

        // Define a velocidade inicial: anda no eixo X (direita) com
        // velocidade "vel". Os valores são (X, Y, Z): Y e Z ficam em 0.
        rb.linearVelocity = new Vector3(vel, 0, 0);

        StartCoroutine (AjustaVelocidade());
    }

    // Update roda TODO frame, e é onde lemos o teclado.
    void Update()
    {
        // GetKeyDown é verdadeiro só no frame em que a tecla é apertada
        // (não fica repetindo enquanto você segura).
        if (Input.GetKeyDown(KeyCode.Space)&& !gameOver)
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("moeda"))
        {
            Destroy (col.gameObject);
            moedasNum ++;
            txtMoedas.text = moedasNum.ToString();
            GameObject fx = Instantiate(ParticulasMoedas, transform.position, Quaternion.identity);

            // Faz todos os sistemas de partículas respeitarem a escala
            foreach (ParticleSystem ps in fx.GetComponentsInChildren<ParticleSystem>())
            {
                var main = ps.main;
                main.scalingMode = ParticleSystemScalingMode.Hierarchy;
            }

            fx.transform.localScale = Vector3.one * 0.1f; // 10% do tamanho original
            Destroy(fx, 2f);
            
        }
    }

    IEnumerator AjustaVelocidade()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds (2);
            if(vel < limiteVel)
            {
                vel += 0.1f;
            }
            
        }
    }
}