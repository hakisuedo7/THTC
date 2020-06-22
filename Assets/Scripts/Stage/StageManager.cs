using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public static class StageManager
    {
        public static List<Stage> AllStages = new List<Stage>();
        public static void LoadAllStage()
        {
            string path = "Stage/";
            TextAsset[] TAs = Resources.LoadAll<TextAsset>(path);

            foreach (TextAsset TA in TAs)
            {
                Stage stage = LoadStage(TA);
                if (stage != null)
                    AllStages.Add(stage);
            }
        }

        public static Stage LoadStage(TextAsset ta)
        {
            Stage stage = new Stage();

            if (stage.SetStage(ta))
                return stage;
            else
                return null;
        }

        public static void ShowAllStagesInfo()
        {
            Debug.Log("Stage Info:");
            foreach(Stage stage in AllStages)
            {
                stage.ShowStageInfo();
            }
        }

        public static Stage GetStage(string name)
        {
            foreach(Stage stage in AllStages)
            {
                if(stage.name() == name)
                {
                    return stage;
                }
            }
            return null;
        }
    }
}
