using System.IO;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace SlackTurnus.DomainModel
{
	public interface IGetSlackTurnus
	{
		SlackTurnus Execute(string turnus);
	}

	public class GetSlackTurnus : IGetSlackTurnus
	{
		public SlackTurnus Execute(string turnus)
		{
			string slackersAsJson;

			// 2do: DI http://stackoverflow.com/questions/6390608/structuremap-is-not-disposing-data-context-when-using-httpcontextscoped
			using (var streamReader = new StreamReader(HostingEnvironment.MapPath("~/DomainModel/" + turnus + ".json")))
			{
				slackersAsJson = streamReader.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<SlackTurnus>(slackersAsJson);
		}
	}
}
