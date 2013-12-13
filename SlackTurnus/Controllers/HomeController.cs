using System.Collections;
using System.Linq;
using System.Web.Mvc;
using SlackTurnus.DomainModel;

namespace SlackTurnus.Controllers
{
	public class HomeController : Controller
	{
		private const string PRIMARY_SLACKER_TURNUS = "primarySlackerTurnus";
		private readonly IGetSlackTurnus _getSlackTurnus;
		private readonly IUpdateSlackTurnus _updateSlackTurnus;

		public HomeController(IGetSlackTurnus getSlackTurnus, IUpdateSlackTurnus updateSlackTurnus)
		{
			_getSlackTurnus = getSlackTurnus;
			_updateSlackTurnus = updateSlackTurnus;
		}

		public ActionResult Index()
		{
			return View(_getSlackTurnus.Execute(PRIMARY_SLACKER_TURNUS).Cast<DictionaryEntry>().Reverse());
		}

		public ActionResult Next()
		{
			var slackTurnus = _getSlackTurnus.Execute(PRIMARY_SLACKER_TURNUS);

			slackTurnus.Next();

			_updateSlackTurnus.Execute(slackTurnus, PRIMARY_SLACKER_TURNUS);

			return RedirectToAction("Index");
		}

		public ActionResult Skip()
		{
			var slackTurnus = _getSlackTurnus.Execute(PRIMARY_SLACKER_TURNUS);

			slackTurnus.Skip();

			_updateSlackTurnus.Execute(slackTurnus, PRIMARY_SLACKER_TURNUS);

			return RedirectToAction("Index");
		}
	}
}
