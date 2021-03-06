﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mock.Dependency.With.Proxy.Apply.Strategy
{
    internal class DeSerializer
    {
        public static T DeSerialise<T>(byte[] objectToSerialize)
        {
            using (var memoryStream = new MemoryStream(objectToSerialize))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
}
