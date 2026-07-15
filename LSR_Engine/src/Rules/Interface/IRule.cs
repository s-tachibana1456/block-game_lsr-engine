namespace LSR_Engine.src.Rules.Interface
{
    internal readonly struct Empty { }

    internal interface IRule<in T, out R>
    {
        public R Execute(T param);
    }
}
