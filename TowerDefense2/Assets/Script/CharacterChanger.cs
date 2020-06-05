using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    GameObject[] playersObjects;
    //qTransform[] players;
    Selected[] selectedComponents;

    bool player1,player2,isDead1,isDead2;
    // Start is called before the first frame update
    void Start()
    {
        //stabilisco lunghezza degli array
        playersObjects = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        //riempio con il contenuto
        playersObjects = GameObject.FindGameObjectsWithTag("Player");
        selectedComponents = new Selected[playersObjects.Length];
        for(int i=0;i < playersObjects.Length; i++){
            //Debug.Log(playersObjects[i].GetComponent<Selected>());
            selectedComponents[i] = playersObjects[i].GetComponent<Selected>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //si può cambiare logica di come si cambia personaggio...
        /*if(Input.GetKeyDown(KeyCode.C)) {
            ChangeCharacter(0);
        }
        if(Input.GetKeyDown(KeyCode.V)) {
            ChangeCharacter(1);
        }*/

        //ad ogni update mi basta controllare se una delle due isDead in Selected è stato
        //spuntato e agire di conseguenza
        isDead1 = selectedComponents[0].isDead;
        isDead2 = selectedComponents[1].isDead;
        if(isDead1){
            //vuol dire mi è rimasto solo personaggio 2
            ChangeCharacter(1);
        }else if(isDead2){
            //vuol dire mi è rimasto solo personaggio 1
            ChangeCharacter(0);
        }else{
            //sono ancora entrambi vivi, posso fare cambio di personaggio
            if(Input.GetKeyDown(KeyCode.LeftShift)){
           // if(Input.GetMouseButton(1)){
            ChangeCharacter(3);
            }
        }
    }

    void ChangeCharacter(int index)
    {
        /*
        for(int i=0; i < selectedComponents.Length; i++) {
            if (i == index){
                selectedComponents[i].isActive = true;
            }else{
                selectedComponents[i].isActive = false;
            }

        }*/
        if(index == 0){
            selectedComponents[0].isActive = true;
            selectedComponents[1].isActive = false;
        }else if(index == 1){
            selectedComponents[0].isActive = false;
            selectedComponents[1].isActive = true;
        }else{
            
            //prendo valori correnti di isActive di entrambi
            //funziona ma un po' a scatti, per ora resta così e ci teniamo entrambi...
            player1 = selectedComponents[0].isActive;
            Debug.Log("L - ACTIVE -> " + player1);
            player2 = selectedComponents[1].isActive;
            Debug.Log("R - ACTIVE -> " + player2);
            if(player1){
                //se è attivo il player1 cambio
                selectedComponents[0].isActive = false;
                selectedComponents[1].isActive = true;
            }
            if(player2){
                selectedComponents[0].isActive = true;
                selectedComponents[1].isActive = false;
            }
        }
        
    }

}
