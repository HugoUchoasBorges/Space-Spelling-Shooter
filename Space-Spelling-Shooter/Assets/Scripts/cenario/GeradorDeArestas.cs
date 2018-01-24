using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeArestas : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Pegando uma referência para a câmera ativa no jogo
        Camera cam = Camera.main;

        //Procurando por um EdgeCollider2D no MESMO GameObject que este script está anexado
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();

        //Pontos da Câmera
        Vector2 bottomLeftCorner = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperLeftCorner = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector2 upperRightCorner = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2 bottomRightCorner = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        //Definindo os pontos do EdgeCollider2D
        collider.points = new Vector2[5]
        {
            bottomLeftCorner,
            upperLeftCorner,
            upperRightCorner,
            bottomRightCorner,
            bottomLeftCorner
        };
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
