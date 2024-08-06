using Assets.Scripts.Misc.Saving;
using Zenject;

namespace Assets.Scripts.Saving
{
    public class SavingManager : ILateDisposable
    {

        public T GetData<T>() where T : new()
        {
            if (Repository.TryGetData(out T data))
            {
                return data;
            }
            return new T();
        }

        [Inject]
        private void PostInject()
        {
            Repository.LoadState();
        }

        public void LateDispose()
        {
            Repository.SaveState();
        }

        public void SaveData<T>(T data) where T : new()
        {
            Repository.SetData(data);
        }
    }
}
