using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

////[RequireComponent(typeof(KinectManager))]
public class GameManager : MonoBehaviour {
    public int m_NumeroCanicas = 5;
    public float radioJuego = 7.5f;
    public int m_LanzamientoNumero = 0;
    //deberia tener algunos delay
    public CameraControl m_CameraControl;
    public Text m_Score;
    public Text m_WinText;
    public Slider m_ForceSlider;

    public GameObject m_ObjetivoPrefab;
    public GameObject m_PlayerPrefab;//esta deberia ser una referencia al prefab, y un jugador manager, para que cunete los que entran y salen, este es una bola
    //este es un prefab jugaddor
    public GameObject m_Player;//esta es la instancia de una bola//este es el jugador, no la pelota
    public Rigidbody m_CanicaPlayer;//instancia de la canica del jugador
    public Collider m_GameZone;
    public Transform m_SpawnPosition;
    private int m_Puntos = 0;
    public Transform[] m_Objetivos;
    //private bool calibrado = false;// solo para controlar  que no se ejeccute siempre la recalibracion
    public void Awake(){
        SetCameraInitial();//mejorar el controlador de la camara, esta cosa esta fllando por alguna razon, quiza usar un intermediaro entre la camara y el avatar, para evitar los problemas, es decir algun objeto que siga a la avatar, y la camara sigue a este
        SetCameraTarget();//quiza esto deberia estar dentro de spawnplayer();
    }

    public void Start(){
        //SpawnPlayer();//no la estoy llamando
        SpawnObjectives();//lo unico que se debe
        NuevoLanzamiento();//verificar que kinect manager este funcionando
    }
    public void SpawnPlayer(){//esta funcion se supone que instancia vanicas
        m_Player = Instantiate(m_PlayerPrefab, m_SpawnPosition.position, m_SpawnPosition.rotation) as GameObject;
        //kinectmanager.ResetAvatarControllers();//esta linea provoca errores, no debo de ejecutarla, pero por alguan razon no esta detectando al avatar
        //no debo hacer esto en start, paree que causa conflicot, quiza deba hacer esta parte de kinect, despues en algun update posterior, de alguna forma, quiza depues de hacer las respectias instancias,
        // es decir despues de todos los start
        //KinectManager kinectmanager = KinectManager.Instance;//creo que esta fallando ahroa, por que o tengo el kinect, sin esa cosa no puedo avanzar
        //print(KinectManager.IsKinectInitialized());//ahora me salen erroes de que no es una instancia, esto podir ser por que no hay tal instancia
        //print(kinectmanager.IsInitialized());//ahora me salen erroes de que no es una instancia, esto podir ser por que no hay tal instancia
        //kinectmanager.Player1Avatars.Add(m_Player.GetComponentInChildren<AvatarController>().gameObject);//por lo visto cualquier acceso provoca estos errores
        //m_Player.GetComponent<PlayerAim>().m_CenterGameZone = m_GameZone.GetComponent<Transform>();//esto es para que gire al rededor de la zona del juego
        //m_Player.GetComponent<PlayerAim>().m_SpawnPoint = m_SpawnPosition;//esto es por si ueiro reinicializar en el punto de inicio,, pero no seria necesario, podira pararle en una funcion
        //m_Player.GetComponent<PlayerThrow>().m_Fuerza = m_ForceSlider;//pasar el slider de fuerza, no sera necesario, en la version con kinect, tal vez, al menos no se llena de la misma forma

        NuevoLanzamiento();//util

        //m_CanicaPlayer = m_Player.GetComponent<AvatarThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();
        /*m_Player.GetComponent<PlayerThrow>().Setup();//quiza al instanciarse, solo deberia geenrar su propia bola
        m_CanicaPlayer = m_Player.GetComponent<PlayerThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();//debieria haber una mejor forma de acceder a esta canica, quiza obtener la referencia a travez de una funcion de playerthrow
        */
        SetCameraTarget();//quiza esto deberia estar dentro de spaenplayer();
        //esto se util para el control de camaras
        //por alguna razon, kinectmanager, no detecta la isntancicion del avatar, probar llamar a la funcion de deteccion de ususario o reset, para hacer la deteccion despues de instanciar esta cosa
        //hacer correcciones de camara, tener cuidado con esta parte, quiza haya que cambiar la forma
    }

    public void SpawnObjectives(){//almacenados
        m_Objetivos = new Transform [m_NumeroCanicas];
        for(int i = 0; i < m_Objetivos.Length; i++){
            GameObject obj = Instantiate(m_ObjetivoPrefab, posicionValida(), Quaternion.identity) as GameObject;//
            m_Objetivos[i] = obj.transform;//no lo estaba haciendo
        }        
    }

    /*private Vector3 posicionValida(){//obetener una posicion valida para las canicas objetivo
        Transform posicion = Instantiate(m_SpawnPosition, new Vector3 (0f, 0.5f, 0f), Quaternion.identity) as Transform;//donde probare las posiciion generada, este es una clon del objeto trasnsform
        //no es aconsejable usar el transform de este gamobject, falla
        posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, radioJuego - 1f));//podira mezclasr la anterior
        posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));//obtener defrente la rotacion
        while(!EsValido(posicion)){
            posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));//podira mezclasr la anterior
            posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));//obtener defrente la rotacion
        }
        return posicion.position;
    }*/

    private Vector3 posicionValida(){
        Vector3 res;
        Transform posicion = Instantiate(m_SpawnPosition, new Vector3 (0f, 0.5f, 0f), Quaternion.identity) as Transform;//donde probare las posiciion generada, este es una clon del objeto trasnsform
        //no es aconsejable usar el transform de este gamebject GameManager, falla
        posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));//podira mezclasr la anterior
        posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));//obtener defrente la rotacion*/
        while(!EsValido(posicion)){
            posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));//podira mezclasr la anterior
            posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));//obtener defrente la rotacion*/
        }
        res = posicion.position;
        Destroy(posicion.gameObject);
        return res;
    }

    private bool EsValido(Transform posicion){
        bool result = true;
        for(int i = 0; i < m_Objetivos.Length; i++){
            if(m_Objetivos[i]){
                result = result && Vector3.Distance(posicion.position, m_Objetivos[i].position) >= 1f;
            }
        }
        return result;
    }
    public void SetCameraTarget(){
        m_CameraControl.m_Player = m_Player;
        m_CameraControl.SetToPlayer();
    }
    public void SetCameraInitial(){
        m_CameraControl.SetStartPosition();
    }

    private void NuevoLanzamiento(){
        m_Player.GetComponent<AvatarThrow>().Setup();
        m_CanicaPlayer = m_Player.GetComponent<AvatarThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();
    }
    void OnTriggerExit(Collider other){
        //cuando todas las canicas se detengan, el turno finalizo
        GameObject m_canica = other.gameObject;
        if(m_canica.layer == LayerMask.NameToLayer("Objetivo")){
            m_Puntos++;//esto esta bien para los objetivos
            Destroy(other.gameObject, 1f);//para que desaparezcan dos segundo despues, ahora el problema es que cuando destruyo una, no la he quitado del array
            SetTextScore();
        }
        if(m_Puntos == m_NumeroCanicas)
            m_WinText.color = Color.white;
    }
    public void SetTextScore(){
        string s = "Puntos: " + m_Puntos + "\nLanzamientos: " + m_LanzamientoNumero;
        m_Score.text = s;
    }
    public void FixedUpdate(){
        //aqui debo verificar que todas las pelotas esten quietas para dar por finalizado el turno
        //tambien deberia comprobar que mi cnica haya sido disparada para incrementar el lnuemro lanzamiento
        bool finalizoLanzamiento = true;
        finalizoLanzamiento = finalizoLanzamiento && (m_CanicaPlayer.IsSleeping() && m_CanicaPlayer.GetComponent<CanicaMove>().lanzado);//di la calinca no se mueve, y ya fue disparada,entoces debe finalizar el alnzamineto
        for(int i = 0; i < m_Objetivos.Length; i++){
            if(m_Objetivos[i]){//este IsSleeping, por que creo que nunca la la velocidad e la poelota entra en el rango minimo que estableci, para la canica funciona bien, pero para los objtivos aprece que no
                finalizoLanzamiento = finalizoLanzamiento && m_Objetivos[i].GetComponent<Rigidbody>().IsSleeping();//si esta quieto, retorna verdadero, si se mueve falso,
            }
        }
        if(finalizoLanzamiento){
            print("Finalizo Lanzamiento");
            Destroy(m_CanicaPlayer.gameObject, 1f);
            NuevoLanzamiento();
            //estas dos lineas siempre van juntas, deberia ponerlas dentro d una funcion
            m_LanzamientoNumero++;
            SetTextScore();
            //como ya finalizo el lanzamiento, toca un cambio de turno, pero or ahora solo le dare una nueva pelota al jugadro
        }
    }
}   