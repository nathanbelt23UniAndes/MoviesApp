using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.AppPeliculaAlquiler
{
    public class RecibidaPeliculaAlquiler
    {
        public class RecibidaPeliculaAlquilerParametros : IRequest<ResponseOperations>
        {
            public int Id { get; set; }
        }

        public class Validations : AbstractValidator<RecibidaPeliculaAlquilerParametros>
        {

            public Validations()
            {
                RuleFor(x => x.Id).NotEmpty();


            }
        }
        public class Manejador : IRequestHandler<RecibidaPeliculaAlquilerParametros, ResponseOperations>
        {
            private readonly VideoBlockOnlineContext _context;
            public Manejador(VideoBlockOnlineContext context)
            {
                _context = context;
            }

            public async Task<ResponseOperations> Handle(RecibidaPeliculaAlquilerParametros request, CancellationToken cancellationToken)
            {
                var alquiler = await _context.PeliculaAlquiler.Where(p => p.PeliculaAlquilerID == request.Id).FirstOrDefaultAsync();
                if (alquiler == null)
                {
                    return new ResponseOperations() { Ok = false, Message = "Ups esta reserva no existe", Id = 0 };
                }
                alquiler.EstadoAquilerID = 2;
                alquiler.FechaEntrega = DateTime.Now;
              
                var pelicula = await _context.Pelicula.Where(p => p.PeliculaID ==alquiler.PeliculaID).FirstOrDefaultAsync();
                pelicula.Disponible = true;
                var valor = await _context.SaveChangesAsync();


                if (valor > 0)
                {
                    return new ResponseOperations() { Ok = true, Message = "La pelicula ha sido recibida", Id = alquiler.PeliculaAlquilerID };
                }
                else
                {
                    return new ResponseOperations() { Ok = false, Message = "La pelicula ha sido recibida no se puede guardar", Id = 0 };
                }

            }
        }
    }
}
