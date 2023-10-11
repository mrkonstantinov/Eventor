namespace Identity.API.Services
{
    public interface IRegisterService<T>
    {
        Task CreateAsync(T user);
    }
}
