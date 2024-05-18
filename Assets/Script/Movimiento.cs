using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movimiento : MonoBehaviour
{
    public int carril;
    public int lateral;
    public int posicionZ;
    public Vector3 posObjetivo;
    public float velocidad=18;
    public Mundo mundo;
    public Transform grafico;
    public LayerMask capaObstaculos;
    public LayerMask capaAgua;
    public LayerMask capaSueloSeguro;
    public float distanciaVista=1;
    public bool vivo = true;
    public Animator animaciones;
    public AnimationCurve curva;

    private int countminijueho=0;
    public GameObject chef;


    bool bloqueo = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MirarAgua",1,0.5f);
        //InvokeRepeating("MirarSueloSeguro",1,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarPosicion();
        if(Input.GetKeyDown(KeyCode.W))
        {
            Avanzar();
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            Retroceder();
        }else if(Input.GetKeyDown(KeyCode.A)){
            MoverLados(-1);
        }else if(Input.GetKeyDown(KeyCode.D)){
            MoverLados(1);
        }
    }
    private void OnDrawGizmos()
    {
        Ray rayo = new Ray(grafico.position + Vector3.one * 0.5f,grafico.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(grafico.position + Vector3.up * 0.5f,grafico.position + Vector3.up * 0.5f + grafico.forward * distanciaVista);
    }
    public void ActualizarPosicion(){

        if(!vivo){

           StartCoroutine(GameOverSequence());
        }
        

    }
      

    public IEnumerator CambiarPosicion()
    {
        bloqueo = true;
        posObjetivo =new Vector3(lateral,0,posicionZ);
        Vector3 posActual = transform.position;

        for(int i=0;i<10;i++)
        {

            transform.position =Vector3.Lerp(posActual,posObjetivo, i * 0.1f) + Vector3.up * curva.Evaluate(i * 0.1f);
            yield return new WaitForSeconds(1f/velocidad);
        } 

        bloqueo = false;
    }
        
    

    public void Avanzar()
    {
        MirarSueloSeguro();
        if(!vivo || bloqueo){
            return;
        }
        grafico.eulerAngles = new Vector3(0,0,0);
        if(MirarAdelante())
        {
            return;
        }
        posicionZ++;
        if (posicionZ > carril)
        {
            carril=posicionZ;
            mundo.CrearSuelos();
        }
        StartCoroutine(CambiarPosicion());
    }
    public void Retroceder()
    {
        if(!vivo || bloqueo){
            return;
        }
        grafico.eulerAngles = new Vector3(0,180,0);
        if(MirarAdelante())
        {
            return;
        }
        if (posicionZ > carril-3)
        {
            posicionZ--;
        }
        StartCoroutine(CambiarPosicion());
    }
    public void MoverLados(int cuanto){
        if(!vivo || bloqueo){
            return;
        }
        grafico.eulerAngles = new Vector3(0,90*cuanto,0);
        if(MirarAdelante())
        {
            return;
        }
        lateral+=cuanto;
        lateral=Mathf.Clamp(lateral,-4,4);
        StartCoroutine(CambiarPosicion());
    }
    
    public bool MirarAdelante()
    {
        RaycastHit hit;
        Ray rayo = new Ray(grafico.position + Vector3.up * 0.5f,grafico.forward);
        if(Physics.Raycast(rayo,out hit,distanciaVista, capaObstaculos))
        {
            return true; 
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coche"))
        {
            animaciones.SetTrigger("morir");
            vivo=false;
        }
    }
    public void MirarAgua() 
    {
        RaycastHit hit;
        Ray rayo = new Ray(grafico.position + Vector3.up,Vector3.down);
        if(Physics.Raycast(rayo,out hit,3, capaAgua))
        {
            if(hit.collider.CompareTag("Agua"))
            {
                animaciones.SetTrigger("agua");
                vivo=false;
            }
        }
    }
     public void MirarSueloSeguro(){
        RaycastHit hit;
        Ray rayo = new Ray(grafico.position + Vector3.up+ Vector3.forward,Vector3.down);
        if(Physics.Raycast(rayo,out hit,2, capaSueloSeguro))
        {
            if(hit.collider.CompareTag("sueloseguro"))
            {
                if(countminijueho==20){
                    Debug.Log("Salta el minijuego");   
                    chef.SetActive(false); 
                    SceneManager.LoadScene("MinijuegoMemoria", LoadSceneMode.Additive); 
                    //Aqui salta el minijuego  
                    countminijueho=0;
                }
                countminijueho++;
            }
        }
     } 
    private IEnumerator GameOverSequence()
    {
        // Añadir una pantalla de game over aquí

        yield return new WaitForSeconds(2); // Espera 2 segundos

        SceneManager.LoadScene("MenuPrincipal"); // Redirige a la escena "menu Principal"
    }

    public void ReactivarChef()
    {
        chef.SetActive(true);  // Reactivar el objeto Chef
    }
}
