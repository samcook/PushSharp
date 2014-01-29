using System;
using System.Collections.Generic;
using System.Linq;

namespace PushSharp.Core
{
	public class NotificationQueue
	{
		public NotificationQueue()
		{
			notifications = new LinkedList<INotification>();
			lockObj = new object();
		}

		private readonly object lockObj;
		private readonly LinkedList<INotification> notifications;

		public void Enqueue(INotification notification)
		{
			lock (lockObj)
				notifications.AddLast(notification);
		}

		public void EnqueueAtStart(INotification notification)
		{
			lock (lockObj)
				notifications.AddFirst(notification);
		}

		public INotification Dequeue()
		{
			var n = default(INotification);

			lock (lockObj)
			{
				if (notifications.Count > 0)
				{
					n = notifications.First.Value;
					notifications.RemoveFirst();
				}
			}

			return n;
		}

		public int Count
		{
			get
			{
				lock (lockObj)
					return notifications.Count;
			}
		}
	}
}

