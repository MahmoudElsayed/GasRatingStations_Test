using GasStationRatingSystem.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GasStationRatingSystem.BLL
{
    public class UploadBll
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadBll(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public bool Save(string path, string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return false;
            }
            byte[] bytes = default;
            try
            {
                bytes = Convert.FromBase64String(base64);
                path = Path.Combine(_webHostEnvironment.WebRootPath, path);
                File.WriteAllBytes(path, bytes);
                return true;

            }
            catch
            {

            }
            return false;
        }
    }
}
