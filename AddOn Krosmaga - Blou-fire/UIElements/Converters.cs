using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AddOn_Krosmaga___Blou_fire.UIElements
{
	public class IntToCardCountStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if ((int)value > 1)
				return "x" + (int)value;
			else
				return string.Empty;
		}

		public object ConvertBack(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			return null;
		}
	}

	public class IntToMatchResultStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if ((int)value == 0)
				return "DEFAITE";
			else
				return "VICTOIRE";
		}

		public object ConvertBack(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			return null;
		}
	}

	public class InverseBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if ((bool)value)
				return Visibility.Collapsed;
			else
				return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
			System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class BooleanToColor : IValueConverter
	{
		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if ((bool)value)
				return "#66000000";
			else
				return "Black";
		}

		public object ConvertBack(object value, Type targetType, object parameter,
			System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class GodClassToColor : IValueConverter
	{
		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			if (value != null)
			{


				JsonCardsParser.Card val = (JsonCardsParser.Card)value;
				if (val.Rarity == 4)
					return "#A5D3E7"; // INFINITE
				else if (val.Rarity == 3)
					return "#6840BD"; // KROSMIQUE
				else
				{
					switch (val.GodType)
					{
						case 1:
							return "#B02511"; // IOP
						case 2:
							return "#474D1A"; // CRA
						case 3:
							return "#C05343"; // ENI
						case 4:
							return "#78128D"; // ECA
						case 5:
							return "#A87E00"; // ENU
						case 6:
							return "#434D67"; // SRAM
						case 7:
							return "#29387E"; // XEL
						case 8:
							return "#382F25"; // SACRI
						case 10:
							return "#848A00"; // SADI
						default:
							return "#734744"; // NEUTRE
					}
				}
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
			System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}