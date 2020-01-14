using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using STL.Parser.Models;
using System.Linq;
using STL.Parser.Domain.Extensions;

namespace STL.Parser.Domain.Implementations
{
    public class STLASCParser<IObject>: IParser<STLObject>
    {
        public async Task<STLObject> Parse(Stream stream)
        {
            var stlObj = new STLObject();
            //find where facets start
            const string regexSolid = @"solid\s+(?<Name>[^\r\n]+)?";
            var facets = new List<Facet>();

            using (var reader = new StreamReader(stream))
            {
                string header = await reader.ReadLineAsync();
                Match headerMatch = Regex.Match(header, regexSolid);
                if (headerMatch.Success)
                {
                    var body = await reader.ReadToEndAsync();
                    //find all verticies by regex
                    var facetsRegex = @"\s*(vertex)\s+(?<X>[^\s]+)\s+(?<Y>[^\s]+)\s+(?<Z>[^\s]+)";

                    var vertices = new List<Vertex>();
                    
                    foreach (Match item in Regex.Matches(body, facetsRegex))
                    {
                        var vertex = new Vertex
                        {
                            //regex matches are named in each group between the <> above
                            X = float.Parse(item.Groups.FirstOrDefault(x => x.Name == "X").Value),
                            Y = float.Parse(item.Groups.FirstOrDefault(x => x.Name == "Y").Value),
                            Z = float.Parse(item.Groups.FirstOrDefault(x => x.Name == "Z").Value)
                        };
                        vertices.Add(vertex);
                    }

                    //every 3 verticies is a complete facet
                    /*
                     * can use short hand here to hide foreach if needed
                     facets = chunked.Select(x => new Facet { vertices = x.ToArray() }).ToList();
                    */
                    var chunked = vertices.ChunkBy(3);
                    foreach(var vertex in chunked)
                    {
                        var f = new Facet
                        {
                            Vertices = vertex.ToArray()
                        };
                        facets.Add(f);
                    }
                   
                }
            }
            stlObj.Facets = facets;
            return stlObj;
        }
    }
}
