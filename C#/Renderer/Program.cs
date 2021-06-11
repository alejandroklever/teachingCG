using GMath;
using Rendering;
using System;
using System.Diagnostics;
using static GMath.Gfx;
using System.IO;
using Renderer.Scene;

namespace Renderer
{
    internal static class Program
    {
        // Scene Setup
        private static readonly int N = pow(2, 7);
        private static readonly float3 Target = float3(5, 5f, -5);
        private static readonly float3 CameraPosition = float3(15, 10f, -15f);
        private static readonly float3 LightPosition = float3(7f, 12f, -7f);
        private static readonly float3 LightIntensity = float3(1f, 1f, 1f) * 750;

        public static void Main(string[] args)
        {
            // Texture to output the image.
            Texture2D texture = new Texture2D(N, N);

            bool UseRT = args.Length == 0 || args[0] == "0";
            if (UseRT)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Algorithms.Raytracing(texture, CameraPosition, Target, LightPosition, LightIntensity);
                stopwatch.Stop();
                texture.Save("test0.rbm");
                Console.WriteLine("Done.\nRendered in " + stopwatch.ElapsedMilliseconds / 1000f + " seconds");
                ShowImageProcess();
            }
            else
            {
                int pass = 0;
                Stopwatch sw = new Stopwatch();
                while (true)
                {
                    Console.WriteLine("Pass: " + pass + $" in {sw.ElapsedMilliseconds / 1000f} seconds");
                    sw.Restart();
                    Algorithms.Pathtracing(texture, pass, CameraPosition, Target, LightPosition, LightIntensity);
                    sw.Stop();
                    texture.Save("test1.rbm");
                    pass++;
                }
            }
        }

        private static void ShowImageProcess()
        {
            var start = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{Path.Combine("..", "..", "..", "image_viewer")} test0.rbm",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using var p = Process.Start(start);
            using var output = p?.StandardOutput;
            Console.Write(output?.ReadToEnd());
        }
    }
}
