using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingController : Controller
    {
        dbDemoEntities db = new dbDemoEntities();
        // GET: Shopping
        public ActionResult List()
        {

            List<tProduct> list = db.tProduct.ToList();
            
            return View(list);
        }

        public ActionResult AddToCart(int? id)
        {
            var p = db.tProduct.FirstOrDefault(x => x.fId == id);
            if (p == null)
                return RedirectToAction("List");
            return View(p);
        }

        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel v)
        {
            tProduct p = db.tProduct.FirstOrDefault(x => x.fId == v.txtFid);
            if(p==null)
                return RedirectToAction("List");
            tShoppingCart item = new tShoppingCart();
            item.fCount = v.txtCount;
            item.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            item.fProductId = v.txtFid;
            item.fPrice = p.fPrice;
            item.fCustomerId = 1;

            db.tShoppingCart.Add(item);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}