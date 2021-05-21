namespace Site.Testing.Common.Helpers
{
    public interface IFunctionsLogger
    {
        void WriteInformation(string info);

        void WriteError(string error);
    }
}