using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise {
    int seed;
    float frequency;

    float amplitude;

    float lacunarity;
    float persistance;

    int octaves;

    public Noise (int seed, float frequency, float amplitude, float lacunarity, float persistance, int octaves) {
        this.seed = seed;
        this.frequency = frequency;
        this.amplitude = amplitude;
        this.lacunarity = lacunarity;
        this.persistance = persistance;
        this.octaves = octaves;

        Debug.Log("Seed: "+seed);
    }

    public float[, ] GetNoiseValues (int width, int height) {
        float[, ] noiseValues = new float[width, height];

        float max = 0f;
        float min = float.MaxValue;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                noiseValues[x, y] = 0f;

                float tempA = amplitude;
                float tempF = frequency;

                for (int k = 0; k < octaves; k++) {
                    noiseValues[x, y] += tempA * Mathf.PerlinNoise ((x + seed) / (float) width * tempF, y / (float) height * tempF);
                    tempF *= lacunarity;
                    tempA *= persistance;
                }

                if (noiseValues[x, y] > max)
                {
                    max = noiseValues[x, y];
                }
                if (noiseValues[x, y] < min)
                {
                    min = noiseValues[x, y];
                }
            }
        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                noiseValues[i, j] = Mathf.InverseLerp(max, min, noiseValues[i, j]);
            }
        }

        return noiseValues;
    }
}




