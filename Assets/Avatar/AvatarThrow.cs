using UnityEngine;

//[RequireComponent(typeof (AvatarController))]
[RequireComponent(typeof (Animator))]
public class AvatarThrow : MonoBehaviour {
    
    public Animator animator;//acceso al cuerpo
    public Transform thumb;
    public Transform hand;//right hand 12 HumanBodyBones.RightHand// 13 KinectWrapper.NuiSkeletonPositionIndex.HandRight
    public Transform shoulder;//rigth shoulder 9 RightShoulder // 10 ShoulderRight
    public Transform hips;
    public Transform neck;
    public Transform posicion;

    public GameObject m_CanicaK;//prefab de la canica que se instancia para lanzar
    public GameObject m_CanicaPlayer;//estaes una instancia referencia a la canica del jugador, este se instacia, aqui
    public CanicaMove m_CanicaMove;
    //velocidad

    public bool lanzar;
    public bool posicionLanzar;
    public void Awake(){//o en awake
        posicion = GetComponent<Transform>();//esto me deberia dar el trnasfor deseado, pero si no debo tratar de encontrarlo
        lanzar = false;
        animator = GetComponent<Animator>();
        thumb = animator.GetBoneTransform(HumanBodyBones.RightThumbProximal);//para que la canica siga a este transform
        hand = animator.GetBoneTransform(HumanBodyBones.RightHand);
        shoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder);
        hips = animator.GetBoneTransform(HumanBodyBones.Hips);
        neck = animator.GetBoneTransform(HumanBodyBones.Neck);
    }
    public void Setup(){
        m_CanicaPlayer = Instantiate(m_CanicaK, hand.position, Quaternion.identity) as GameObject;
        if(m_CanicaPlayer){
            m_CanicaMove = m_CanicaPlayer.GetComponent<CanicaMove>();
            m_CanicaMove.hand = hand;//a lo que va a seguir, a qui tambien falla por que dice que no hay hand, corregir esto(el error)
        }
    }
    public void VerificarPosicionLanzamiento(){
        if(hips.position.y >= 0.5f){
            posicionLanzar = true;
        }
        else{
            posicionLanzar = false;
        }
        Vector3 dirHipsToNeck = neck.position - hips.position;
    }
}