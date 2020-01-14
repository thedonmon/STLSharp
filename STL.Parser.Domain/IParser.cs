using System;
using System.IO;
using System.Threading.Tasks;

namespace STL.Parser.Domain
{
    public interface IParser<T>
    {
        Task<T> Parse(Stream stream);
    }
}
