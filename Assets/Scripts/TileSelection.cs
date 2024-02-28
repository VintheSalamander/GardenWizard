using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileState
{
    Normal,
    Watered,
    Seeded,
    Planted
}

public class Tile : MonoBehaviour
{
    
    public Material groundMat;
    public Material wateredMat;
    TileState currentState;
    Renderer rendererTile;
    Collider colliderTile;
    Camera cam;
    bool isSelected;

    // Start is called before the first frame update
    void Awake()
    {
        currentState = TileState.Normal;
        isSelected = false;
        cam = Camera.main;
        rendererTile = GetComponent<Renderer>();
        colliderTile = GetComponent<Collider>();
        
        rendererTile.material = groundMat;
    }

    // Update is called once per frame
    

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (colliderTile.Raycast(ray, out hit, Mathf.Infinity))
        {
            rendererTile.material.SetColor("_SquareTop", Color.white);
            isSelected = true;
        }else{
            switch (currentState)
            {
                case TileState.Normal:
                    rendererTile.material.SetColor("_SquareTop", groundMat.GetColor("_SquareTop"));
                    break;
                case TileState.Watered:
                    rendererTile.material.SetColor("_SquareTop", wateredMat.GetColor("_SquareTop"));
                    break;
                case TileState.Seeded:
                    // Handle Seeded state
                    break;
                case TileState.Planted:
                    // Handle Planted state
                    break;
                default:
                    break;
            }
            isSelected = false;
        }

        if(isSelected){
            if (Input.GetMouseButtonDown(0)){
                rendererTile.material = wateredMat;
                currentState = TileState.Watered;
            }
        }
    }

}
