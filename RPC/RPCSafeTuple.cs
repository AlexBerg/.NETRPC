using System;

namespace HttpRPC.RPC
{
    public static class RPCSafeTuple
    {
        public static RPCSafeTuple<T1, T2> ConvertFromValueTuple<T1, T2>(ValueTuple<T1, T2> tuple)
        {
            return new RPCSafeTuple<T1, T2>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3> ConvertFromValueTuple<T1, T2, T3>(ValueTuple<T1, T2, T3> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4> ConvertFromValueTuple<T1, T2, T3, T4>(ValueTuple<T1, T2, T3, T4> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4, T5> ConvertFromValueTuple<T1, T2, T3, T4, T5>(ValueTuple<T1, T2, T3, T4, T5> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4, T5>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4, T5, T6> ConvertFromValueTuple<T1, T2, T3, T4, T5, T6>(ValueTuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4, T5, T6>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4, T5, T6, T7> ConvertFromValueTuple<T1, T2, T3, T4, T5, T6, T7>(ValueTuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4, T5, T6, T7>(tuple);
        }

        public static ValueTuple<T1, T2> ConvertToValueTuple<T1, T2>(RPCSafeTuple<T1, T2> tuple)
        {
            var (i1, i2) = tuple;
            return (i1, i2);
        }
        public static ValueTuple<T1, T2, T3> ConvertToValueTuple<T1, T2, T3>(RPCSafeTuple<T1, T2, T3> tuple)
        {
            var (i1, i2, i3) = tuple;
            return (i1, i2, i3);
        }
        public static ValueTuple<T1, T2, T3, T4> ConvertToValueTuple<T1, T2, T3, T4>(RPCSafeTuple<T1, T2, T3, T4> tuple)
        {
            var (i1, i2, i3, i4) = tuple;
            return (i1, i2, i3, i4);
        }
        public static ValueTuple<T1, T2, T3, T4, T5> ConvertToValueTuple<T1, T2, T3, T4, T5>(RPCSafeTuple<T1, T2, T3, T4, T5> tuple)
        {
            var (i1, i2, i3, i4, i5) = tuple;
            return (i1, i2, i3, i4, i5);
        }
        public static ValueTuple<T1, T2, T3, T4, T5, T6> ConvertToValueTuple<T1, T2, T3, T4, T5, T6>(RPCSafeTuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            var (i1, i2, i3, i4, i5, i6) = tuple;
            return (i1, i2, i3, i4, i5, i6);
        }
        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> ConvertToValueTuple<T1, T2, T3, T4, T5, T6, T7>(RPCSafeTuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            var (i1, i2, i3, i4, i5, i6, i7) = tuple;
            return (i1, i2, i3, i4, i5, i6, i7);
        }


        public static bool IsValueTuple(this object value) =>
            value switch
            {
                ValueTuple<object, object> _                                         => true,
                ValueTuple<object, object, object> _                                 => true,
                ValueTuple<object, object, object, object> _                         => true,
                ValueTuple<object, object, object, object, object> _                 => true,
                ValueTuple<object, object, object, object, object, object> _         => true,
                ValueTuple<object, object, object, object, object, object, object> _ => true,
                null                                                                 => false,
                _                                                                    => false
            };

        public static Type GetCorrectRPCSafeTupleType(this object value) =>
            value switch
            {
                ValueTuple<object, object> _                                         => typeof(RPCSafeTuple<object, object>),
                ValueTuple<object, object, object> _                                 => typeof(RPCSafeTuple<object, object, object>),
                ValueTuple<object, object, object, object> _                         => typeof(RPCSafeTuple<object, object, object, object>),
                ValueTuple<object, object, object, object, object> _                 => typeof(RPCSafeTuple<object, object, object, object, object>),
                ValueTuple<object, object, object, object, object, object> _         => typeof(RPCSafeTuple<object, object, object, object, object, object>),
                ValueTuple<object, object, object, object, object, object, object> _ => typeof(RPCSafeTuple<object, object, object, object, object, object, object>),
                _                                                                    => throw new ArgumentException("Type must be ValueTuple type")
            };
    }

    public class RPCSafeTuple<T1, T2>
    {
        public RPCSafeTuple(ValueTuple<T1, T2> tuple)
        {
            Item1 = tuple.Item1;
            Item2 = tuple.Item2;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public void Deconstruct(out T1 t1, out T2 t2)
        {
            t1 = Item1;
            t2 = Item2;
        }
    }

    public class RPCSafeTuple<T1, T2, T3>
    {
        public RPCSafeTuple(ValueTuple<T1, T2, T3> tuple)
        {
            Item1 = tuple.Item1;
            Item2 = tuple.Item2;
            Item3 = tuple.Item3;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }

        public void Deconstruct(out T1 t1, out T2 t2, out T3 t3)
        {
            t1 = Item1;
            t2 = Item2;
            t3 = Item3;
        }
    }

    public class RPCSafeTuple<T1, T2, T3, T4>
    {
        public RPCSafeTuple(ValueTuple<T1, T2, T3, T4> tuple)
        {
            Item1 = tuple.Item1;
            Item2 = tuple.Item2;
            Item3 = tuple.Item3;
            Item4 = tuple.Item4;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }

        public void Deconstruct(out T1 t1, out T2 t2, out T3 t3, out T4 t4)
        {
            t1 = Item1;
            t2 = Item2;
            t3 = Item3;
            t4 = Item4;
        }
    }

    public class RPCSafeTuple<T1, T2, T3, T4, T5>
    {
        public RPCSafeTuple(ValueTuple<T1, T2, T3, T4, T5> tuple)
        {
            Item1 = tuple.Item1;
            Item2 = tuple.Item2;
            Item3 = tuple.Item3;
            Item4 = tuple.Item4;
            Item5 = tuple.Item5;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }

        public void Deconstruct(out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5)
        {
            t1 = Item1;
            t2 = Item2;
            t3 = Item3;
            t4 = Item4;
            t5 = Item5;
        }
    }

    public class RPCSafeTuple<T1, T2, T3, T4, T5, T6>
    {
        public RPCSafeTuple(ValueTuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            Item1 = tuple.Item1;
            Item2 = tuple.Item2;
            Item3 = tuple.Item3;
            Item4 = tuple.Item4;
            Item5 = tuple.Item5;
            Item6 = tuple.Item6;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }

        public void Deconstruct(out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6)
        {
            t1 = Item1;
            t2 = Item2;
            t3 = Item3;
            t4 = Item4;
            t5 = Item5;
            t6 = Item6;
        }
    }

    public class RPCSafeTuple<T1, T2, T3, T4, T5, T6, T7>
    {
        public RPCSafeTuple(ValueTuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            Item1 = tuple.Item1;
            Item2 = tuple.Item2;
            Item3 = tuple.Item3;
            Item4 = tuple.Item4;
            Item5 = tuple.Item5;
            Item6 = tuple.Item6;
            Item7 = tuple.Item7;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }

        public void Deconstruct(out T1 t1, out T2 t2, out T3 t3, out T4 t4, out T5 t5, out T6 t6, out T7 t7)
        {
            t1 = Item1;
            t2 = Item2;
            t3 = Item3;
            t4 = Item4;
            t5 = Item5;
            t6 = Item6;
            t7 = Item7;
        }
    }
}
