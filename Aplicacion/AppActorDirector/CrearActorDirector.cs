using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Aplicacion.ManejadorError;
using Persistencia;
using Dominio;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;

namespace Aplicacion.AppActorDirector
{
    public class CrearActorDirector
    {

        public class CreateBookParameters : IRequest<ResponseOperations>
        {
            public string Nombre { get; set; }
            public bool EsActor { get; set; }
            public bool EsDirector { get; set; }
            public string urlFoto { get; set; }
            public object Data { get; set; }
        }

        public class Validations : AbstractValidator<CreateBookParameters>
        {

            public Validations()
            {
                RuleFor(x => x.Nombre).NotEmpty();
           
            }
        }
        public class Manejador : IRequestHandler<CreateBookParameters, ResponseOperations>
        {
            private readonly VideoBlockOnlineContext _context;
            public Manejador(VideoBlockOnlineContext context)
            {
                _context = context;
            }

            public async Task<ResponseOperations> Handle(CreateBookParameters request, CancellationToken cancellationToken)
            {



                string FileName = Guid.NewGuid() + ".jpg";
                try
                {
                    if (request.Data != null)
                    {

                        string files = request.Data.ToString().Replace("data:image/png;base64,", String.Empty); ;
                        files = files.Replace("data:image/jpg;base64,", String.Empty);
                        files = files.Replace("data:image/jpeg;base64,", String.Empty);
                        byte[] Contenido = Convert.FromBase64String(files);
                        byteArrayToImage(Contenido, FileName);
                    }
                    else
                    {
                        FileName = "noImage.png";
                    }

                }
                catch {
                    FileName = "noImage.png";
                }

                var ActorDirector = new ActorDirector()
                {
                    Nombre = request.Nombre,
                    EsActor = request.EsActor,
                    EsDirector = request.EsDirector,
                    urlFoto = FileName
                };

                _context.Add(ActorDirector);
                var valor = await _context.SaveChangesAsync();

                if (valor > 0)
                {
                    return new ResponseOperations() { Ok = true, Message = "El actor / director fue guardado", Id = ActorDirector.ActorDirectorID };
                }
                else
                {
                    return new ResponseOperations() { Ok = false, Message = "El actor / director fue guardado", Id = 0 };
                }
            }

            public Image byteArrayToImage(byte[] bytesArr, string archivo)
            {
                using (MemoryStream memstr = new MemoryStream(bytesArr))
                {
                    Image img = Image.FromStream(memstr);


                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/actoresdirectores", archivo);
                    img.Save(path);
            

                    return img;
                }
            }
        }
    }
}
