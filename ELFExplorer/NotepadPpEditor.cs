﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EmbeddedMemoryExplorer {
    public class NotepadPpEditor : FileEditor
    {

        public string Language { get; set; } = "cpp";

        public static string FindExecutablePath()
        {
            string[] paths = new[] {
                @"C:\Program Files (x86)\Notepad++\notepad++.exe",
                @"C:\Program Files\Notepad++\notepad++.exe"
            };

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return null;
        }

        public NotepadPpEditor(string executablePath)
        {
            FileTypes.Add(new FileExtensionMatch(".c"));
            FileTypes.Add(new FileExtensionMatch(".c++"));
            FileTypes.Add(new FileExtensionMatch(".cpp"));
            FileTypes.Add(new FileExtensionMatch(".cxx"));
            FileTypes.Add(new FileExtensionMatch(".h"));
            FileTypes.Add(new FileExtensionMatch(".hpp"));
            Path = executablePath;
        }

        public override string GetArguments(Uri file)
        {
            string result = file.AbsoluteUri;
            result += " " + LineNumberParameter(RowID);
            result += " " + ColumnNumberParameter(ColumnId);
            result += " " + LanguageParameter(Language);
            result= result.Replace("file:///", "");
            return result;
        }

        public static string LineNumberParameter(int line)
        {
            return string.Format("-n{0}", line);
        }

        public static string ColumnNumberParameter(int column) {
            return string.Format("-c{0}", column);
        }

        public static string LanguageParameter(string language) {
            return string.Format("-l{0}", language);
        }
    }
}
