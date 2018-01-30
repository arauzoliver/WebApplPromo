using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplPromo.Models;

namespace WebApplPromo.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            ProductosContexts db = new ProductosContexts();

            //List<articulo> lista = db.articulo.Where(a => a.cant < 10).ToList();

            return View(db.articulo.ToList());
        }

        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(articulo a)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (ProductosContexts db = new ProductosContexts())
                {
                    db.articulo.Add(a);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar", ex);
                return View();
            }


        }
        
        public ActionResult Editar(int id)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new ProductosContexts())
                {
                    //articulo ar = db.articulo.Where(a => a.codigo == id).FirstOrDefault();
                    articulo art = db.articulo.Find(id);
                    //db.SaveChanges();

                    return View(art);
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(articulo a)
        {
            try
            {
                using (var db = new ProductosContexts())
                {
                    articulo art = db.articulo.Find(a.codigo);
                    art.cant = a.cant;
                    art.nombre = a.nombre;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult DetallesArticulo(int id)
        {
            using (var db = new ProductosContexts())
            {
                articulo art = db.articulo.Find(id);
                return View(art);
            }
        }

        public ActionResult Borrar(int id)
        {
            using (var db = new ProductosContexts())
            {
                articulo art = db.articulo.Find(id);
                db.articulo.Remove(art);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /*
        public ActionResult Borrar(int id)
        {
            using (var db = new ProductosContexts())
            {
                articulo art = db.articulo.Find(id);
                db.articulo.Remove(art);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        */

    }
}