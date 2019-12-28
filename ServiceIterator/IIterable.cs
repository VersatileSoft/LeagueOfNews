using System.Timers;

namespace ServiceIterator
{
    public interface IIterable
    {
        void Call(object source, ElapsedEventArgs e);
    }
}
