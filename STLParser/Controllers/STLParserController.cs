using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using STL.Parser.Domain.Factory;
using STL.Parser.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STLParser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class STLParserController : ControllerBase
    {
        private readonly IParserFactory<STLObject> _factory;
        public STLParserController(IParserFactory<STLObject> factory)
        {
            _factory = factory;
        }
        [HttpPost]
        public async Task<STLObject> Read([FromBody] sFile file)
        {
            STLObject obj = null;
            using (Stream stream = System.IO.File.OpenRead(file.path))
            {
                var imp = _factory.GetImplementation(stream);
                obj = await imp.Parse(stream);
            }
            return obj;
                
        }
    }
    public class sFile
    {
        public string path { get; set; }
    }
}
