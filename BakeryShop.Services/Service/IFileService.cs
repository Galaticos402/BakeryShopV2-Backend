using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Service
{
    public interface IFileService
    {
        bool Upload(IFormFile formFile);
        string GetFileUri(string fileName);
    }
}
