using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace SimSim
{
	class CmdReceiver
	{
		public bool stopped = false;
		MainForm mainForm;

		public void RunCmdReceiver(MainForm frm)
		{
			mainForm = frm;

			ThreadStart ts = new ThreadStart(DoReceive);
			Thread receiverThread = new Thread(ts);
            receiverThread.Name = "RecThread";
            receiverThread.IsBackground = true;
			receiverThread.Start();
		}

		void DoReceive()
		{
            string cmdString = string.Empty;
			DateTime currDt;
            byte[] buff = new byte[10];

			while (!stopped)
			{
                if (mainForm.comPort.IsOpen)
                {
                    cmdString = cmdString + mainForm.comPort.ReadExisting();
                    if (cmdString.Length > 1)
                    {
                        switch (cmdString.Substring(0, 2))
                        {
                            case "$R":  // Polling
                                mainForm.SendFromThread();
                                break;

                            case "$N":  // $Nxxx - Endre nodenummer


                                // mainForm.NodeNum = cmdString.Substring(2);
                                int nodeNumber = int.Parse(cmdString.Substring(2, 3));
                                UpdateNodeNumberType UNNDelegate = new UpdateNodeNumberType(mainForm.UpdateNodeNumber);
                                mainForm.Invoke(UNNDelegate, nodeNumber);

                                break;

                            case "$D":  // $Dyyyymmdd - Sette dato
                                currDt = mainForm.SimDateTime;
                                try
                                {
                                    mainForm.SimDateTime =
                                        new DateTime(int.Parse(cmdString.Substring(2, 4)),
                                                     int.Parse(cmdString.Substring(6, 2)),
                                                     int.Parse(cmdString.Substring(8, 2)),
                                                     currDt.Hour, currDt.Minute, currDt.Second);
                                }
                                catch (Exception) { }
                                break;

                            case "$T":  // $Thhmmss - Sette tid
                                currDt = mainForm.SimDateTime;
                                try
                                {
                                    mainForm.SimDateTime =
                                        new DateTime(currDt.Year, currDt.Month, currDt.Day,
                                                     int.Parse(cmdString.Substring(2, 2)),
                                                     int.Parse(cmdString.Substring(4, 2)),
                                                     int.Parse(cmdString.Substring(6, 2)));
                                }
                                catch (Exception) { }
                                break;

                            case "$S":  // $Sxxx - Endre meldingsintervall
                                try
                                {
                                    int seconds = int.Parse(cmdString.Substring(2, 3));
                                    UpdateSendIntervalType USIDelegate = new UpdateSendIntervalType(mainForm.UpdateSendInterval);
                                    mainForm.Invoke(USIDelegate, seconds);
                                }
                                catch (Exception) { }
                                break;

                            case "$O":  // $Oxy - Sette digitale verdier
                                UpdateOutputCheckedBoxType OOCBDelegate = new UpdateOutputCheckedBoxType(mainForm.UpdateOutputCheckBox);
                                try
                                {
                                    int n = int.Parse(cmdString.Substring(2, 1));
                                    bool status = true;
                                    if (cmdString.Substring(3, 1) == "0") status = false;
                                    object[] argumenter = { (object)n, (object)status };
                                    mainForm.Invoke(OOCBDelegate, argumenter);
                                }
                                catch { }
                                break;
                        }
                        int startNextCommandPosition = cmdString.IndexOf('$', 1);
                        if (startNextCommandPosition > 0) cmdString = cmdString.Substring(startNextCommandPosition);
                        else cmdString = "";
                    }
                }
				Thread.Sleep(500);
			}
		}

	}
}
