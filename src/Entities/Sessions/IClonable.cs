namespace Entities.Sessions;

public interface IClonable<T>
{
    T Clone();
}