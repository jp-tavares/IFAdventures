using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._02.Script.Utils
{
    public static class Controls
    {
        public static KeyCode[] CONFIRM = new KeyCode[] {
            KeyCode.Z,
            KeyCode.Space
        };

        public static KeyCode[] ESCAPE = new KeyCode[] {
            KeyCode.Escape
        };

        public static bool confirmAction()
        {
            return (Input.GetKeyDown(CONFIRM[0]) || Input.GetKeyDown(CONFIRM[1]));
        }

        public static bool escapeAction()
        {
            return Input.GetKeyDown(ESCAPE[0]);
        }
    }
}
