using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class ContractRenewalsController : BaseController
    {
        //
        // GET: /ContractsRenew/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtainListResultGoogle()
        {
            //var table = DataTable(7, 7, 3, 14, ReadExcelGoogle("Contract Renewals!G3:G14"));
            var table = ReadExcelGoogle("Contract Renewals!C44:N44").ToList();
            var lista = new List<int>();
            foreach (var elm in table)
            {
                foreach (var elemento in elm)
                {
                    if (Convert.ToInt32(elemento) != 0)
                    {
                        lista.Add(Convert.ToInt32(elemento));
                    }
                }
            }

            var table1 = ReadExcelGoogle("Contract Renewals!C23:N23").ToList();
            var lista2 = new List<int>();
            foreach (var elm in table1)
            {
                foreach (var elemento in elm)
                {
                    if (Convert.ToInt32(elemento) != 0)
                    {
                        lista2.Add(Convert.ToInt32(elemento));
                    }
                }
            }

            var result = new { data = lista,pending=lista2 };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
