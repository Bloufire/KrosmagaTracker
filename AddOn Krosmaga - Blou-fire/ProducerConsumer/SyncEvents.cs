using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.ProducerConsumer
{
	public class SyncEvents
	{
		public SyncEvents()
		{
			_newItemEvent = new AutoResetEvent(false);
			_exitThreadEvent = new ManualResetEvent(false);
			_eventArray = new WaitHandle[2];
			_eventArray[0] = _newItemEvent;
			_eventArray[1] = _exitThreadEvent;
		}

		public EventWaitHandle ExitThreadEvent
		{
			get { return _exitThreadEvent; }
		}

		public EventWaitHandle NewItemEvent
		{
			get { return _newItemEvent; }
		}

		public WaitHandle[] EventArray
		{
			get { return _eventArray; }
		}

		private EventWaitHandle _newItemEvent;
		private EventWaitHandle _exitThreadEvent;
		private WaitHandle[] _eventArray;
	}
}