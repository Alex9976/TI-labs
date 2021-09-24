using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TILab4
{
    class LFSR
    {
        public enum AlgorithmMode { Encrypt, Decrypt };
        static private ulong _registerValue;
        private ulong _initValue;
        private static LFSR _instance = null;
        public string PathToFile { get; set; }
        private const int BUFFER_LENGTH = 8192;
        public int Progress { get; set; }
        public bool isReady;
        public static object _look = new object();

        private LFSR() { }

        public static LFSR getInstance()
        {
            if (_instance == null)
                _instance = new LFSR();
            return _instance;
        }

        public void setInitValue(ulong initValue)
        {
            _initValue = initValue;
        }

        private void ResetRegister()
        {
            _registerValue = _initValue;
        }

        public void Start(AlgorithmMode mode)
        {
            ResetRegister();
            byte[] buffer = new byte[BUFFER_LENGTH];
            int readSize, facticalReadSize = 0;
            isReady = false;
            string newPath;
            try
            {
                var fileSize = new FileInfo(PathToFile).Length;
                BinaryReader reader;
                BinaryWriter writer;
                if (mode == AlgorithmMode.Encrypt)
                {
                    reader = new BinaryReader(File.Open(PathToFile, FileMode.Open));
                    writer = new BinaryWriter(File.Open(PathToFile + ".arc", FileMode.Create));
                }
                else
                {
                    reader = new BinaryReader(File.Open(PathToFile, FileMode.Open));
                    if (PathToFile.Contains(".arc"))
                    {
                        newPath = PathToFile.Substring(0, PathToFile.IndexOf(".arc"));
                    }
                    else
                    {
                        newPath = PathToFile;
                    }
                    writer = new BinaryWriter(File.Open(newPath, FileMode.Create));
                }
                
                do
                {
                    readSize = reader.Read(buffer, 0, BUFFER_LENGTH);
                    for (int i = 0; i < BUFFER_LENGTH; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            buffer[i] = (byte)(buffer[i] ^ (getValue() << (8 - j)));
                        }
                    }
                    writer.Write(buffer, 0, readSize);
                    facticalReadSize += readSize;
                    lock (_look)
                    {
                        Progress = (int)((float)facticalReadSize / fileSize * 100);
                    }
                } while (readSize > 0);
                reader.Close();
                writer.Close();
                MessageBox.Show("Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            isReady = true;
            Progress = 0;
        }

        private ulong getValue()
        {
            // Polynomial: x^30 + x^16 + x^15 + x + 1
            _registerValue = ((((_registerValue >> 0) ^ (_registerValue >> 14) ^ (_registerValue >> 15) ^ (_registerValue >> 29)) & 1) << 31) | (_registerValue >> 1);
            return _registerValue & 1;
        }
    }
}
