using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace launcherdotnet
{
    public class DummyFile
    {
        // bros not following 80 character limit
        public static string hex = "4D\r\n5A\r\n6B\r\n65\r\n72\r\n6E\r\n65\r\n6C\r\n33\r\n32\r\n00\r\n00\r\n50\r\n45\r\n00\r\n00\r\n4C\r\n01\r\n03\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n78\r\n00\r\n0F\r\n03\r\n0B\r\n01\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n14\r\n31\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n0C\r\n00\r\n00\r\n00\r\n00\r\n00\r\n40\r\n00\r\n00\r\n10\r\n00\r\n00\r\n00\r\n02\r\n00\r\n00\r\n04\r\n00\r\n00\r\n00\r\n01\r\n00\r\n00\r\n00\r\n04\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\nA6\r\n31\r\n00\r\n00\r\n14\r\n01\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n03\r\n00\r\n00\r\n00\r\n00\r\n00\r\n10\r\n00\r\n00\r\n10\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n02\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n7E\r\n31\r\n00\r\n00\r\n4B\r\n00\r\n00\r\n00\r\n00\r\n00\r\n47\r\n65\r\n74\r\n53\r\n74\r\n64\r\n48\r\n61\r\n6E\r\n64\r\n6C\r\n65\r\n00\r\n00\r\n01\r\n00\r\n00\r\n00\r\n00\r\n10\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n40\r\n00\r\n30\r\nC0\r\n78\r\n78\r\n78\r\n78\r\n78\r\n78\r\n78\r\n00\r\n01\r\n00\r\n00\r\n00\r\n00\r\n20\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n40\r\n00\r\n30\r\nC0\r\n78\r\n78\r\n78\r\n78\r\n78\r\n78\r\n78\r\n00\r\nA6\r\n01\r\n00\r\n00\r\n00\r\n30\r\n00\r\n00\r\n90\r\n01\r\n00\r\n00\r\n02\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n20\r\n00\r\n30\r\nE0\r\n6A\r\nF5\r\nFF\r\n15\r\n5B\r\n31\r\n40\r\n00\r\n50\r\n89\r\nE1\r\n6A\r\n00\r\n51\r\n6A\r\n0F\r\n68\r\n38\r\n31\r\n40\r\n00\r\n50\r\nFF\r\n15\r\n5F\r\n31\r\n40\r\n00\r\n6A\r\n00\r\nFF\r\n15\r\n57\r\n31\r\n40\r\n00\r\n48\r\n65\r\n6C\r\n6C\r\n6F\r\n2C\r\n20\r\n57\r\n6F\r\n72\r\n6C\r\n64\r\n21\r\n0D\r\n0A\r\n65\r\n31\r\n00\r\n00\r\n94\r\n30\r\n00\r\n00\r\n72\r\n31\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n65\r\n31\r\n00\r\n00\r\n94\r\n30\r\n00\r\n00\r\n72\r\n31\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n45\r\n78\r\n69\r\n74\r\n50\r\n72\r\n6F\r\n63\r\n65\r\n73\r\n73\r\n00\r\n00\r\n57\r\n72\r\n69\r\n74\r\n65\r\n46\r\n69\r\n6C\r\n65\r\n00\r\n47\r\n31\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n00\r\n02\r\n30\r\n00\r\n00\r\n57\r\n31\r\n00\r\n00\r\n";

        public static void MakeDummyExe(string outputPath)
        {
            string[] hexValues = hex.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[hexValues.Length];

            for (int i = 0; i < hexValues.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexValues[i], 16);
            }

            File.WriteAllBytes(outputPath, bytes);
        }

        public static void MakeDummyZip(string zipPath)
        {
            string[] hexValues = hex.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            byte[] exeBytes = new byte[hexValues.Length];

            for (int i = 0; i < hexValues.Length; i++)
            {
                exeBytes[i] = Convert.ToByte(hexValues[i], 16);
            }

            using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                string folderName = "DummyGameFolder/";         // folder inside zip
                var entry = archive.CreateEntry(folderName + "DummyGame.exe");

                using var stream = entry.Open();
                stream.Write(exeBytes, 0, exeBytes.Length);
            }
        }
    }
}
