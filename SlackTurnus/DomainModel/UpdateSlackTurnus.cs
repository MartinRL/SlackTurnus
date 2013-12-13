using System.Collections.Specialized;
using System.IO;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace SlackTurnus.DomainModel
{
	public interface IUpdateSlackTurnus
	{
		void Execute(IOrderedDictionary slackTurnus, string turnus);
	}

	public class UpdateSlackTurnus : IUpdateSlackTurnus
	{
		public void Execute(IOrderedDictionary slackTurnus, string turnus)
		{
			// 2do: DI
			using (var streamWriter = new StreamWriter(HostingEnvironment.MapPath("~/DomainModel/" + turnus + ".json")))
			{
				streamWriter.Write(JsonConvert.SerializeObject(slackTurnus));
			}
		}
	}
}
