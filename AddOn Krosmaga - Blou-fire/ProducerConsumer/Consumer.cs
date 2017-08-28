using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.ProducerConsumer
{
    public class Consumer
    {
        private Queue<byte[]> _queue;
        private SyncEvents _syncEvents;

        public Consumer(Queue<byte[]> q, SyncEvents e)
        {
            _queue = q;
            _syncEvents = e;
        }
        // Consumer.ThreadRun
        public void ThreadRun()
        {
            byte[] data;
            while (WaitHandle.WaitAny(_syncEvents.EventArray) != 1)
            {
                lock (((ICollection)_queue).SyncRoot)
                {
                    data = _queue.Dequeue();
                }

            }
        }
    }
}
