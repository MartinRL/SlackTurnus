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

			Remove(firstInLineSlacker.Key);

			Insert(index: this.Count - unchecked((int)(long)firstInLineSlacker.Value), key: firstInLineSlacker.Key, value: 0);
		}

		public void Skip()
		{
			var firstInLineSlacker = this.Cast<DictionaryEntry>().First();

			var numberOfSkips = (long)this[0] + 1;
			this[0] = numberOfSkips;

			var index = 0;
			while ((long)this[index] != 0)
			{
				index++;
			}

			Remove(firstInLineSlacker.Key);

			Insert(index: index, key: firstInLineSlacker.Key, value: numberOfSkips);
		}
	}
}
