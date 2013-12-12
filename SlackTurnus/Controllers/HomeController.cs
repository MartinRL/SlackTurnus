using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft.Json;
using SlackTurnus.DomainModel;

namespace SlackTurnus.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGetSlackTurnus _getSlackTurnus;
		private readonly IUpdateSlackTurnus _updateSlackTurnus;

		public HomeController(IGetSlackTurnus getSlackTurnus, IUpdateSlackTurnus updateSlackTurnus)
		{
			_getSlackTurnus = getSlackTurnus;
			_updateSlackTurnus = updateSlackTurnus;
		}

		public ActionResult Index()
		{
			return View(_getSlackTurnus.Execute());
		}

		public ActionResult Next()
		{
			var slackTurnus = _getSlackTurnus.Execute();

			var lastIndex = slackTurnus.Count - 1;

			var firstInLineSlacker = slackTurnus.Cast<DictionaryEntry>().Last();

			slackTurnus.RemoveAt(lastIndex);

			slackTurnus.Insert(0, firstInLineSlacker.Key, firstInLineSlacker.Value);

			_updateSlackTurnus.Execute(slackTurnus);

			return RedirectToAction("Index");
		}
	}

	public interface IUpdateSlackTurnus
	{
		void Execute(IOrderedDictionary slackTurnus);
	}

	public class UpdateSlackTurnus : IUpdateSlackTurnus
	{
		public void Execute(IOrderedDictionary slackTurnus)
		{
			// 2do: DI
			using (var streamWriter = new StreamWriter(HostingEnvironment.MapPath("~/DomainModel/slackTurnus.json")))
			{
				streamWriter.Write(JsonConvert.SerializeObject(slackTurnus));
			}
		}
	}
}
