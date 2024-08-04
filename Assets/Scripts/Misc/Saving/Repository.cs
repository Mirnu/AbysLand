using Assets.Scripts.Game;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Misc.Saving
{
    public static class Repository
    {
        private const string GAME_STATE_KEY = "GameState";

        private static Dictionary<string, string> currentState = new();

        public static void LoadState()
        {
            if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                  File.Open(Application.persistentDataPath
                  + "/MySaveData.dat", FileMode.Open);
                currentState = (Dictionary<string, string>)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                currentState = new Dictionary<string, string>();
            }
        }

        public static void SaveState()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
              + "/MySaveData.dat");
            bf.Serialize(file, currentState);
            file.Close();
        }

        public static T GetData<T>(JsonSerializerSettings settings)
        {
            var serializedData = currentState[typeof(T).Name];
            return JsonConvert.DeserializeObject<T>(serializedData, settings);
        }

        public static void SetData<T>(T value, JsonSerializerSettings settings)
        {
            Debug.Log(" ::: " + value.GetType());
            var serializedData = JsonConvert.SerializeObject(value, settings);
            currentState[typeof(T).Name] = serializedData;
        }

        public static bool TryGetData<T>(out T value)
        {
            if (currentState.TryGetValue(typeof(T).Name, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }
    }
}
