using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class Stage
    {
        private string StageName;
        private int BattleFieldIndex;
        private int WaveCount;
        private int CurProgress;
        private List<List<string>> WaveList = new List<List<string>>();

        public bool SetStage(TextAsset ta)
        {
            if(ta == null)
            {
                Debug.Log("no stage text");
                return false;
            }
            else
            {
                string[] lines = ta.text.Split('\n');
                for(int i = 0; i < lines.Length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i]))
                        continue;
                    string[] kv = lines[i].Split(':');

                    switch (kv[0])
                    {
                        case "Name":
                            StageName = kv[1].Trim();
                            break;
                        case "BattleField":
                            BattleFieldIndex = int.Parse(kv[1]);
                            break;
                        case "Wave":
                            string[] wv = kv[1].Split(' ');
                            WaveCount = int.Parse(wv[0]);
                            SetWave(wv);
                            break;
                        case "Progress":
                            CurProgress = int.Parse(kv[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
        }

        void SetWave(string[] waves)
        {
            for(int i = 1; i <= WaveCount; i++)
            {
                List<string> tmp = new List<string>();
                string str = waves[i].Substring(1, waves[i].Trim().Length - 2);
                string[] wave = str.Split(',');
                for(int j = 0; j < wave.Length; j++)
                {
                    tmp.Add(wave[j].Trim());
                }
                WaveList.Add(tmp);
            }
        }

        public void ShowStageInfo()
        {
            Debug.Log("Stage Name: " + StageName);
            Debug.Log("Background index: " + BattleFieldIndex);
            Debug.Log("Wave Count:" + WaveCount);
            foreach(List<string> wave in WaveList)
            {
                Debug.Log("wave: " + WaveList.IndexOf(wave));
                foreach(string enemy in wave)
                {
                    Debug.Log(enemy);
                }
            }
        }

        public string name()
        {
            return StageName;
        }

        public int BFindex()
        {
            return BattleFieldIndex;
        }

        public int WaveC()
        {
            return WaveCount;
        }

        public List<List<string>> WaveL()
        {
            return WaveList;
        }

        public int CurrentProgress()
        {
            return CurProgress;
        }
    }
}
