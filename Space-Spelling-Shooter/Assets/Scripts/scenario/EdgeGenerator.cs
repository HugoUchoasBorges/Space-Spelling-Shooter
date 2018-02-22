﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeGenerator : MonoBehaviour {

    public static Vector2 bottomLeftCorner;
    public static Vector2 upperLeftCorner;
    public static Vector2 upperRightCorner;
    public static Vector2 bottomRightCorner;

    // Executed before Start
    private void Awake()
    {
        // Main actived camera reference
        Camera cam = Camera.main;

        //Pontos da Câmera
        bottomLeftCorner = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        upperLeftCorner = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        upperRightCorner = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        bottomRightCorner = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
    }

    // Use this for initialization
    void Start () {
        
    }

    void geraCollider()
    {
        // Looking for a EdgeCollider2D in the same GameObject this script is annexed
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();

        // Defining the EdgeCollider2D points
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