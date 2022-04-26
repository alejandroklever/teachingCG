using Rendering;
using System;
using System.Diagnostics;
using static GMath.Gfx;
using System.IO;
using Renderer.Scene;
using float3 = GMath.float3;

namespace Renderer
{
    internal static class Program
    {
        private static readonly int N = pow(2, 8);
        private static readonly float3 Target = float3(5, 7, -5);
        private static readonly float3 CameraPosition = float3(15, 6, -25);

        private static readonly float3[] LightsPositions =
        {
            float3(15, 20, -13),
        };
        private static readonly float3[] LightsIntensities =
        {
            float3(1f, 1f, 1f) * 3000,
        };
        
        public static void Main(string[] args)
        {
            // Texture to output the image.
            var texture = new Texture2D(N, N);

            Algorithms.CreateScene = Scenes.CreateMeshScene;

            var useRT = args.Length == 0 || args[0] == "0";
            if (useRT)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Algorithms.Raytracing(texture, CameraPosition, Target, LightsPositions, LightsIntensities);
                stopwatch.Stop();
                texture.Save("test0.rbm");
                Console.WriteLine("Done.\nRendered in " + stopwatch.ElapsedMilliseconds / 1000f + " seconds");
                ShowImageProcess();
            }
            else
            {
                var pass = 0;
                var sw = new Stopwatch();
                while (true)
                {
                    Console.WriteLine("Pass: " + pass + $" in {sw.ElapsedMilliseconds / 1000f} seconds");
                    sw.Restart();
                    Algorithms.Pathtracing(texture, pass, CameraPosition, Target, LightsPositions, LightsIntensities);
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
