using UnityEngine;

public class Hand : MonoBehaviour{

    public void OnTriggerEnter(Collider other){
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("CanicaJugador")){
            print("Lanzar");
            CanicaMove canicaMove = canica.GetComponent<CanicaMove>();
            canicaMove.Lanzar();
        }  
    }
}