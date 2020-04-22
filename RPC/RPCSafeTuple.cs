using System;

namespace HttpRPC.RPC
{
    public static class RPCSafeTuple
    {
        public static RPCSafeTuple<T1, T2> ConvertValueTuple<T1, T2>(ValueTuple<T1, T2> tuple)
        {
            return new RPCSafeTuple<T1, T2>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3> ConvertValueTuple<T1, T2, T3>(ValueTuple<T1, T2, T3> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4> ConvertValueTuple<T1, T2, T3, T4>(ValueTuple<T1, T2, T3, T4> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4, T5> ConvertValueTuple<T1, T2, T3, T4, T5>(ValueTuple<T1, T2, T3, T4, T5> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4, T5>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4, T5, T6> ConvertValueTuple<T1, T2, T3, T4, T5, T6>(ValueTuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4, T5, T6>(tuple);
        }
        public static RPCSafeTuple<T1, T2, T3, T4, T5, T6, T7> ConvertValueTuple<T1, T2, T3, T4, T5, T6, T7>(ValueTuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            return new RPCSafeTuple<T1, T2, T3, T4, T5, T6, T7>(tuple);
        }
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
