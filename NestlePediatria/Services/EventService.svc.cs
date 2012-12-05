using System;
using System.Collections.Generic;
using System.Linq;
using Rest.Services.Model;
using RestEntities;
using System.IO;
using System.ServiceModel.Web;

namespace Rest.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EventService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EventService.svc or EventService.svc.cs at the Solution Explorer and start debugging.
    public class EventService : IEventService
    {
        public List<Evento> ListaEstados(string pais_id)
        {
            var _context = new PruebaEntities();
            int paisId = 0;
            List<Evento> resultado = new List<Evento>();

            Int32.TryParse(pais_id, out paisId);

            if (paisId > 0)
            {
                List<evento> eventos = (from e in _context.evento where e.publicado == 1 select e).ToList();

                foreach (evento evento in eventos)
                {
                    pais pais = (from p in evento.pais where p.id == paisId select p).FirstOrDefault();
                    if (pais != null)
                    {

                        resultado.Add(new Evento()
                        {
                            id = evento.id,
                            nombre = evento.nombre,
                            descripcion = evento.descripcion,
                            lugar = evento.lugar,
                            fechaInicio = evento.fecha_inicio.ToShortDateString(),
                            fechaFin = evento.fecha_fin.ToShortDateString(),
                            dirigido = evento.dirigido,
                        });
                    }
                }
            }

            return resultado;
        }

        public Stream ObtenImagen(string id)
        {
            int eventId = 0;
            Int32.TryParse(id, out eventId);

            var _context = new PruebaEntities();

            if (eventId > 0)
            {

                evento eventoEntity = (from a in _context.evento where a.id == eventId select a).FirstOrDefault();
                if (eventoEntity != null)
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpg";
                    return new MemoryStream(eventoEntity.foto);
                }
            }

            return null;
        }
    }
}
