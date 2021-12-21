using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IImageRepository
    {
       Task<string> Upload(IFormFile file,string fileName);
    }
}