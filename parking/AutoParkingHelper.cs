using System.IO;
using System.Xml.Serialization;

namespace parking
{
    public static class AutoParkingHelper
    {
        private static readonly XmlSerializer Xs = new XmlSerializer(typeof(AutoParkingDto));
        public static void WriteToFile(string fileName, AutoParkingDto data)
        {
            using (var fileStream = File.Create(fileName))
            {
                Xs.Serialize(fileStream, data);
            }
        }

        public static AutoParkingDto LoadFromFile(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                return (AutoParkingDto)Xs.Deserialize(fileStream);
            }
        }
        public static AutoParkingDto LoadFromStream(Stream file)
        {
            return (AutoParkingDto)Xs.Deserialize(file);
        }
    }
}