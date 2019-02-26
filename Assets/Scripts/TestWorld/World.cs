﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    public static World instance;

    public Material material;

    public int width;
    public int height;
    public int chunksize = 100;

    public Tile[,] tiles;

    // Use this for initialization
    void Awake()
    {

        instance = this;
    }

    void Start()
    {

        CreateTiles();
        SubdivideTilesArray();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateTiles()
    {

        tiles = new Tile[width, height];
        

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float value = Mathf.PerlinNoise(i / (float)width * 2f, j / (float)height * 2f) * 0.75f +
                    Mathf.PerlinNoise(i / (float)width * 0.5f, j / (float)height * 0.5f) * 0.5f;

                if (value > 0.5f)
                {
                    tiles[i, j] = new Tile(Tile.Type.Grass);
                }
                else
                {
                    tiles[i, j] = new Tile(Tile.Type.Dirt);
                }


            }
        }
    }

    void SubdivideTilesArray(int i1 = 0, int i2 = 0)
    {

        if (i1 > tiles.GetLength(0) && i2 > tiles.GetLength(1))
            return;

        //Get size of segment
        int sizeX, sizeY;

        if (tiles.GetLength(0) - i1 > chunksize)
        {

            sizeX = 100;
        }
        else
            sizeX = tiles.GetLength(0) - i1;

        if (tiles.GetLength(1) - i2 > chunksize)
        {

            sizeY = 100;
        }
        else
            sizeY = tiles.GetLength(1) - i2;

        GenerateMesh(i1, i2, sizeX, sizeY);

        if (tiles.GetLength(0) > i1 + chunksize)
        {
            SubdivideTilesArray(i1 + chunksize, i2);
            return;
        }

        if (tiles.GetLength(1) > i2 + chunksize)
        {
            SubdivideTilesArray(0, i2 + chunksize);
            return;
        }
    }

    void GenerateMesh(int x, int y, int width, int height)
    {

        MeshData data = new MeshData(x, y, width, height);

        GameObject meshGO = new GameObject("CHUNK_" + x + "_" + y);
        meshGO.transform.SetParent(this.transform);

        MeshFilter filter = meshGO.AddComponent<MeshFilter>();
        MeshRenderer render = meshGO.AddComponent<MeshRenderer>();
        render.material = material;

        Mesh mesh = filter.mesh;

        mesh.vertices = data.vertices.ToArray();
        mesh.triangles = data.triangles.ToArray();
        mesh.uv = data.UVs.ToArray();
    }

    public Tile GetTileAt(int x, int y)
    {

        if (x < 0 || x >= width || y < 0 || y >= height)
        {

            return null;
        }

        return tiles[x, y];
    }
}