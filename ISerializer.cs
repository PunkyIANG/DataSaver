namespace DataSaver
{
    public interface ISerializer
    {
        string Serialize<T>(T input);

        void DeserializeOverwrite<T>(string input, T target);
    }
}

