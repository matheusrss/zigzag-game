using UnityEngine;

public class TriggerCode : MonoBehaviour
{
    // Referência ao Rigidbody da plataforma. Aparece no Inspector,
    // mas o Start preenche sozinho, então não precisa arrastar nada.
    [SerializeField]
    private Rigidbody rb;

    void Start()
    {
        // Pega o Rigidbody que está NESTE mesmo objeto (a plataforma)
        // e guarda em rb. Se o objeto não tiver Rigidbody, rb fica nulo.
        rb = GetComponentInParent<Rigidbody>();
    }

    // Chamado automaticamente pela Unity no frame em que algum objeto
    // SAI da área deste trigger. "col" é o collider de quem saiu.
    void OnTriggerExit(Collider col)
    {
        // Só continua se quem saiu tem a tag "bolinha".
        // Assim, outros objetos passando pelo trigger são ignorados.
        if (col.gameObject.CompareTag("bolinha"))
        {
            // Liga a gravidade da plataforma: ela começa a cair.
            rb.isKinematic = false; 
            rb.useGravity = true;

            // Destrói este objeto (a plataforma) depois de 0.3 segundos,
            // dando tempo de ela cair um pouco antes de sumir.
            Destroy(gameObject, 0.3f);
        }
    }
}