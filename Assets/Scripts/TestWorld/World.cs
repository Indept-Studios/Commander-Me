using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public static World instance;

    public Material material;

    public int width;
    public int height;
    public int chunksize = 100;

    public Tile[, ] tiles;

    public int seed;
    public bool randomSeed;
    public float frequency;
    public float amplitude;
    public float lacunarity;
    public float persistance;
    public int octaves;

    public float seaLevel;
    public float beachStartHeight;
    public float beachEndHeight;
    public float dirtStartHeight;
    public float dirtEndHeight;
    public float grassStartHeight;
    public float grassEndHeight;
    public float stoneStartHeight;
    public float stoneEndHeight;

    Noise noise;

    // Use this for initialization
    void Awake () {
        instance = this;

        if (randomSeed == true) {
            seed = Random.Range(-10000, 10000);
        }
        noise = new Noise (seed, frequency, amplitude, lacunarity, persistance, octaves);
    }

    void Start () {

        CreateTiles ();
        SubdivideTilesArray ();
    }

    void CreateTiles () {

        tiles = new Tile[width, height];

        float[, ] noiseValues = noise.GetNoiseValues (width, height);

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                tiles[i, j] = MakeTileAtHeight (noiseValues[i, j]);
                //Debug.Log ("Type: " + tiles[i, j] + " width: " + i + " height: " + j);
            }
        }
    }

    Tile MakeTileAtHeight (float currentHeight) {
        if (currentHeight <= seaLevel)
            return new Tile (Tile.Type.Water);
        if (currentHeight >= beachStartHeight && currentHeight <= beachEndHeight)
            return new Tile (Tile.Type.Sand);
        if (currentHeight >= grassStartHeight && currentHeight <= grassEndHeight)
            return new Tile (Tile.Type.Grass);
        if (currentHeight >= dirtStartHeight && currentHeight <= dirtEndHeight)
            return new Tile (Tile.Type.Dirt);
        if (currentHeight >= stoneStartHeight && currentHeight <= stoneEndHeight)
            return new Tile (Tile.Type.Stone);

        return new Tile (Tile.Type.Void);
    }

    void SubdivideTilesArray (int i1 = 0, int i2 = 0) {

        if (i1 > tiles.GetLength (0) && i2 > tiles.GetLength (1))
            return;

        //Get size of segment
        int sizeX, sizeY;

        if (tiles.GetLength (0) - i1 > chunksize) {

            sizeX = 100;
        } else
            sizeX = tiles.GetLength (0) - i1;

        if (tiles.GetLength (1) - i2 > chunksize) {

            sizeY = 100;
        } else
            sizeY = tiles.GetLength (1) - i2;

        GenerateMesh (i1, i2, sizeX, sizeY);

        if (tiles.GetLength (0) > i1 + chunksize) {
            SubdivideTilesArray (i1 + chunksize, i2);
            return;
        }

        if (tiles.GetLength (1) > i2 + chunksize) {
            SubdivideTilesArray (0, i2 + chunksize);
            return;
        }
    }

    void GenerateMesh (int x, int y, int width, int height) {

        MeshData data = new MeshData (x, y, width, height);

        GameObject meshGO = new GameObject ("CHUNK_" + x + "_" + y);
        meshGO.transform.SetParent (this.transform);

        MeshFilter filter = meshGO.AddComponent<MeshFilter> ();
        MeshRenderer render = meshGO.AddComponent<MeshRenderer> ();
        render.material = material;

        Mesh mesh = filter.mesh;

        mesh.vertices = data.vertices.ToArray ();
        mesh.triangles = data.triangles.ToArray ();
        mesh.uv = data.UVs.ToArray ();
    }

    public Tile GetTileAt (int x, int y) {

        if (x < 0 || x >= width || y < 0 || y >= height) {
            return null;
        }
        return tiles[x, y];
    }
}