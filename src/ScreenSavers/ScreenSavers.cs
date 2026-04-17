using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daybreak.Common.Features.Authorship;
using Daybreak.Common.Features.ModPanel;
using Terraria.ModLoader;

namespace ScreenSavers
{
	public class ScreenSavers : Mod, IHasCustomAuthorMessage
	{
		public ScreenSavers()
		{
			MusicAutoloadingEnabled = false;
			MusicSkipsVolumeRemap = true;
		}
		
		string IHasCustomAuthorMessage.GetAuthorText()
		{
			return AuthorText.GetAuthorTooltip(this, headerText: null);
		}
	}
}
