using System;
using System.IO;
using UnityEngine;

namespace DataSaver
{
    public abstract class DataSaver<T> where T : DataSaver<T>
    {
        private static ISerializer _serializer = new UnityJsonSerializer();
        
        private readonly string _filename = typeof(T).Name + ".json";
        private static readonly string DocumentsFolderPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + '\\'
                                                                             + GetProjectFolderName() + '\\';
        
        protected DataSaver()
        {
            if (!TryLoadSettings())
                SaveData();
        }        
        
        public void SaveData()
        {
            if (!Directory.Exists(DocumentsFolderPath))
                Directory.CreateDirectory(DocumentsFolderPath);

            File.WriteAllText(DocumentsFolderPath + _filename, _serializer.Serialize(this));
        }

        /// <summary>
        /// Attempts loading data from a file, returns true on success, false on failure
        /// </summary>
        public bool TryLoadSettings()
        {
            if( !File.Exists( DocumentsFolderPath + _filename ) )
            {
                Debug.LogWarning( "File " + DocumentsFolderPath + _filename + " does not exist" );
                return false;
            }

            try
            {
                JsonUtility.FromJsonOverwrite( File.ReadAllText( DocumentsFolderPath + _filename ), this );
            }
            catch ( Exception e )
            {
                Debug.LogWarning( e );
                return false;
            }

            return true;
        }

        private static string GetProjectFolderName()
        {
            var path = Application.dataPath.Split('/');
            return path[path.Length - 2];
        }
    }
}