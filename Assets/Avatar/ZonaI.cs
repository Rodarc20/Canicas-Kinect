using UnityEngine;

public class ZonaI : MonoBehaviour {
    public void OnTriggerEnter(Collider other){//podria ser con Exit tambie, asi no tomaria en cuanta el teimpo que el jugador tiene la mano en posicion inical
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("Jugador")){
            CanicaMove canicaMove = other.GetComponent<CanicaMove>();
            canicaMove.IniciarLanzamiento();
        }
    }
}