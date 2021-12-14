namespace InvoiceMakerCore.Models.Base
{
    public interface IDeepCopy<T>
        where T : new()
    {
        public void CopyTo(T target);

        public T DeepCopy()
        {
            var copy = new T();
            CopyTo(copy);
            return copy;
        }
    }
}