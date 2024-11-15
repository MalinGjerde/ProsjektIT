using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace SimSim
{
    class AutoSender
    {
        public bool stopped = false;
        MainForm mainForm;

        public void RunAutoSender(MainForm frm)
        {
            mainForm = frm;

            ThreadStart ts = new ThreadStart(AutoSend);
            Thread senderThread = new Thread(ts);
            senderThread.IsBackground = true;
            senderThread.Start();
        }

        void AutoSend()
        {
            while (!stopped)
            {
                if (mainForm.InvokeRequired)
                {
                    mainForm.SendFromThread();
                }

                Thread.Sleep(mainForm.AutoInterval * 1000);
            }
        }

    }
}
