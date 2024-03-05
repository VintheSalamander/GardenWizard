using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileState
{
    Normal,
    Seeded,
    Watered,
    Frozen,
    Evil,
    Grown
}

public class Tile : MonoBehaviour
{
    public Controller controller;
    public Material groundMat;
    public Material wateredMat;
    public Material seededMat;
    public Material grownMat;
    public Material frozenMat;
    public Material evilMat;

    TileState currentState;
    Renderer rendererTile;
    Collider colliderTile;
    Camera cam;
    bool isSelected;
    PlantType correctPlant;
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
        correctPlant = GetRandomPlant();
    }

    // Update is called once per frame
    

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (colliderTile.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(correctPlant);
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
                case TileState.Frozen:
                    rendererTile.material.SetColor("_SquareTop", frozenMat.GetColor("_SquareTop"));
                    break;
                case TileState.Evil:
                    rendererTile.material.SetColor("_SquareTop", evilMat.GetColor("_SquareTop"));
                    break;
                case TileState.Grown:
                    rendererTile.material.SetColor("_SquareTop", grownMat.GetColor("_SquareTop"));
                    break;
                default:
                    break;
            }
            isSelected = false;
        }

        if(isSelected){
            if (Input.GetMouseButtonDown(0)){
                controller.DoAction(this);
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
            case TileState.Frozen:
                rendererTile.material = frozenMat;
                break;
            case TileState.Evil:
                rendererTile.material = evilMat;
                break;
            case TileState.Grown:
                rendererTile.material = grownMat;
                if(currentPlant.GetComponent<Plant>().GetPlantType() == correctPlant){
                    controller.IncrementScore();
                }
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

    public PlantType GetCorrectPlant(){
        return correctPlant;
    }

    PlantType GetRandomPlant(){
        float rand = Random.value;
        if(rand <= 0.1f)
            return PlantType.CherryBlossom;
        if(rand <= 0.4f)
            return PlantType.Tomato;
        return PlantType.Daisy;
    }
}
