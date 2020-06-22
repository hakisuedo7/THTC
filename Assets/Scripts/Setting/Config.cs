using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Setting
{
    public static class Config
    {
        // 檔案路徑&名稱
        private static string ConfigFile = Application.persistentDataPath + "/TetrisCombat.cfg";
        // 背景音量
        private static int _BGMVolume;
        // 效果音量
        private static int _SEVolume;
        // 遊戲進度
        private static int _progress;

        public static void LoadConfig()
        {
            // 讀檔
            if (File.Exists(ConfigFile))
                SetConfig();
            else
                InitConfig();
        }

        public static void InitConfig()
        {
            Default();
            _progress = 0;
            SaveConfig();
        }

        public static void SetConfig()
        {
            string AllConfig = File.ReadAllText(ConfigFile);
            string[] LineConfig = AllConfig.Split(System.Environment.NewLine.ToCharArray());

            foreach(string line in LineConfig)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                string[] config = line.Split(':');

                switch (config[0])
                {
                    case "BGMVolume":
                        _BGMVolume = int.Parse(config[1]);
                        break;
                    case "SEVolume":
                        _SEVolume = int.Parse(config[1]);
                        break;
                    case "progress":
                        _progress = int.Parse(config[1]);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void SaveConfig()
        {
            // 存檔
            string data = "";

            data += "BGMVolume:" + _BGMVolume.ToString() + "\n";
            data += "SEVolume:" + _SEVolume.ToString() + "\n";
            data += "progress:" + _progress.ToString() + "\n";

            File.WriteAllText(ConfigFile, data);
        }

        public static int BGMVOLUME
        {
            get { return _BGMVolume; }
            set { _BGMVolume = value; }
        }

        public static int SEVOLUME
        {
            get { return _SEVolume; }
            set { _SEVolume = value; }
        }

        public static int PROGRESS
        {
            get { return _progress; }
            set { _progress = value; }
        }

        public static void Default()
        {
            _BGMVolume = 70;
            _SEVolume = 65;
        }

        public static void UpdateProgress(int index)
        {
            if (_progress <= index + 1)
                _progress = index + 1;
            SaveConfig();
        }
    }
}