using System.Timers;

namespace ServiceIterator
{
    public interface IExecutable
    {
        void Execute(object source, ElapsedEventArgs e);
    }
}
