namespace InvoiceMakerCore.Models.Base
{
    public static class DeepCopyExtensions
    {
        public static T DeepCopy<T>(this IDeepCopy<T> source)
            where T : new()
        {
            return source.DeepCopy();
        }

        public static T DeepCopy<T>(this T source)
            where T : BaseModel, new()
        {
            return ((IDeepCopy<T>)source).DeepCopy();
        }
    }
}