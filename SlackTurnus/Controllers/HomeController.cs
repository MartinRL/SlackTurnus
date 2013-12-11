using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlackTurnus.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View(new OrderedDictionary
			{
				{"Niels H", 0},
				{"Kim", 0},
				{"Michael", 0},
				{"Niels K", 0},
				{"Christian M", 0},
				{"Henrik", 0},
				{"Martin L", 0},
			});
		}

		public ActionResult Accept()
		{
			throw new NotImplementedException();
		}
	}
}
