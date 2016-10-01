using UnityEngine;

public class CanicaMove : MonoBehaviour{
    public Transform hand;//transforma que sigue, no siempre sera la mano
    [HideInInspector]
    public bool lanzado;// = false;
    private bool inicioLanzamiento;//modificado por el AvatarThrow
    [HideInInspector]
    public bool verificacion;
    private float timerLanzamiento;
    private float tiempoMaximoLanzamiento = 2f;//por ahora parece ser un tiempo prudente 2s
    private Rigidbody m_Rigidbody;
    public float m_Desaceleracion = 0f;//para la friccion
    private Vector3 direccion;
    private float velocidad;
    public float factorVelocidad = 1f;
    //private Vector3 posicionActual;//esta variable no seria necesaria
    private Vector3 posicionAnterior;

    public void Start(){
        //temporalmente, direccion y velocidad seran definidas aqui
        lanzado = false;
        inicioLanzamiento = false;
        posicionAnterior = transform.position;
        direccion = new Vector3 (0.0f, 1.0f, 1.0f);//solo or que tengan algunos vaores iniciales
        direccion.Normalize();
        velocidad = 10f;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.useGravity = false;
    }

    public void FixedUpdate(){
        if(inicioLanzamiento){
            timerLanzamiento += Time.deltaTime;
        }
        if(!lanzado){
            //esto se llama en el fixed por que el avatar tambien se actualiza en el fixed
            transform.position = hand.position;//siguie la mano
            //calculo de velocidad
            direccion = transform.position - posicionAnterior;
            velocidad = direccion.magnitude / Time.deltaTime;
            velocidad *= factorVelocidad;
            direccion.Normalize();
            posicionAnterior = transform.position;//esta linea siempre al final de los calculos
        }
        if(lanzado){//para la desaceleracion
            //esto es para cuadno friccione con el piso
            Vector3 direccionD = m_Rigidbody.velocity.normalized;
            if(m_Desaceleracion != 0f){//falta esta variable, cuano esta en contacto con el piso debo cambiar el valor de esta variable
                m_Rigidbody.AddForce(m_Rigidbody.velocity.normalized * -1 * m_Desaceleracion, ForceMode.Acceleration);//esta desaceleracion funciona
            }
            if((m_Rigidbody.velocity.magnitude <= 0.01f || m_Rigidbody.velocity.normalized == direccionD*-1f) && m_Rigidbody.velocity != Vector3.zero){//este evita que entre constante menete a reemplazar por vector zero
                m_Rigidbody.isKinematic = true;//esto deteiene el movimieitno, evita que le afecten fuerzas fisicas
                m_Rigidbody.isKinematic = false;//esto lo vuelve a poner modificable por fuerzas fisicas
            } 
        }
        if(timerLanzamiento > tiempoMaximoLanzamiento){//se supone que si ha sido lanzado, entonces ya se reincio, otra forma, es que si se a excedido del tiempo, lo reinico, no tengo que preguntar si se lanzo
            inicioLanzamiento = false;
            timerLanzamiento = 0f;
        }
    }

    public void Lanzar(){//llamada por el trigger, tengo que acceder a este script, y luego acceder a esta funcion
        //su contraparte es Fire()
        //esta funcion podria recibir los datos, pero en este caso se calcularan de forma interna
        if(inicioLanzamiento && !lanzado && timerLanzamiento <= tiempoMaximoLanzamiento){//opor ahora el efecto de si detecto el lanzamiento es, si deja de seguir a la mano
            //si el tiempo del desde que toco inicio, es menor que el tiempo maximo entonces se puede lanzar
            lanzado = true;
            m_Rigidbody.useGravity = true;
            m_Rigidbody.AddForce(direccion*velocidad, ForceMode.Impulse);
            print(direccion);
            print(velocidad);
            timerLanzamiento = 0f;
            inicioLanzamiento = false;
        }
    }
    public void IniciarLanzamiento(){
        if(!inicioLanzamiento){
            inicioLanzamiento = true;
            timerLanzamiento = 0f;
        }
    }
}
//la clase esta lista


//cuando la canica toque el trigger apropiado, lanzar la canica
//puedo usar el collider de la canica, o puedo usar el trigger con la funcion onTriggerEnter
//debo cambiar el bool a true y agregar la fuerza al rigidbody adecuad
//combinar este script con el de canica, que aplica la desaceleracion
//
//debido alas imprecisiones de kinect, debo tomar varios putos para calcular la direccion y velocidad, para evitar errores por el kinect
//probablemnte haya que multiplicar la velocidad obtenida por algun otro factor
//
//Incrementar el tamaño del avatar , aver si responde bien, en caso de que no probar con reducir el tamaño de las canicas, esto definitivamente a fectara la dinamica del juego
//tambien debo controlar el rigidbody, antes de ser lanzado no debe estar sujeto a fuerzas, pero una vez que se lance, debo activar el inematic, o el usegravity, y luego lanzar
//por ahora debo verificar que esta cosa lance, para ello debo agregar un escipt al collider, y ponerle un tag a las cosas estas