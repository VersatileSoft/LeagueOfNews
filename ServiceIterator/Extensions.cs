using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceIterator
{
    public static class Extensions
    {
        public static void UseServicesIterator(this IApplicationBuilder applicationBuilder)
        {
            IEnumerable<IExecutable> iterables = applicationBuilder.ApplicationServices.GetServices<IExecutable>();
            foreach (IExecutable iterable in iterables)
            {
                long time = 1000;
                Attribute[] attributes = Attribute.GetCustomAttributes(iterable.GetType());

                foreach (Attribute attribute in attributes)
                {
                    if (attribute is ExecuteDelayAttribute)
                    {
                        time = ((ExecuteDelayAttribute)attribute).Milliseconds;
                    }
                }

                Timer _timer = new Timer(time);
                _timer.Elapsed += new ElapsedEventHandler(iterable.Execute);
                _timer.Start();
            }
        }
    }
}
