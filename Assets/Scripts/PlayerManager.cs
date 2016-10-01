using System;
using UnityEngine;

[Serializable]
public class PlayerManager {//similar al script de multiplayer
    public Transform SpawnPosition;
    [HideInInspector] public GameObject m_Player;//esta sera la instancia de un objeto Jugador
    //se enteinde que este ultimo objeto sera reemplazado por el avatar, es decir por el obtejo U_Character_REF
    //en lugar de las referencias a laas canicas prefab, y demas, debo tener accedos, al script avatathrow
    public AvatarThrow m_AvatarThrow;
    public GameObject m_CanicaK;//prefab de la canica que se instancia para lanzar
    public GameObject m_CanicaPlayer;//estaes una instancia referencia a la canica del jugador, este se instacia, aqui
    public CanicaMove m_CanicaMove;
    private bool m_FinLanzamiento = false;
    public int m_Lanzamientos = 0;//solo si cuento lanzamientos
    public int m_ObjetivosObtenidos = 0;//siempre cuento los objetivos obtenidos
    //esto ahora es util por que controlara cuando instanciar canicas para que sigan a la maño del avatar, este tambien deberia crear al avatar, o almenos tener un areferencia de este, y conotrlar tambine los scripts de la canica, y destruirla, tal vez
    public void Setup(){ //para establecer las referencias y valores inicales
        //m_Player = Instantiate(m_PlayerPrefab, m_SpawnPosition.position, m_SpawnPosition.rotation) as GameObject;//esta es para la forma 2//sin embargo este escript no es hernecia de MonoBehavior, por lo tanto no tengo la funcion Instantiate, por eso la forma 2 no funciona
        m_AvatarThrow = m_Player.GetComponent<AvatarThrow>();
        m_FinLanzamiento = false;
        m_Player.SetActive(false);//todos los jugadores deben iniciar inactivos, los cativara y desactivara el gamemanager cuadno seasu turno
    }
    public void NewThrow(){//esta se llamara al inicio de cada turno, al igual quiza que enable control, la camara tambien se debe asiganar a cada jugador correspondiento
        m_FinLanzamiento = false;
        //m_Throw.Setup();//talvez no sea necesario, ademas podria hacer que retorne la referencia al rigidbbody de la canica para quepueda ser util, si la quisiera conservar
        //m_CanicaPlayer = Instantiate(m_CanicaK, transform.position, transform.rotation) as GameObject; //quiza no deba hace que sea herencia de monobehavior
        m_CanicaMove = m_CanicaPlayer.GetComponent<CanicaMove>();
        //deberia llamar a nuevo lanzamiento en nuevo throw
        //el scrpit de AvatarThrow podira ser el que instcncie la canica, asi evito, que este lo haga, ssolo que igual tendra lasreferencias, despues de pdirla, asi se parecera un poco al singleplayer
        //aqui debo pasarle el parametro de a que transform debe seguir, es decir pasarle la mano
    }
    public bool FinalizoLanzamiento(){//esta funcion debe haberse asegurado de haber contado todo, para que desde aqui se desactive el gameobjet jugador(m_Player.SetActive(false)), o hacerlo desde el gamemanager
        if(!m_FinLanzamiento && m_CanicaPlayer != null){//este if no es necesaio, solo erapor el error anterior
            //m_FinLanzamiento =  m_CanicaPlayer.IsSleeping() && m_CanicaPlayer.GetComponent<CanicaPlayer>().m_Fired;//deberia comprobar que plyerthrow teng ifred
            //m_FinLanzamiento =  m_CanicaPlayer.IsSleeping() && m_CanicaPlayer.GetComponent<CanicaPlayer>().m_Fired && m_Throw.m_Throwed;// aun falla parece haber desaparecido el bug

        }
        return m_FinLanzamiento;//no era esto
        //hay un poroblema con esta funcion, por alguna razon se llaa, pero cuadno m_CanicaPlayer no existe, provicando errores
    }
}
    /*private PlayerAim m_Aim;
    private PlayerThrow m_Throw;//esto son para poder habilitar y deshabilitar el control una vez que se realizo un lanzamiento, aun que dberia hacerlo de forma iterna

    
    public void Setup(){
        //creaa canicas de lanzamineto
    }
    public void NewThrow(){
        //PlayerThrow.Setup();
    }
    public void EnableControl(){
        m_Aim.enabled = true;
        m_Throw.enabled = true;

    }
    public void DisableControl(){
        m_Aim.enabled = false;
        m_Throw.enabled = false;
    }*/