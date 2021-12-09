namespace InvoiceMakerCore.Annotations.Builders
{
    public abstract class BaseBuilder<T> where T : new()
    {
        protected T _result;

        protected BaseBuilder()
        {
            _result = new T();
        }

        public virtual T Build()
        {
            return _result;
        }

        public virtual void Clear()
        {
            _result = new T();
        }
    }
}