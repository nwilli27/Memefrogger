using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace FroggerStarter.Serializer
{
    /// <summary>
    ///     Generic Serializer that will handle serialization of objects of a provided type.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize or deserialize</typeparam>
    public static class Serializer<T>
    {
        #region Methods

        /// <summary>
        ///     Writes the object to file.
        ///     Precondition: fileName != null; !fileName.Equals(string.Empty); serializeObject != null
        ///     Post-condition: Generates/Overwrites XML file containing objects information.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="serializeObject">The serialized object.</param>
        /// <exception cref="ArgumentNullException">fileName</exception>
        /// <exception cref="ArgumentException">fileName</exception>
        /// <exception cref="NullReferenceException">fileName</exception>
        public static void WriteObjectToFile(string fileName, T serializeObject)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (fileName.Equals(string.Empty))
            {
                throw new ArgumentException(nameof(fileName));
            }

            if (serializeObject == null)
            {
                throw new ArgumentNullException(nameof(serializeObject));
            }

            
            var outStream = Task.Run(async () =>
                await getOutStream(fileName)).Result;

            var writer = new XmlSerializer(typeof(T));
            using (outStream)
            {
                writer.Serialize(outStream, serializeObject);
            }

            outStream.Dispose();
        }

        private static async Task<Stream> getOutStream(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            var outStream = await file.OpenStreamForWriteAsync();

            return outStream;
        }

        /// <summary>
        ///     Reads the object from file.
        ///     Precondition: fileName != null; !fileName.Equals(string.Empty); file must exist
        ///     Post-condition: none
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     The read object.  If file doesn't exist, return null.
        /// </returns>
        public static T ReadObjectFromFile(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (fileName.Equals(string.Empty))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var inStream = Task.Run(async () =>
                await getInStream(fileName)).Result;

            if (inStream == null)
            {
                return default;
            }

            T readObject;
            var deserializer = new XmlSerializer(typeof(T));
            using (inStream)
            {
                readObject = (T)deserializer.Deserialize(inStream);
            }

            inStream.Dispose();
            return readObject;
        }

        private static async Task<Stream> getInStream(string fileName)
        {
            var stream = await setupReadFile(fileName);

            return stream;
        }

        private static async Task<Stream> setupReadFile(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;
            if (!File.Exists(folder.Path + @"\" + fileName))
            {
                return default;
            }

            var file = await folder.GetFileAsync(fileName);
            var inStream = await file.OpenStreamForReadAsync();

            return inStream;
        }

        #endregion
    }
}