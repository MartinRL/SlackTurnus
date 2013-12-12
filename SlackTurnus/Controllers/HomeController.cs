using System.Collections;
using System.Linq;
using System.Web.Mvc;
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
			return View(_getSlackTurnus.Execute().Cast<DictionaryEntry>().Reverse());
		}

		public ActionResult Next()
		{
			var slackTurnus = _getSlackTurnus.Execute();

			var firstInLineSlacker = slackTurnus.Cast<DictionaryEntry>().First();

			slackTurnus.Remove(firstInLineSlacker.Key);

			slackTurnus.Insert(index: slackTurnus.Count - unchecked((int)(long)firstInLineSlacker.Value), key: firstInLineSlacker.Key, value: 0);

			_updateSlackTurnus.Execute(slackTurnus);

			return RedirectToAction("Index");
		}

		public ActionResult Skip()
		{
			var slackTurnus = _getSlackTurnus.Execute();

			var slackTurnusAsDictionaryEntries = slackTurnus.Cast<DictionaryEntry>().ToList();

			var firstInLineSlacker = slackTurnusAsDictionaryEntries.First();

			firstInLineSlacker.Value = (long)firstInLineSlacker.Value + 1;


//			var slackerOfIndex = slackTurnusAsDictionaryEntries.Reverse().First(slacker => (long)slacker.Value == 0).Key;
			/*int index;

			if (slackTurnusAsDictionaryEntries.Any(slacker => (long)slacker.Value > 0))
			{
				index = slackTurnusAsDictionaryEntries.ToList().FindIndex(slacker => (long) slacker.Value > 0) - 1;
			}
			else
			{
				index = slackTurnus.Count - 1;
			}
*/
			var index = slackTurnusAsDictionaryEntries.FindIndex(slacker => (long)slacker.Value == 0);

			slackTurnus.Remove(firstInLineSlacker.Key);
			
			slackTurnus.Insert(index, firstInLineSlacker.Key, firstInLineSlacker.Value);

			_updateSlackTurnus.Execute(slackTurnus);

			return RedirectToAction("Index");
		}
	}
}
