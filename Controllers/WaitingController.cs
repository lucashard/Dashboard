using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class WaitingController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObtainListResult()
        {
            var listaDouble = new List<double>();
            try
            {
                var lista = ReadExcelGoogle("Invoice & NOA Follow up!H3:H14");
                #if DEBUG
                listaDouble.AddRange(lista.Select(elment => elment.ToList().ConvertAll(x => x.ToString().Substring(1))).Select(list => Convert.ToDouble(list[0].Replace(",", "."))));
                #else
                listaDouble.AddRange(lista.Select(elment => elment.ToList().ConvertAll(x => x.ToString().Substring(1))).Select(list => Convert.ToDouble(list[0])));
                #endif
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json(listaDouble, JsonRequestBehavior.AllowGet);
        }

    }
}
