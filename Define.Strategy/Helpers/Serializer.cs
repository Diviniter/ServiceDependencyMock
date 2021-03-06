﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mock.Dependency.With.Proxy.Define.Strategy
{
    internal class Serializer
    {
        public static byte[] Serialise<T>(T objectToSerialize)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, objectToSerialize);
                return ms.ToArray();
            }
        }
    }
}
