 namespace Antidote
{
    public interface IProcessor
    {
         void Process(string filename);
         bool IsValid(string filename);
    }
}