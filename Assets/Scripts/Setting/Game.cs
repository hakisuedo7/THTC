using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Setting
{
    public static class Game
    {
        private static string _currentStageName;

        private static float _fallTick;
        private static float _movellTick;
        private static float _inputFallTick;

        private static int _blockDmg;

        public static string CURRENTSTAGE
        {
            get { return _currentStageName; }
            set { _currentStageName = value; }
        }

        public static float FALLTICK
        {
            get { return _fallTick; }
            set { _fallTick = value; }
        }

        public static float MOVETICK
        {
            get { return _movellTick; }
            set { _movellTick = value; }
        }

        public static float INPUTFALLTICK
        {
            get { return _inputFallTick; }
            set { _inputFallTick = value; }
        }

        public static int BLOCKDAMAGE
        {
            get { return _blockDmg; }
            set { _blockDmg = value; }
        }

        public static void Default()
        {
            _fallTick = 1.5f;
            _movellTick = 0.08f;
            _inputFallTick = 0.08f;
            _blockDmg = 10;
        }

    }
}
