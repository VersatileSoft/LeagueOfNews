using System;

namespace ServiceIterator
{
    public class CallDurationAttribute : Attribute
    {
        public int Milliseconds { get; set; }
    }
}
