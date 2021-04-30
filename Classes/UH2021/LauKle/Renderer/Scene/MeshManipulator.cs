using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GMath;
using Renderer.Scene;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;


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
        public float weight;
        
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

        public override string ToString()
        {
            return $"Node {(i, j)}";
        }
    }

    public class Graph<N> : IEnumerable<N> where N : INode
    {
        private readonly Dictionary<N, List<N>> _dictionary;

        public Graph()
        {
            _dictionary = new Dictionary<N, List<N>>();
        }

        public List<N> this[N n]
        {
            get => Neighbors(n);
            set => _dictionary[n] = value;
        }

        public List<N> Neighbors(N node) => _dictionary[node];
        
        public void Clear() => _dictionary.Clear();
        
        public IEnumerator<N> GetEnumerator() => _dictionary.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MeshManipulator
    {
        public static Mesh<V> Adjust<V>(int stacks, int slices, Mesh<V> mesh, BezierCurve curve)
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

            var index = 0;
            var n = set.Count;
            var subgraph = InducedSubGraph(graph, set);
            var firstNode = subgraph.First(node => node.i == 0 && node.j == (slices + 1) / 2);
            var secondNode = subgraph[firstNode].First(node => node.j == firstNode.j - 1);

            // this is a trap for starting a path in a non directed circle graph
            subgraph[firstNode] = new List<Node>(new[] {secondNode});

            var currentNode = firstNode;
            while (true)
            {
                Console.WriteLine((currentNode, index / (float) n));
                var source = currentNode.pos;
                currentNode.pos = curve.GetPoint(index++ / (float) n);
                currentNode.direction = normalize(currentNode.pos - source);
                currentNode.Visited = true;

                foreach (var node in graph[firstNode])
                    queue.Enqueue(node);

                if (subgraph[currentNode].All(node => node.Visited))
                    break;

                currentNode = subgraph[currentNode].First(node => !node.Visited);
            }

            var t = .8f;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Visited) continue;
                node = UpdateNodePosition(node, graph, t);
                node.Visited = true;

                foreach (var adj in graph[node].Where(adj => !adj.Visited))
                    queue.Enqueue(adj);

                t /= 2f;
            }

            for (var i = 0; i < stacks + 1; i++)
            for (var j = 0; j < slices + 1; j++)
                mesh.Vertices[j + i * (slices + 1)] = new V {Position = grid[i, j].pos};

            return mesh;
        }

        public static Node UpdateNodePosition(Node node, Graph<Node> graph, float t = 1f)
        {
            var visitedAdjacent = graph[node].Where(n => n.Visited);
            var lastPos = node.pos;
            node.pos += t * visitedAdjacent.Select(n => n.direction)
                .Aggregate(float3.zero, (total, current) => total + current);
            node.direction = normalize(node.pos - lastPos); // dest - src
            return node;
        }

        public static Graph<T> InducedSubGraph<T>(Graph<T> graph, ISet<T> nodes) where T : INode
        {
            var subgraph = new Graph<T>();
            foreach (var node in nodes)
                subgraph[node] = graph[node].Where(nodes.Contains).ToList();
            return subgraph;
        }
    }
}