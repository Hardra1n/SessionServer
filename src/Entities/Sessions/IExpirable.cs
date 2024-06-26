namespace Entities.Sessions;

public interface IExpirable<T>
{
    bool IsExpired(T obj);
}