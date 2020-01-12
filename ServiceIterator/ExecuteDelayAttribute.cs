using System;

namespace ServiceIterator
{
    public class ExecuteDelayAttribute : Attribute
    {
        public long Milliseconds { get; set; }
    }
}
