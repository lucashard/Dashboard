using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class OfClaimsController : BaseController
    {
        //
        // GET: /OfClaims/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtainListResultGoogle()
        {
            //var table = DataTable(7, 7, 3, 14, ReadExcelGoogle("Contract Renewals!G3:G14"));
            var table = ReadExcelGoogle("Invoice & NOA Follow up!G3:G14").ToList();
            var lista = new List<int>();
            foreach (var elm in table)
            {
                foreach (var elemento in elm)
                {
                    if (!elemento.ToString().Equals(" "))
                    {
                        lista.Add(Convert.ToInt32(elemento));
                    }
                }
            }
            var result = new { data = lista };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
