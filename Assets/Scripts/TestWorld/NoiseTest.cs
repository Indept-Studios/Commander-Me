using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TestWorld
{
    public class NoiseTest : MonoBehaviour
    {
        private void Start()
        {
            float[] amplitudes = new float[] { 0.3f, 1, 5 };
            float[] frequencies = new float[] { 0.1f, 2 };
            float[] lacunarities = new float[] { 0.2f, 0.9f };
            int[] octaves = new int[] { 2, 5 };
            float[] persistances = new float[] { 0.95f };

            int width = 150;
            int height = 150;
            float[,] noiseValues;


            var sb = new System.Text.StringBuilder();

            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            foreach (float frequency in frequencies)
            {
                foreach (float lacunarity in lacunarities)
                {
                    foreach (float persistance in persistances)
                    {
                        foreach (int octave in octaves)
                        {
                            foreach (float amplitude in amplitudes)
                            {
                                noiseValues = new Noise(100, frequency, amplitude, lacunarity, persistance, octave).GetNoiseValues(width, height);

                                for (int y = 0; y < height; y++)
                                {
                                    for (int x = 0; x < width; x++)
                                    {
                                        if (x != 0)
                                            sb.Append(';');

                                        sb.Append(noiseValues[y, x].ToString(System.Globalization.CultureInfo.InvariantCulture));
                                    }
                                    sb.AppendLine();
                                }

                                System.IO.File.WriteAllText(
                                    System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "! Noises", $"Noise {frequency} {amplitude} {lacunarity} {persistance} {octave} .csv"),
                                    sb.ToString());

                                sb.Clear();
                            }
                        }
                    }
                }
            }
        }
    }
}
