using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft.Json;

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

		public ActionResult Accept()
		{
			throw new NotImplementedException();
		}
	}

	public interface IGetSlackTurnus
	{
		IOrderedDictionary Execute();
	}

	public class GetSlackTurnus : IGetSlackTurnus
	{
		public IOrderedDictionary Execute()
		{
			string slackersAsJson;

			// 2do: DI http://stackoverflow.com/questions/6390608/structuremap-is-not-disposing-data-context-when-using-httpcontextscoped
			using (var streamReader = new StreamReader(HostingEnvironment.MapPath("~/DomainModel/slackTurnus.json")))
			{
				slackersAsJson = streamReader.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<OrderedDictionary>(slackersAsJson);
		}
	}
}
