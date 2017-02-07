using System.Web.Mvc;
using DashBoard.Models.Login;

namespace DashBoard.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Login());
        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
