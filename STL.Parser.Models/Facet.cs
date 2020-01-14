using System;
namespace STL.Parser.Models
{
    public struct Facet
    {
        public Vertex Normal { get; set; }
        public Vertex[] Vertices { get; set; }
    }
}
