using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ProductController : Controller
    {
        dbDemoEntities db = new dbDemoEntities();

        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtkeyword"];
            List<tProduct> list = null;
            if (string.IsNullOrEmpty(keyword))
                list = db.tProduct.ToList();
            else
                list = db.tProduct.Where(x => x.fName.Contains(keyword)).ToList();

            return View(list);
        }


        //public ActionResult List()
        //{
        //    string keyword = Request.Form["txtkeyword"];
        //    CCustomerFactory f = new CCustomerFactory();
        //    List<CCustomer> list = null;
        //    if (string.IsNullOrEmpty(keyword))
        //        list = f.queryAll();
        //    else
        //        list = f.queryByKeyword(keyword);
        //    return View(list);
        //}

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(tProduct p)
        {
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)  //int? 當參數時才要轉型
        {
            var p=db.tProduct.FirstOrDefault(x => x.fId == id);  //int? 當參數時不用轉型
            if (p != null)
            {
                db.tProduct.Remove(p);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)  
        {
            var p = db.tProduct.FirstOrDefault(x => x.fId == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(tProduct p)
        {
            var temp = db.tProduct.FirstOrDefault(x => x.fId == p.fId);
            temp.fName = p.fName;
            temp.fCost = p.fCost;
            temp.fPrice = p.fPrice;
            temp.fQty = p.fQty;
            
            db.SaveChanges();
            return RedirectToAction("List");
        }

        
    }
}