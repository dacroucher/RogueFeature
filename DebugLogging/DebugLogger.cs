using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace RogueFeature.DebugLogging
{
    public static class DebugLogger
    {
        public static TextBlock txtErrorBlock { private set; get; }
        public static TextBlock txtMessageBlock { private set; get; }
        private const uint iMaxCount = 5000;
        private const uint iMaxLineCharacters = 1000;
        private static uint errorCount;
        private static uint messageCount;

        static DebugLogger()
        {
            DebugLogger.errorCount = 0;
            DebugLogger.messageCount = 0;
        }
        public static void Setup(TextBlock txtMessageBlock, TextBlock txtErrorBlock)
        {
            DebugLogger.txtMessageBlock = txtMessageBlock;
            DebugLogger.txtErrorBlock = txtErrorBlock;
        }

        public static bool LogIt(string msg)
        {
            bool isErr = false;
            if (msg.Length >= 2)
            {
                if (msg.Substring(0, 2).Equals("$E"))
                {
                    isErr = true;
                }
            }

            if (DebugLogger.txtMessageBlock != null &&
                !isErr)
            {
                if (++messageCount == iMaxCount)
                {
                    DebugLogger.txtMessageBlock.Text = "";
                    messageCount = 1;
                }

                txtMessageBlock.Text += messageCount.ToString() + msg + "\n" + txtMessageBlock.Text;
                return true;
            }
            else if (DebugLogger.txtErrorBlock != null &&
                isErr)
            {
                if (++errorCount == iMaxCount)
                {
                    DebugLogger.txtErrorBlock.Text = "";
                    errorCount = 1;
                }

                txtErrorBlock.Text += errorCount.ToString() + msg + "\n" + txtErrorBlock.Text;
                return true;
            }
            return false;
        }
    }
}
