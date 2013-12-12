using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlackTurnus.DomainModel;

namespace SlackTurnus.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGetSlackTurnus _getSlackTurnus;

		public HomeController(IGetSlackTurnus getSlackTurnus)
		{
			_getSlackTurnus = getSlackTurnus;
		}

		public ActionResult Index()
		{
			return View(_getSlackTurnus.Execute());
		}

		public ActionResult Next()
		{
			throw new NotImplementedException();
		}
	}
}
