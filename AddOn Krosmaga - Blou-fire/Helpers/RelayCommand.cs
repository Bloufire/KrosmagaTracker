using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AddOn_Krosmaga___Blou_fire.Helpers
{
	public class RelayCommand : ICommand
	{
		private readonly Action actionAExecuter;
		private readonly Action<object> _execute;

		public RelayCommand(Action action)
		{
			actionAExecuter = action;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			if(parameter == null)
			actionAExecuter();
			else
			_execute(parameter);
		}

			#region Fields



		#endregion // Fields

		#region Constructors

		public RelayCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
		}
		#endregion // Constructors

		
	}
}