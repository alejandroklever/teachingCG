using System;
using System.Diagnostics;
using System.IO;

namespace Renderer
{
    public static class Processing
    {
        public static void ShowImageProcess()
        {
            var start = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{Path.Combine("..", "..", "..", "image_viewer")} test.rbm",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using var p = Process.Start(start);
            using var output = p?.StandardOutput;
            Console.Write(output?.ReadToEnd());
        }
    }
}