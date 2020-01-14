using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using STL.Parser.Models;

namespace STL.Parser.Domain.Implementations
{
    /// <summary>
    /// Couldn't finish this class, have to work more with binary parsing in C# to understand how
    /// to effectively read this STL file in binary. Gets close but ultimately results in too many false
    /// positive as it is reading the byte representations of 
    /// </summary>
    /// <typeparam name="IObject"></typeparam>
    public class STLBinaryParser<IObject> : IParser<STLObject>
    {
        public async Task<STLObject> Parse(System.IO.Stream stream)
        {
            var stlObj = new STLObject();

            byte[] buffer;
            var verticies = new List<Vertex>();

            using (var reader = new BinaryReader(stream, Encoding.ASCII, true))
            {
                buffer = reader.ReadBytes(80);
                reader.ReadBytes(4);
             
                while ((reader.BaseStream.Position != reader.BaseStream.Length))
                {
                    const int floatSize = sizeof(float);
                    const int vertexSize = floatSize * 3;
                    const int doubleFloatSize = floatSize * 2;
                    byte[] data = new byte[vertexSize];
                    int bytesRead = reader.Read(data, 0, data.Length);

                    if (bytesRead == 0)
                        break;
                    else if (bytesRead != data.Length)
                        break;
                    var isStringX = System.Text.Encoding.ASCII.GetString(data,0,data.Length);
                    var isStringY = System.Text.Encoding.ASCII.GetString(data, floatSize, data.Length-floatSize);
                    var isStringZ = System.Text.Encoding.ASCII.GetString(data, doubleFloatSize, data.Length- doubleFloatSize);

                    var verti = new Vertex()
                    {
                        X = BitConverter.ToSingle(data, 0),
                        Y = BitConverter.ToSingle(data, floatSize),
                        Z = BitConverter.ToSingle(data, (floatSize * 2))
                    };
                    verticies.Add(verti);

                }
                
            }

            stlObj.Facets = new Facet[] { };
            return stlObj;
        }
    }
}
