using System;
using System.Collections.Generic;
using System.Linq;
using GMath;
using Renderer.Scene;
using Rendering;

namespace Renderer.Scene
{
    public interface INode
    {
        bool Visited { get; set; }
    }

    public class Node : INode
    {
        public float3 pos;
        public float3 direction = float3.zero;
        public readonly int j;
        public readonly int i;
        
        public bool Visited { get; set; }
        
        public Node(int _i, int _j, float3 _pos)
        {
            i = _i;
            j = _j;
            pos = _pos;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(i, j);
        }
    }

    public class Graph<N> where N : INode
    {
        private readonly Dictionary<N, List<N>> _dictionary;

        public Graph()
        {
            _dictionary = new Dictionary<N, List<N>>();
        }

        public List<N> this[N n]
        {
            get => _dictionary[n];
            set => _dictionary[n] = value;
        }

        public void Clear() => _dictionary.Clear();
    }

    public class MeshManipulator
    {
        public static float3[] Adjust<V>(int stacks, int slices, Mesh<V> mesh, BezierCurve curve)
            where V : struct, IVertex<V>
        {
            var grid = new Node[stacks + 1, slices + 1];
            var graph = new Graph<Node>();
            var set = new HashSet<Node>();
            var queue = new Queue<Node>();
            
            
            for (var i = 0; i < stacks + 1; i++)
            for (var j = 0; j < slices + 1; j++)
            {
                grid[i, j] = new Node(i, j, mesh.Vertices[j + i * (slices + 1)].Position);
                if (i == 0 || j == 0 || i == stacks || j == slices)
                    set.Add(grid[i, j]);
            }

            bool IsValid(int i, int j) => i <= stacks && j <= slices && i >= 0 && j >= 0;
            
            for (var i = 0; i < stacks + 1; i++)
            for (var j = 0; j < slices + 1; j++)
            {
                var node = grid[i, j];
                graph[node] = new List<Node>();

                if (IsValid(i + 1, j))
                    graph[node].Add(grid[i + 1, j]);
                if (IsValid(i + 1, j + 1))
                    graph[node].Add(grid[i + 1, j + 1]);
                if (IsValid(i, j + 1))
                    graph[node].Add(grid[i, j + 1]);
                if (IsValid(i - 1, j + 1))
                    graph[node].Add(grid[i - 1, j + 1]);
                if (IsValid(i - 1, j))
                    graph[node].Add(grid[i - 1, j]);
                if (IsValid(i - 1, j - 1))
                    graph[node].Add(grid[i - 1, j - 1]);
                if (IsValid(i, j - 1))
                    graph[node].Add(grid[i, j - 1]);
                if (IsValid(i + 1, j - 1))
                    graph[node].Add(grid[i + 1, j - 1]);
            }

            var n = queue.Count;
            var firstNode = queue.First(node => node.i == 0 && node.j == (slices + 1) / 2);
            
            
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                
                if (node.Visited) continue;

                node = UpdateNodePosition(node, graph);
                node.Visited = true;
                
                foreach (var adj in graph[node].Where(adj => !adj.Visited))
                    queue.Enqueue(adj);
            }
            

            return null;
        }

        public static Node UpdateNodePosition(Node node, Graph<Node> graph)
        {
            var visitedAdjacent = graph[node].Where(n => n.Visited);
            var lastPos = node.pos;
            node.pos += visitedAdjacent.Select(n => n.direction)
                .Aggregate(float3.zero, (total, current) => total + current);
            node.direction = node.pos - lastPos;
            return node;
        }
    }
}