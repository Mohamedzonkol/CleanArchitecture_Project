using CleanArchitecture.Infrastructre.Generics.Abstract;

namespace CleanArchitecture.Infrastructre.Abstract
{
    public interface IViewReporesatory<T> : IGenericRepo<T> where T : class
    {
    }
}
