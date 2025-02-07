﻿using GMath;
using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene
{
    public struct PositionNormal : INormalVertex<PositionNormal>
    {
        public float3 Position { get; set; }
        public float3 Normal { get; set; }

        public PositionNormal Add(PositionNormal other)
        {
            return new()
            {
                Position = Position + other.Position,
                Normal = Normal + other.Normal
            };
        }

        public PositionNormal Mul(float s)
        {
            return new()
            {
                Position = Position * s,
                Normal = Normal * s
            };
        }

        public PositionNormal Transform(float4x4 matrix)
        {
            var p = float4(Position, 1);
            p = mul(p, matrix);
                
            var n = float4(Normal, 0);
            n = mul(n, matrix);

            return new PositionNormal
            {
                Position = p.xyz / p.w,
                Normal = n.xyz
            };
        }
    }

}