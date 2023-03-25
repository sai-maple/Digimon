using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public sealed class CsvConverter : EditorWindow
    {
        [SerializeField] private string _tsvText;

        [MenuItem("Tools/Digimon/CsvConverter")]
        private static void ShowWindow()
        {
            GetWindow<CsvConverter>("CsvConverter");
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (GUILayout.Button("変換"))
            {
                Convert();
            }

            EditorGUILayout.LabelField("TSV形式のテキストを入力してください");
            _tsvText = EditorGUILayout.TextArea(_tsvText, GUILayout.Height(500));

            EditorGUILayout.EndVertical();

            if (!EditorGUI.EndChangeCheck()) return;
        }

        private void Convert()
        {
            var stringBuilder = new StringBuilder();
            var fileName = "";
            var dirPath = Application.streamingAssetsPath + "/../Resources/Events";
            foreach (var line in _tsvText.Split("\n"))
            {
                if (line.Contains(".csv"))
                {
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        SaveTextToFile(stringBuilder.ToString(), Path.Combine(dirPath, fileName));
                        stringBuilder.Clear();
                    }

                    fileName = line.Split("\t")[0];
                    continue;
                }

                if(string.IsNullOrEmpty(line)) continue;
                stringBuilder.Append(line + "\n");
            }

            SaveTextToFile(stringBuilder.ToString(), Path.Combine(dirPath, fileName));
        }

        private static void SaveTextToFile(string text, string filePath, bool append = false)
        {
            var dirName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirName))
            {
                if (dirName != null) Directory.CreateDirectory(dirName);
            }

            // ファイルに文字列を書き込む
            var sw = new StreamWriter(filePath, append);
            sw.WriteLine(text.Replace("\t", ","));
            sw.Flush();
            sw.Close();

            // アセットだったらRefresh
            if (filePath.Contains(Application.dataPath))
            {
                AssetDatabase.Refresh();
            }
        }
    }
}