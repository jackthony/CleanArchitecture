namespace CA_ApplicationLayer.Common.IExternalServices
{
    public interface IESGetAllDataAsync<T>
    {
        Task<IEnumerable<T>> GetDataAsync();
    }
}
