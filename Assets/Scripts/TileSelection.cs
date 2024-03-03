using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileState
{
    Normal,
    Seeded,
    Watered,
    Grown
}

public class Tile : MonoBehaviour
{
    public ActionController actionController;
    public Material groundMat;
    public Material wateredMat;
    public Material seededMat;
    public Material grownMat;
    TileState currentState;
    Renderer rendererTile;
    Collider colliderTile;
    Camera cam;
    bool isSelected;

    GameObject currentPlant;

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
            switch (currentState){
                case TileState.Normal:
                    rendererTile.material.SetColor("_SquareTop", groundMat.GetColor("_SquareTop"));
                    break;
                case TileState.Watered:
                    rendererTile.material.SetColor("_SquareTop", wateredMat.GetColor("_SquareTop"));
                    break;
                case TileState.Seeded:
                    rendererTile.material.SetColor("_SquareTop", seededMat.GetColor("_SquareTop"));
                    break;
                case TileState.Grown:
                    rendererTile.material.SetColor("_SquareTop", groundMat.GetColor("_SquareTop"));
                    break;
                default:
                    break;
            }
            isSelected = false;
        }

        if(isSelected){
            if (Input.GetMouseButtonDown(0)){
                actionController.DoAction(gameObject);
            }
        }
    }

    public TileState GetCurrentState(){
        return currentState;
    }

    public void SetTileState(TileState newState){
        currentState = newState;
        switch (currentState){
            case TileState.Normal:
                rendererTile.material = groundMat;
                break;
            case TileState.Watered:
                rendererTile.material = wateredMat;
                break;
            case TileState.Seeded:
                rendererTile.material = seededMat;
                break;
            case TileState.Grown:
                rendererTile.material = grownMat;
                break;
            default:
                break;
        }
    }
    public void SetCurrentPlant(GameObject newPlant){
        currentPlant = newPlant;
    }

    public GameObject GetCurrentPlant(){
        return currentPlant;
    }

}
