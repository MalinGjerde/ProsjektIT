using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace SimSim
{

	public delegate void UpdateOutputCheckedBoxType(int number, bool isChecked);
	public delegate void UpdateSendIntervalType(int number);
	public delegate void UpdateNodeNumberType(int number);

	public partial class MainForm : Form
	{
		public int AutoInterval 
		{
			get 
			{
				try
				{
					return int.Parse(tbIntervall.Text); 
				}
				catch (Exception)
				{
					return 5;
				}
			}

			set 
			{
				if (value > 0)
				{
					tbIntervall.Text = value.ToString();
				}
				else
				{
					tbIntervall.Text = "5";
				}
			}
		}
		AutoSender autoSender;
		CmdReceiver cmdReceiver;

		public SerialPort comPort = new SerialPort();
		public string NodeNum
		{ 
			get { return tbNodeNum.Text; }
			set 
			{ 
				tbNodeNum.Text = value;
				nodeNum = int.Parse(value);
			}
		}
		public DateTime SimDateTime { get; set; }

		public CheckBox[] inputs = new CheckBox[8];
		public CheckBox[] outputs = new CheckBox[8];
		int nodeNum = 1;
		int Termistor { get { return trbTermistor.Value; } }
		int Potm1 { get { return trbPotm1.Value; } }
		int Potm2 { get { return trbPotm2.Value; } }
		int TempSens1 { get { return trbTemp1.Value; } }
		int TempSens2 { get { return trbTemp2.Value; } }

		public MainForm()
		{
			InitializeComponent();

			SimDateTime = DateTime.Now;
			Point inpBase = label1.Location;
			Point outpBase = label2.Location;

			for (int i = 0; i < inputs.Length; i++)
			{
				inputs[i] = new CheckBox();
				inputs[i].Width = 30;
				inputs[i].Text = i.ToString();
				inputs[i].Location = new Point(inpBase.X + 20, inpBase.Y + 20 + 20 * i);
				this.Controls.Add(inputs[i]);
			}

			for (int i = 0; i < outputs.Length; i++)
			{
				outputs[i] = new CheckBox();
				outputs[i].Width = 30;
				outputs[i].Text = i.ToString();
				outputs[i].Location = new Point(outpBase.X + 20, outpBase.Y + 20 + 20 * i);
				this.Controls.Add(outputs[i]);
			}

			// this.Load += new EventHandler(MainForm_Load);
		}

		public void UpdateNodeNumber(int nodeNumber)
		{
			NodeNum = nodeNumber.ToString();
		}

		public void UpdateSendInterval(int seconds)
		{
			AutoInterval = seconds;
		}

		public void UpdateOutputCheckBox(int number, bool isChecked)
		{
			try
			{
				if ((number >= 0) && (number < 9))
				{
					outputs[number].Checked = isChecked;
				}
				else if (number == 9)
				{
					for (int i = 0; i < 8; i++)
					{
						outputs[i].Checked = isChecked;
					}
				}
				else { }
			}
			catch { }
		}


		void MainForm_Load(object sender, EventArgs e)
		{
			string[] portNames = SerialPort.GetPortNames();
			foreach (string name in portNames)
			{
				cbComPort.Items.Add(name);
			}
			cbComPort.SelectedIndex = 0;

			autoSender = new AutoSender();
			cmdReceiver = new CmdReceiver();
			cmdReceiver.RunCmdReceiver(this);
		}

		private void cbComPort_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				comPort.Close();
				comPort.PortName = cbComPort.SelectedItem.ToString();
				comPort.BaudRate = 9600;
                comPort.ReadTimeout = 1000;
				comPort.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Prøv en annen serieport.");
			}
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnSendStatus_Click(object sender, EventArgs e)
		{
			SendStatus();
		}

		private string DigToStr(CheckBox[] chkArr)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < chkArr.Length; i++)
			{
				if (chkArr[i].Checked)
					sb.AppendFormat("1");
				else
					sb.AppendFormat("0");
			}

			return sb.ToString();
		}

		private void chkAutoSend_CheckedChanged(object sender, EventArgs e)
		{
			if (chkAutoSend.Checked)
			{
				label10.Visible = true;
				tbIntervall.Visible = true;
				// tbIntervall.Enabled = false;
				
                
                autoSender.stopped = false;
				autoSender.RunAutoSender(this);
			}
			else
			{
				autoSender.stopped = true;
				label10.Visible = false;
				tbIntervall.Visible = false;
				// tbIntervall.Enabled = true;
			}
		}


		delegate void SendDelegate();
		public void SendFromThread()
		{
			if (InvokeRequired)
			{
				SendDelegate d = new SendDelegate(SendStatus);
				Invoke(d);
			}
		}

		public void SendStatus()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("{0}{1}$A{2:D3}B{3}C{4}D{5}E{6}F{7:D4}G{8:D4}H{9:D4}I{10:D3}J{11:D3}#",
				Convert.ToChar(10), Convert.ToChar(13), nodeNum, SimDateTime.ToString("yyyyMMdd"), 
				SimDateTime.ToString("hhmmss"), DigToStr(inputs), DigToStr(outputs), Termistor, 
				Potm1, Potm2, TempSens1, TempSens2);

			string s = sb.ToString();
			comPort.Write(s);
		}
	}
}
