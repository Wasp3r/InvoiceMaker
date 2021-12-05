using System.IO;

namespace InvoiceMakerTests
{
    public static class TestPathUtils
    {
        private static string _tempPath => $"{Path.GetTempPath()}InvoiceMaker";
        public static string TempPath
        {
            get
            {
                if (!Directory.Exists(_tempPath))
                {
                    Directory.CreateDirectory(_tempPath);
                }
                return _tempPath;
            }
        }
    }
}