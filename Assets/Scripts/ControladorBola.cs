using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //Variaveis Game over
    [SerializeField]
    private Text txtbtn, txtGO;
    [SerializeField]
    private Image imgBtn, imgFundo;
    [SerializeField]
    private bool controle;


    void Awake()
    {
        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        gameOver = false;
    }

    // Start roda UMA vez, quando o jogo começa.
    void Start()
    {
        moedasNum = PlayerPrefs.GetInt("MoedasGame");
        txtMoedas.text = moedasNum.ToString();
        // Pega o Rigidbody que está no mesmo objeto deste script
        // e guarda na variável rb. Se o objeto não tiver Rigidbody,
        // rb fica nulo e dá erro no Console.
        rb = GetComponent<Rigidbody>();

        // Define a velocidade inicial: anda no eixo X (direita) com
        // velocidade "vel". Os valores são (X, Y, Z): Y e Z ficam em 0.
        rb.linearVelocity = new Vector3(vel, 0, 0);

        txtGO = GameObject.FindWithTag ("txtgo").GetComponent<Text>();
        txtbtn = GameObject.FindWithTag ("txtbtn").GetComponent<Text>();
        imgBtn = GameObject.FindWithTag ("imgbtn").GetComponent<Image>();
        imgFundo = GameObject.FindWithTag ("imgfundo").GetComponent<Image>();

        controle = true;
        txtbtn.enabled = false;
        txtGO.enabled = false;
        imgBtn.enabled = false;
        imgFundo.enabled = false;

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

        if (gameOver && controle)
        {
            PlayerPrefs.SetInt("MoedasGame", moedasNum);
            txtbtn.enabled = true;
            txtGO.enabled = true;
            imgBtn.enabled = true;
            imgFundo.enabled = true;
            controle = false;
        
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

    public void Novamente()
    {
        SceneManager.LoadScene(0);
    }
}