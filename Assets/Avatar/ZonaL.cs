using UnityEngine;

public class ZonaL : MonoBehaviour {
    public void OnTriggerEnter(Collider other){
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("Jugador")){
            print("Lanzar");
            CanicaMove canicaMove = canica.GetComponent<CanicaMove>();
            canicaMove.Lanzar();
        } 
    }
}