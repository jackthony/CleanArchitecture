

namespace CA_ApplicationLayer
{
    public interface IExternalServiceAdapter<T>
    {
        Task<IEnumerable<T>> GetDataAsync();
    }
}
