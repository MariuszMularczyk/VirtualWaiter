namespace VirtualWaiter.Utils
{
    public class SelectModelBinder : SelectModelBinder<string>
    {

    }
    public class SelectModelBinder<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }
    }
}
