using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;
using WPFLocalizeExtension.Engine;

namespace AddOn_Krosmaga___Blou_fire.Utility
{
	public static class LocUtil
	{
		private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();

		public static void UpdateCultureInfo()
		{
			var locStr = Language.frFR.ToString();
			if (locStr.Length > 2)
				locStr = locStr.Insert(2, "-");
			LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo(locStr);
		}

		public static string Get(string key, bool upper = false)
		{
			var culture = LocalizeDictionary.Instance.Culture;
			var cacheKey = culture + key;
			if (!Cache.TryGetValue(cacheKey, out var str))
			{
				str = LocalizeDictionary.Instance.GetLocalizedObject("KrosmagaTracker", "Strings", key, culture)?.ToString();
				Cache[cacheKey] = str;
			}
			return str != null && upper ? str.ToUpper(culture) : str;
		}
	}
}
