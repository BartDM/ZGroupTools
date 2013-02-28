using System.Windows;

namespace WeakEventsTest
{
    public class MyWeakEventManager : WeakEventManager
    {
        public static MyWeakEventManager CurrentManager
        {
            get
            {
                var managerType = typeof(MyWeakEventManager);
                var manager = GetCurrentManager(managerType) as MyWeakEventManager;

                if (manager == null)
                {
                    manager = new MyWeakEventManager();
                    SetCurrentManager(managerType, manager);
                }

                return manager;
            }
        }

        public static void AddListener(Application source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(Application source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        protected override void StartListening(object source)
        {
            ((App)source).MyCustomEvent += DeliverEvent;
        }

        protected override void StopListening(object source)
        {
            ((App)source).MyCustomEvent -= DeliverEvent;
        }
    }
}
