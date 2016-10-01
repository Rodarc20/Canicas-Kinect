using UnityEngine;
using UnityEngine.UI;

public class CanicaPlayer : MonoBehaviour {
    public bool m_Fired;//si ha sido disparado
    //public bool m_Move;//si se esta moviendo, hago esto para evitar comprobar el rigidbody y usar la funcion IsSleeping()
    //aunq ue deberia llamar a issleeping, por que el problemas es que algun choque que no controlo, puede mover la pelota, pero no afectar a esta variable, lo cual traeria errores
    public Rigidbody m_Rigidbody;
    public Transform m_Player;//este es la posicion del Jugador
    public float m_Desaceleracion = 0f;
    public void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Fired = false;
        //m_Move = false;
    }
    public void Update(){
        if(!m_Fired){
            transform.position = m_Player.position;//con esto las canicas disparadas, no siguen la traslacion del jugador
        }
    }
    public void FixedUpdate(){
        if(m_Fired){
            Vector3 direccion = m_Rigidbody.velocity.normalized;
            if(m_Desaceleracion != 0f){
                m_Rigidbody.AddForce(m_Rigidbody.velocity.normalized * -1 * m_Desaceleracion, ForceMode.Acceleration);//esta desaceleracion funciona
            }
            if((m_Rigidbody.velocity.magnitude <= 0.01f || m_Rigidbody.velocity.normalized == direccion*-1f) && m_Rigidbody.velocity != Vector3.zero){//este evita que entre constante menete a reemplazar por vector zero
                //en este momento el movimiento ya es muy pequeño, puedo cambia r los valores de volocity y angleVelocity a 0, para detener los calculos corespondientes, y en el caso de canica player terminar el turno
                m_Rigidbody.isKinematic = true;//esto deteiene el movimieitno, evita que le afecten fuerzas fisicas
                m_Rigidbody.isKinematic = false;//esto lo vuelve a poner modificable por fuerzas fisicas
                //m_Move = false;
                //m_Rigidbody.velocity = Vector3.zero;
                //m_Rigidbody.angularVelocity = Vector3.zero;
                //print("Velocidad 0");//en las colisiones a veces se queda quieta, provocario un falso sleeping, que haria que se elimine antes, de los debido
                //hay un pequeño error al comienzo del lanzamiento, me deja entrara a esta funcion uan vez antes de que entre en contacto con el piso despues de lanzarla
            }
        }
    }

    public void Fire(Vector3 fuerza){
        if(!m_Fired){
            m_Fired = true;
            //m_Move = true;
            m_Rigidbody.AddForce(fuerza, ForceMode.Impulse);
            //deberia descativar el script PlayerThrow, y activarse denuevo cuando se cree una nuva canica
        }
    }

    /*public void OnTriggerEnter(Collider other){//esto es para mejorar un poco el sistema de colisiones

    }*/
}
//basicamente es para que la canica siga la psicion del jugador cuand este se traslada, tambien podria hacer que controle la fuerza a tra vez de este script, y ya solo player throw, se encarga de transmitir la informacion necesaria