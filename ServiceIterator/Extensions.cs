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
            IEnumerable<IIterable> iterables = applicationBuilder.ApplicationServices.GetServices<IIterable>();
            foreach (IIterable iterable in iterables)
            {
                int time = 1000;
                Attribute[] attributes = Attribute.GetCustomAttributes(iterable.GetType());

                foreach (Attribute attribute in attributes)
                {
                    if (attribute is CallDurationAttribute)
                    {
                        time = ((CallDurationAttribute)attribute).Milliseconds;
                    }
                }

                Timer _timer = new Timer(time); //one hour in milliseconds
                _timer.Elapsed += new ElapsedEventHandler(iterable.Call);
                _timer.Start();
            }
        }
    }
}
