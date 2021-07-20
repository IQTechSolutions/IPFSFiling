using System;
using System.IO;
using System.Threading.Tasks;
using Ipfs;
using Ipfs.Engine;

namespace IpfsModule
{
    public class IpfsFilingService
    {
        const string passphrase = "this is not a secure pass phrase";
        private readonly IpfsEngine ipfs;

        /// <summary>
        /// The default Constructor
        /// </summary>
        public IpfsFilingService()
        {
            ipfs = new IpfsEngine(passphrase.ToCharArray());
        }

        /// <summary>
        /// Uploads a file to IPFS
        /// </summary>
        /// <param name="relativePath">The relative path of the file</param>
        /// <returns>a task of <see cref="IFileSystemNode"/></returns>
        public async Task<IFileSystemNode> UploadFile(string relativePath)
        {
            return await ipfs.FileSystem.AddFileAsync(relativePath);
        }

        /// <summary>
        /// Uploads a file to IPFS
        /// </summary>
        /// <param name="stream">A <see cref="Stream"/> of the file</param>
        /// <param name="fileName">The name of the file</param>
        /// <returns>a task of <see cref="IFileSystemNode"/></returns>
        public async Task<IFileSystemNode> UploadFile(Stream stream, string fileName)
        {
            return await ipfs.FileSystem.AddAsync(stream, fileName);
        }

        /// <summary>
        /// Reads the file from IPFS as normal text
        /// </summary>
        /// <param name="cid">The identity of the file on IPFS</param>
        /// <returns>A string containing the file</returns>
        public async Task<string> ReadIpfsTextFile(string cid)
        {
            var pp = await ipfs.FileSystem.ReadAllTextAsync(cid);
            return pp;
        }

        /// <summary>
        /// Reads the file from IPFS as a <see cref="Stream"/>
        /// </summary>
        /// <param name="cid">The identity of the file on IPFS</param>
        /// <returns>A string containing the file</returns>
        public async Task<Stream> ReadIpfsStream(string cid)
        {
            var pp = await ipfs.FileSystem.ReadFileAsync(cid);
            return pp;
        }
        

        #region Helpers

        /// <summary>
        /// Creates a new Directory
        /// </summary>
        /// <param name="path">The relative path of the folder you want to create</param>
        private static string CreateDirectory(string path)
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, path);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        #endregion
    }
}
