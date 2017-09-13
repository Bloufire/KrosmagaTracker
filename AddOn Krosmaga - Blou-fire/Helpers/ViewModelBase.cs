using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Helpers
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void OnPropertyChanged<T>(Expression<Func<T>> action)
		{
			var propertyName = GetPropertyName(action);
			RaisePropertyChanged(propertyName);
		}

		public static string GetPropertyName<T>(Expression<Func<T>> action)
		{
			var expression = (MemberExpression) action.Body;
			var propertyName = expression.Member.Name;
			return propertyName;
		}
	}
}