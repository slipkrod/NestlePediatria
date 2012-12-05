using System;
using System.Collections.Generic;
using System.Linq;
using Rest.Services.Model;
using RestEntities;
using System.IO;
using System.ServiceModel.Web;

namespace Rest.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GeneralService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GeneralService.svc or GeneralService.svc.cs at the Solution Explorer and start debugging.
    public class GeneralService : IGeneralService
    {
        public List<Pais> ListaPaises()
        {
            List<Pais> paises = null;
            var _context = new PruebaEntities();

            foreach (pais pais in _context.pais)
            {
                if (paises == null)
                {
                    paises = new List<Pais>();
                }

                paises.Add(new Pais() { Id = pais.id, Nombre = pais.nombre });
            }

            return paises;
        }

        public List<Estado> ListaEstados(string id)
        {
            var _context = new PruebaEntities();
            int paisId = 0;
            List<Estado> resultado = null;

            Int32.TryParse(id, out paisId);

            if (paisId > 0)
            {
                List<estado> estados = (from e in _context.estado where e.pais_id == paisId select e).ToList();

                foreach (estado estado in estados)
                {
                    if (resultado == null)
                    {
                        resultado = new List<Estado>();
                    }

                    resultado.Add(new Estado() { Id = estado.id, Nombre = estado.nombre, PaisId = estado.pais_id });
                }
            }

            return resultado;
        }

        public List<Ciudad> ListaCiudades(string id)
        {
            var _context = new PruebaEntities();
            int estadoId = 0;
            List<Ciudad> resultado = null;

            Int32.TryParse(id, out estadoId);

            if (estadoId > 0)
            {
                List<ciudad> ciudades = (from c in _context.ciudad where c.estado_id == estadoId select c).ToList();

                foreach (ciudad ciudad in ciudades)
                {
                    if (resultado == null)
                    {
                        resultado = new List<Ciudad>();
                    }

                    resultado.Add(new Ciudad() { Id = ciudad.id, Nombre = ciudad.nombre, EstadoId = ciudad.estado_id });
                }
            }

            return resultado;
        }

        public List<Especialidad> ListaEspecialidades()
        {
            List<Especialidad> especialidades = null;
            var _context = new PruebaEntities();

            foreach (especialidad especialidad in _context.especialidad)
            {
                if (especialidades == null)
                {
                    especialidades = new List<Especialidad>();
                }

                especialidades.Add(new Especialidad() { Id = especialidad.id, Nombre = especialidad.nombre });
            }

            return especialidades;
        }

        public List<SubEspecialidad> ListaSubEspecialidades()
        {
            List<SubEspecialidad> subespecialidades = null;
            var _context = new PruebaEntities();

            foreach (subespecialidad subespecialidad in _context.subespecialidad)
            {
                if (subespecialidades == null)
                {
                    subespecialidades = new List<SubEspecialidad>();
                }

                subespecialidades.Add(new SubEspecialidad() { Id = subespecialidad.id, Nombre = subespecialidad.nombre });
            }

            return subespecialidades;
        }

        public Stream ObtenPdfMealPlanner(string id)
        {
            int mealId = 0;
            Int32.TryParse(id, out mealId);

            var _context = new PruebaEntities();

            if (mealId > 0)
            {
             
                etapa_pais mealEntity = (from a in _context.etapa_pais where a.id == mealId select a).FirstOrDefault();
                if (mealEntity != null)
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    return new MemoryStream(mealEntity.ruta_pdf);
                }
            }

            return null;
        }

        public Stream ObtenImagenProducto(string id)
        {
            int productId = 0;
            Int32.TryParse(id, out productId);

            var _context = new PruebaEntities();

            if (productId > 0)
            {

                producto productoEntity = (from a in _context.producto where a.id == productId select a).FirstOrDefault();
                if (productoEntity != null)
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpg";
                    return new MemoryStream(productoEntity.foto);
                }
            }

            return null;
        }

        public Stream ObtenPdfMaterial(string id)
        {
            int materialId = 0;
            Int32.TryParse(id, out materialId);

            var _context = new PruebaEntities();

            if (materialId > 0)
            {

                material materialEntity = (from a in _context.material where a.id == materialId select a).FirstOrDefault();
                if (materialEntity != null)
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    return new MemoryStream(materialEntity.documento);
                }
            }

            return null;
        }

        public Stream ObtenImagenMaterial(string id)
        {
            int materialId = 0;
            Int32.TryParse(id, out materialId);

            var _context = new PruebaEntities();

            if (materialId > 0)
            {

                material materialEntity = (from a in _context.material where a.id == materialId select a).FirstOrDefault();
                if (materialEntity != null)
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpg";
                    return new MemoryStream(materialEntity.foto);
                }
            }

            return null;
        }

    }
}
