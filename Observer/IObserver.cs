using Tareasv2;

namespace Tareasv2.Observer
{
    public interface IObserver
    {
        void Update(Tarea tarea);
        void Create(Tarea tarea);
    }
}
