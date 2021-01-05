using System;
using System.Collections.Generic;

namespace ExpressionConvert
{
    public class PolymorphContent<T> where T : class
    {
        public readonly ClassProcessor<T> processor;
        public readonly List<Type> inhabitanceOrders;

        public PolymorphContent (ClassProcessor<T> processor)
        {
            this.processor = processor;
            inhabitanceOrders = new List<Type>();
        }
    }
}