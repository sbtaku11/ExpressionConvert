using System;
using System.Linq.Expressions;

namespace ExpressionConvert
{
    public static class ExpressionConvert<T, U>
    {
        private readonly static Func<T, U> caster;

        static ExpressionConvert ()
        {
            var p = Expression.Parameter(typeof(T));
            var c = Expression.Convert(p, typeof(U));
            caster = Expression.Lambda<Func<T, U>>(c, p).Compile();
        }

        public static U Cast (T val)
        {
            return caster(val);
        }
    }
}