using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using Rest.Services.Model;
using RestEntities;

namespace Rest.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ArticleService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ArticleService.svc or ArticleService.svc.cs at the Solution Explorer and start debugging.
    public class ArticleService : IArticleService
    {

        public List<Categoria> ListaCategorias(string pais_id)
        {
            var _context = new PruebaEntities();
            int paisId = 0;
            List<Categoria> resultado = new List<Categoria>();

            Int32.TryParse(pais_id, out paisId);

            if (paisId > 0)
            {
                List<categoria> categorias = (from c in _context.categoria orderby c.nombre select c).ToList();

                foreach (categoria categoriaEntity in categorias)
                {
                    Categoria categoria = new Categoria() { id = categoriaEntity.id, Nombre = categoriaEntity.nombre };
                    List<Articulo> listaArticulos = new List<Articulo>();

                    foreach (articulo articuloEntity in categoriaEntity.articulo.OrderByDescending(a => a.fecha))
                    {
                        pais pais = (from p in _context.pais where p.id == paisId select p).FirstOrDefault();
                        if (pais != null)
                        {
                            Articulo articulo = new Articulo() { id = articuloEntity.id, nombre = articuloEntity.nombre, descripcion = articuloEntity.descripcion, autor = articuloEntity.autor };
                            listaArticulos.Add(articulo);
                        }
                    }

                    if (listaArticulos.Count > 0)
                    {
                        categoria.Articulos = listaArticulos;
                        resultado.Add(categoria);
                    }
                }
            }

            return resultado;
        }

        public List<Categoria> ListaArticulos(string pais_id, string cat_id)
        {
            var _context = new PruebaEntities();
            int paisId = 0;
            int categoriaId = 0;
            List<Categoria> resultado = new List<Categoria>();

            Int32.TryParse(pais_id, out paisId);
            Int32.TryParse(cat_id, out categoriaId);

            if (paisId > 0)
            {
                if (categoriaId > 0)
                {
                    categoria categoriaEntity = (from c in _context.categoria where c.id == categoriaId select c).FirstOrDefault();

                    if (categoriaEntity != null)
                    {
                        Categoria categoria = new Categoria() { id = categoriaEntity.id, Nombre = categoriaEntity.nombre };
                        List<Articulo> listaArticulos = new List<Articulo>();

                        foreach (articulo articuloEntity in categoriaEntity.articulo.OrderByDescending(a => a.fecha))
                        {
                            pais pais = (from p in _context.pais where p.id == paisId select p).FirstOrDefault();
                            if (pais != null)
                            {
                                Articulo articulo = new Articulo() { id = articuloEntity.id, nombre = articuloEntity.nombre, descripcion = articuloEntity.descripcion, autor = articuloEntity.autor };
                                listaArticulos.Add(articulo);
                            }
                        }

                        if (listaArticulos.Count > 0)
                        {
                            categoria.Articulos = listaArticulos;
                            resultado.Add(categoria);
                        }
                    }
                }
                else
                {
                    foreach (categoria categoriaEntity in _context.categoria)
                    {
                        Categoria categoria = new Categoria() { id = categoriaEntity.id, Nombre = categoriaEntity.nombre };
                        List<Articulo> listaArticulos = new List<Articulo>();

                        foreach (articulo articuloEntity in categoriaEntity.articulo.OrderByDescending(a => a.fecha))
                        {
                            pais pais = (from p in _context.pais where p.id == paisId select p).FirstOrDefault();
                            if (pais != null)
                            {
                                Articulo articulo = new Articulo() { id = articuloEntity.id, nombre = articuloEntity.nombre, descripcion = articuloEntity.descripcion, autor = articuloEntity.autor };
                                listaArticulos.Add(articulo);
                            }
                        }

                        if (listaArticulos.Count > 0)
                        {
                            categoria.Articulos = listaArticulos;
                            resultado.Add(categoria);
                        }
                    }
                }
            }

            return resultado;
        }

        public List<Annal> ListaAnnales()
        {
            var _context = new PruebaEntities();
            List<Annal> resultado = new List<Annal>();

            List<annales> annales = (from c in _context.annales orderby c.nombre select c).ToList();

            foreach (annales annalEntity in annales)
            {

                Annal articulo = new Annal() { id = annalEntity.id, nombre = annalEntity.nombre, descripcion = annalEntity.descripcion, autor = annalEntity.autor };
                resultado.Add(articulo);
                        
            }

            return resultado;
        }

        public List<Nidito> ListaNidito()
        {
            var _context = new PruebaEntities();
            List<Nidito> resultado = new List<Nidito>();

            List<nidito> niditos = (from c in _context.nidito orderby c.nombre select c).ToList();

            foreach (nidito niditoEntity in niditos)
            {

                Nidito articulo = new Nidito() { id = niditoEntity.id, nombre = niditoEntity.nombre, descripcion = niditoEntity.descripcion, autor = niditoEntity.autor };
                resultado.Add(articulo);

            }

            return resultado;
        }

        public Articulo DetallesArticulo(string idArticulo)
        {
            int id = 0;
            Int32.TryParse(idArticulo, out id);
            var _context = new PruebaEntities();

            if (id > 0)
            {
                articulo articuloEntity = _context.articulo.FirstOrDefault(a=>a.id == id);

                return new Articulo() { autor = articuloEntity.autor, descripcion = articuloEntity.descripcion, nombre = articuloEntity.nombre };
            }

            return null;
        }


        public Nidito DetallesNidito(string idNidito)
        {
            int id = 0;
            Int32.TryParse(idNidito, out id);
            var _context = new PruebaEntities();

            if (id > 0)
            {
                nidito articuloEntity = _context.nidito.FirstOrDefault(n=>n.id==id);

                return new Nidito() { autor = articuloEntity.autor, descripcion = articuloEntity.descripcion, nombre = articuloEntity.nombre };
            }

            return null;
        }

        public Annal DetallesAnnal(string idAnnal)
        {
            int id = 0;
            Int32.TryParse(idAnnal, out id);
            var _context = new PruebaEntities();

            if (id > 0)
            {
                annales articuloEntity = _context.annales.FirstOrDefault(a=>a.id==id);

                return new Annal() { autor = articuloEntity.autor, descripcion = articuloEntity.descripcion, nombre = articuloEntity.nombre };
            }

            return null;
        }

        public Stream ObtenImagen(string list, string articleId)
        {
            int articuloId = 0;
            Int32.TryParse(articleId, out articuloId);

            var _context = new PruebaEntities();

            if (articuloId > 0)
            {

                if (list == "Article")
                {
                    articulo articulo = (from a in _context.articulo where a.id == articuloId select a).FirstOrDefault();
                    if (articulo != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpg";
                        return new MemoryStream(articulo.foto);
                    }
                }
                else if (list == "Annal")
                {
                    annales articulo = (from a in _context.annales where a.id == articuloId select a).FirstOrDefault();
                    if (articulo != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpg";
                        return new MemoryStream(articulo.foto);
                    }
                }
                else if (list == "Nidito")
                {
                    nidito articulo = (from a in _context.nidito where a.id == articuloId select a).FirstOrDefault();
                    if (articulo != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpg";
                        return new MemoryStream(articulo.foto);
                    }
                }
                
            }

            return null;
        }

        public Stream ObtenPdf(string list, string articleId)
        {
            int articuloId = 0;
            Int32.TryParse(articleId, out articuloId);

            var _context = new PruebaEntities();

            if (articuloId > 0)
            {

                if (list == "Article")
                {
                    articulo articulo = (from a in _context.articulo where a.id == articuloId select a).FirstOrDefault();
                    if (articulo != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        return new MemoryStream(articulo.documento);
                    }
                }
                else if (list == "Annal")
                {
                    annales articulo = (from a in _context.annales where a.id == articuloId select a).FirstOrDefault();
                    if (articulo != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        return new MemoryStream(articulo.documento);
                    }
                }
                else if (list == "Nidito")
                {
                    nidito articulo = (from a in _context.nidito where a.id == articuloId select a).FirstOrDefault();
                    if (articulo != null)
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        return new MemoryStream(articulo.documento);
                    }
                }
                
            }

            return null;
        }
    }
}
