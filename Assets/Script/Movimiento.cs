using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Movimiento : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minigameCamera;
    public Camera minigameCamera1;
    public Camera minigameCamera2;
    public Camera minigameCamera3;
    public Camera minigamePuzzle;
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

    public TMP_Text scoreText;  
    private int score = 0;  
    private HashSet<int> posicionesVisitadas = new HashSet<int>();  
    public Canvas canvasJugando;
    public Canvas gameOverCanvas;
    public TMP_Text gameOverScoreText;

    public AudioClip clickBoton;
    private AudioSource audioSource;
    public AudioSource Musica;
    public AudioSource Musicaminijuegocoche;


    bool bloqueo = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("MirarAgua",1,0.5f);
        UpdateScoreText();
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
        if (posicionesVisitadas.Add(posicionZ))
        {
            AddPoint();
        }
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
                if(countminijueho==15){
                    Debug.Log("Salta el minijuego");   
                    chef.SetActive(false); 
                    Musica.Pause();
                    Random.InitState(System.DateTime.Now.Millisecond);
                    eligeMinijuego();
                    countminijueho=0;
                }
                countminijueho++;
            }
        }
     } 
    private void eligeMinijuego()
        {
            // Primero, elegimos entre el minijuego de coche o el de memoria
            int juego = Random.Range(0, 1); // 0 para coche, 1 para memoria, 2 para puzzles


            if (juego == 0)
            {
                // Minijuego de coche: elegir aleatoriamente uno de los cuatro niveles
                int nivelCoche = Random.Range(0, 4); // 0, 1, 2, o 3
                mainCamera.gameObject.SetActive(false);
                Musicaminijuegocoche.Play();
                switch(nivelCoche)
                {
                    case 0:
                        minigameCamera.gameObject.SetActive(true);
                        break;
                    case 1:
                        minigameCamera1.gameObject.SetActive(true);
                        break;
                    case 2:
                        minigameCamera2.gameObject.SetActive(true);
                        break;
                    case 3:
                        minigameCamera3.gameObject.SetActive(true);
                        break;
                }
            }
            else if (juego == 1)
            {
                // Minijuego de memoria
                SceneManager.LoadScene("MinijuegoMemoria", LoadSceneMode.Additive);
            }
            else if (juego == 2)
            {
                // Minijuego de puzzles
                mainCamera.gameObject.SetActive(false);
                minigamePuzzle.gameObject.SetActive(true);
            }
        }


    private IEnumerator GameOverSequence()
    {
        canvasJugando.gameObject.SetActive(false);
        gameOverScoreText.text = "Puntuacion: " + score;
        gameOverCanvas.gameObject.SetActive(true);
        yield return null;
    }

    public void ReactivarChef()
    {
        chef.SetActive(true);  // Reactivar el objeto Chef
        Musicaminijuegocoche.Stop();  // Parar Musica
        Musica.Play();  // Reactivar Musica
    }

    private void AddPoint()
    {
        score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void Retry()
    {
        audioSource.PlayOneShot(clickBoton);
        StartCoroutine(ReloadSceneAfterSound());
    }

    public void Exit()
    {
        audioSource.PlayOneShot(clickBoton);
        StartCoroutine(ExitAfterSound());
    }

    private IEnumerator ReloadSceneAfterSound()
    {
        yield return new WaitForSeconds(clickBoton.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ExitAfterSound()
    {
        yield return new WaitForSeconds(clickBoton.length);
        SceneManager.LoadScene("MenuPrincipal");
    }
}
