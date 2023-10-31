using System.Threading;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class DropdownTestWithDelay : DropdownTest
	{
		public DropdownTestWithDelay() : base()
		{
		}

		public override async Task LoadContent()
		{
			await Task.Delay(5000);

			await base.LoadContent();
		}
	}
}
