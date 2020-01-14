using System;
using System.IO;
using System.Text;
using STL.Parser.Domain.Implementations;
using STL.Parser.Models;

namespace STL.Parser.Domain.Factory
{
    public interface IParserFactory<T> where T : IObject
    {
        IParser<T> GetImplementation(Stream stream);
    }
    /// <summary>
    /// Determine if parsing ASCII or Binary file and get the correct implementation
    /// </summary>
    /// <typeparam name="IObject"></typeparam>
    public class ParserFactory<IObject> : IParserFactory<STLObject>
    {
        public IParser<STLObject> GetImplementation(Stream stream)
        {
            if (IsText(stream))
                return new STLASCParser<STLObject>();
            else
                return new STLBinaryParser<STLObject>();
        }

        private bool IsText(Stream stream)
        {
            const string solid = "solid";

            byte[] buffer = new byte[5];
      
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);

            string header = Encoding.ASCII.GetString(buffer);

            return solid.Equals(header, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
