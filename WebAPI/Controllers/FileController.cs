﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/file")]
    [ApiController]

    public class FileController : ControllerBase
    {
        

        VideoBlockOnlineContext _booksOnlineContext;
        public FileController(VideoBlockOnlineContext booksOnlineContext)
        {
            _booksOnlineContext = booksOnlineContext;
        }
  

        [HttpGet("download/{url}/{tipo}")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(string url, string tipo)
        {
            char ch = (char)34;
            url = url.Replace(ch.ToString(), "");
           
            string imagenDefault = "noImage.png";

            string imagen=tipo+"/"+ url;



           
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", imagen);
            if (!System.IO.File.Exists(path))
            {
                path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot", imagenDefault);
            }
            
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }





        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats" +
                "officedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}