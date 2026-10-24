using UnityEngine;

public class RotacaoMoeda : MonoBehaviour
{
    [SerializeField]
    private float vel = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, vel * Time.deltaTime));
    }
}
