using System.Collections;
using System.Collections.Specialized;
using System.Linq;

namespace SlackTurnus.DomainModel
{
	public class SlackTurnus : OrderedDictionary
	{
		public void Next()
		{
			var firstInLineSlacker = this.Cast<DictionaryEntry>().First();

			this.Remove(firstInLineSlacker.Key);

			this.Insert(index: this.Count - unchecked((int)(long)firstInLineSlacker.Value), key: firstInLineSlacker.Key, value: 0);
		}
	}
}
