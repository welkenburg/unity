﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endlessTerrain : MonoBehaviour
{
    public const float maxViewOst = 300;
    public Transform viewer;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunksVisibleInViewOst;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();

    void start(){
    	chunkSize = MapGenerator.mapChunkSize - 1;
    	chunksVisibleInViewOst = Mathf.RoundToInt(maxViewOst / chunkSize);

    }

    void Update(){
    	viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
    	UpdateVisibleChunks();
    }

    void UpdateVisibleChunks(){
    	int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
    	int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

    	for(int yOffset = -chunksVisibleInViewOst; yOffset <= chunksVisibleInViewOst; yOffset++){
    		for(int xOffset = -chunksVisibleInViewOst; xOffset <= chunksVisibleInViewOst; xOffset++){
    			Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset,currentChunkCoordX + xOffset);

    			if (terrainChunkDictionary.ContainsKey(viewedChunkCoord)){
    				terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
    			}
    			else{
    				terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord,chunkSize));
    			}
    		}
    	}
    }

    public class TerrainChunk{

    	GameObject meshObject;
    	Vector2 position;
    	Bounds bounds;

    	public TerrainChunk(Vector2 coord, int size){
    		position = coord * size;
    		bounds = new Bounds(position, Vector2.one * size);
    		Vector3 positionV3 = new Vector3(position.x,0,position.y);

    		meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
    		meshObject.transform.position = positionV3;
    		meshObject.transform.localScale = Vector3.one * size/10f;
    		setVisible(false);
    	}

    	public void UpdateTerrainChunk(){
    		float viewerDistanceFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
    		bool visible = viewerDistanceFromNearestEdge <= maxViewOst;
    		setVisible(visible);
    	}

    	public void setVisible(bool visible){
    		meshObject.SetActive(visible);
    	}
    }
}
//script non fonctionel
