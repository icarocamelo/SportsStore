using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 2;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Product p)
        {
            if (ModelState.IsValid)
            {
                p = repository.Save(p);
                ViewBag.Message = "Product added successfully!";
                return RedirectToAction("List");
            }

            return View(p);
        }

        public ViewResult List(int page = 1)
        {
            //var result = repository.Products.OrderBy(x => x.ProductID)
            //    .Skip( (page - 1) * PageSize)
            //    .Take(PageSize);

            var result = repository.Products.ToList();
            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = repository.Products
                    .Where(x => x.ProductID == id)
                    .SingleOrDefault();
            
            return View(result);            
        }

        [HttpPost]
        public ActionResult Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                repository.Save(p);

                return RedirectToAction("List");
            }

            ViewBag.Message = "Unexpected error!";

            return View();
        }

        public ActionResult Details(int id)
        {
            var result = repository.Products
                .Where(x => x.ProductID == id)
                .SingleOrDefault();

            return View(result);
        }


        public ActionResult Delete(int id)
        {
            var product = repository.Products
                .Where(x => x.ProductID == id)
                .SingleOrDefault();            

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {                
                var product = repository.Products
                    .Where(x => x.ProductID == id)
                    .SingleOrDefault();

                bool deleted = repository.Delete(product);

                ViewBag.Name = product.Name;
            }
            return RedirectToAction("List");
        }
    }
}
