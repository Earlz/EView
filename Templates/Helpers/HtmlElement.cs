using System;
namespace Earlz.EView.Helpers
{
	abstract public class HtmlElement : IEView
	{
		virtual public string EViewRender ()
		{
			throw new NotImplementedException ();
		}
	}
}

