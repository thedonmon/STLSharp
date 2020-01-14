using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace STL.Parser.Models
{
    public interface IObject { } //syntax sugar for generic parser factory

    public class STLObject : IObject
    {
        public STLObject()
        {
            Facets = new List<Facet>();
        }
        public IEnumerable<Facet> Facets { get; set; }
        //Sum all vertex areas to get surface area of all facets combined
        public float SurfaceArea => Facets.Sum(x => FindTotalArea(x.Vertices));
        //Sum volumes of each facets 
        public float Volume => Facets.Sum(x => VolumeOfTriangleFacet(x.Vertices));
        //count of facets is number of triangles
        public int TriangleCount => Facets.Count(); 

        private float MaxX => Facets.Max(x => x.Vertices.Max(y => y.X));
        private float MaxY => Facets.Max(x => x.Vertices.Max(y => y.Y));
        private float MaxZ => Facets.Max(x => x.Vertices.Max(y => y.Z));
        private float MinX => Facets.Min(x => x.Vertices.Min(y => y.X));
        private float MinY => Facets.Min(x => x.Vertices.Min(y => y.Y));
        private float MinZ => Facets.Min(x => x.Vertices.Min(y => y.Z));

        public float Length => MaxX - MinX;
        public float Breadth => MaxY - MinY;
        public float Height => MaxZ - MinZ;
        //this doesnt seem to be the correct formula or way to find bounding box
        //plug formula here
        public float[] BoundingBox => new float[]{Length, Breadth, Height};

        private float CalcVertexDistance(Vertex v1, Vertex v2)
        {
            var a = (float)Math.Pow((v1.X - v2.X), 2) + (float)Math.Pow((v1.Y - v2.Y), 2) + (float)Math.Pow((v1.Z - v2.Z), 2);
            var distance = (float)Math.Pow(a,0.5f);
            return distance;
        }
        private float AreaFormula(float a, float b, float c)
        {
            var s = (a + b + c) / 2;
            var area = (float)Math.Pow((s * (s - a) * (s - b) * (s - c)), 0.5);
            return area;
        }
        public float FindTotalArea(Vertex[] vertices)
        {
            var a = CalcVertexDistance(vertices[0], vertices[1]);
            var b = CalcVertexDistance(vertices[1], vertices[2]);
            var c = CalcVertexDistance(vertices[2], vertices[0]);
            var area = AreaFormula(a, b, c);
            return area;
        }
        public float VolumeOfTriangleFacet(Vertex[] vertices)
        {
            //0 based indexing
            var v321 = vertices[2].X * vertices[1].Y * vertices[0].Z;
            var v231 = vertices[1].X * vertices[2].Y * vertices[0].Z;
            var v312 = vertices[2].X * vertices[0].Y * vertices[1].Z;
            var v132 = vertices[0].X * vertices[2].Y * vertices[1].Z;
            var v213 = vertices[1].X * vertices[0].Y * vertices[2].Z;
            var v123 = vertices[0].X * vertices[1].Y * vertices[2].Z;
            return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123); //volume formula
        }
    }
    
}
