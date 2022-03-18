using UnityEngine;

namespace DataSaver
{
    public class UnityJsonSerializer : ISerializer
    {
        public string Serialize<T>(T input)
        {
            return JsonUtility.ToJson(input, prettyPrint:true);
        }

        public void DeserializeOverwrite<T>(string input, T target)
        {
            JsonUtility.FromJsonOverwrite(input, target);
        }
    }
}