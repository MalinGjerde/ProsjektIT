using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;


namespace Kortleser_program
{
    internal class Kortleser
    {
        static string kortlesernummer = "";
        static string romnummer = "";
        static Stopwatch Tidgått;
        static bool døråpen = false;
        private static Socket clientSocket;
        private static SerialPort serialPort;

        static bool e5;
        static bool e6;
        static bool e7;
        static void Main(string[] args)
        {
            Tidgått = new Stopwatch();
            e5 = false;
            e6 = false;
            e7 = false;

            Console.WriteLine("Oppgi kortlesernummer(xxxx): " );
            kortlesernummer = Console.ReadLine();


            // koble til server
            KobleTilServer("127.0.0.1", 9050);

            // åpne seriel port om den er tilgjenlig legg in rett com port
            OpenSerialPort("COM1", 9600);

            // Start a thread to listen for data from the serial port
            Thread serialThread = new Thread(new ThreadStart(BehandleSerielData));
            serialThread.Start();

            
            //kan startes som en egen tråd om det trengs
            BrukerInput();


            //lage en metode som behandler den motatte daten fra server ok eller ikke ok
            //alarm funksjoner via serielport kom
            //behandelr data i fra seriel komunikasjonen

        }  // av main

        static void VisKommunikasjonsinformasjon(IPEndPoint l, IPEndPoint r)
        {
            Console.WriteLine("Kommuniserer med {0}:{1}, min nettverksinfo er {2}:{3}", r.Address, r.Port, l.Address, l.Port);
        }

        static void BrukerInput()
        {
            IPEndPoint l = clientSocket.LocalEndPoint as IPEndPoint;
            IPEndPoint r = clientSocket.RemoteEndPoint as IPEndPoint;
            VisKommunikasjonsinformasjon(l, r);

            string dataFraServer = MottaData(clientSocket);
            Console.WriteLine(dataFraServer);

            bool ferdig = false;

            while (!ferdig)
            {
                Console.Write("Oppgi data som skal sendes til server: ");
                string dataFraBruker = "";
                dataFraBruker = Console.ReadLine();

                //lage en switch struktur med forventet innputs?

                switch (dataFraBruker.ToLower())
                {
                    //legge inn spesifike kommandoer som skal sendes til seriel og ikkje server default er å sende til server
                    case "avslutt":
                        ferdig = true;
                        break;

                    case "dør":
                        døråpen = false;
                        break;
                    

                    default:
                        bool altOK = SendData(dataFraBruker, clientSocket);

                        if (altOK)
                        {
                            dataFraServer = MottaData(clientSocket);


                            Console.Write("Svar fra server: ");
                            Console.WriteLine(dataFraServer);
                            BehandleDataFraServer(dataFraServer);

                        }
                        break;
                        
                }

                //if (dataFraBruker == "avslutt")
                //{
                //    ferdig = true;
                //}
                //else if (dataFraBruker.Length > 0)
                //{
                //    bool altOK = SendData(dataFraBruker, clientSocket);

                //    if (altOK)
                //    {
                //        dataFraServer = MottaData(clientSocket);


                //        Console.Write("Svar fra server: ");
                //        Console.WriteLine(dataFraServer);

                //    }
                //    else
                //    {
                //        Console.WriteLine("Avslutter klientprogram");
                //        ferdig = true;

                //    }
                //}
            }

            Console.WriteLine("Bryter forbindelsen med serveren ...");
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            Console.ReadKey();
        }

        static void BehandleDataFraServer(string s)
        {
            if (s.ToLower() == "tilgang godkjent")
            {
                Console.WriteLine("Låser opp dør....");
                //if (e5 == false)

                //SendMelding("$E0", serialPort);

                Thread dør = new Thread(DørÅpen);
                døråpen = true;
                dør.Start();
                //legge inn kode som sender rette seriel kommando for å aktivere dør e5
                //Thread.Sleep(2000);
                //legge inn kode som sender rette seriel kommando for å deaktivere dør e5
                //så lenge e6 dør er lukket så skal den låses igjen
                //Console.WriteLine("Låser dør......");
                    
            }
        }

        static void DørÅpen()
        {
            bool alarmaktiv = false;
            Console.WriteLine("tid startet");
            Tidgått.Start();
            //bytte true med kortet sin data for åpen dør
            while (døråpen)
            {
                if (Tidgått.ElapsedMilliseconds >= 5000 && !alarmaktiv)
                {
                    Console.WriteLine("Alarm har gått");
                    //Console.BackgroundColor;
                    
                    alarmaktiv = true;
                }
                
            }
            alarmaktiv = false;
            Tidgått.Restart();
            Console.WriteLine("Dør lukket, alarm deaktivert");
            //skal gi alarm om døren er åpen for lenge

        }

        static bool Avlest_e5()
        {
            bool svar = false;
            return svar;
        }

        static bool SendData(string data, Socket s)
        {
            bool svar = true;
            try
            {
                byte[] dataSomBytes = new byte[1024];
                dataSomBytes = Encoding.ASCII.GetBytes(data);
                s.Send(dataSomBytes);
            }
            catch (Exception unntak)
            {
                Console.WriteLine("Feil uunder sending av data: " + unntak.Message);
                svar = false;
            }
            return svar;
        }

        static string MottaData(Socket s)
        {
            string svar = "";
            try
            {
                byte[] dataSomBytes = new byte[1024];
                int nMottatt = s.Receive(dataSomBytes);
                svar = Encoding.ASCII.GetString(dataSomBytes, 0, nMottatt);
            }
            catch (Exception unntak)
            {
                Console.WriteLine("Feil uunder mottak av data: " + unntak.Message);
            }
            return svar;
        }
        static void KobleTilServer(string ipAddress, int port)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
                Console.WriteLine("Connected to server at {0}:{1}", ipAddress, port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting to server: " + e.Message);
            }
        }

        static void OpenSerialPort(string portName, int baudRate)
        {
            try
            {
                serialPort = new SerialPort(portName, baudRate);
                serialPort.Open();
                Console.WriteLine("Serial port {0} opened at baud rate {1}", portName, baudRate);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error opening serial port: " + e.Message);
            }
        }
        static void BehandleSerielData()
        {
            try
            {
                while (serialPort.IsOpen)
                {
                    string serialData = serialPort.ReadLine();
                    Console.WriteLine("Received from serial port: " + serialData);

                    // Send the data to the server
                    //Bytte denne koden ut slik at kortleser program håndtere data og sender kun alarmer til server
                    byte[] data = Encoding.ASCII.GetBytes(serialData);
                    if (clientSocket.Connected)
                    {
                        clientSocket.Send(data);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Serial port read error: " + e.Message);
            }
        }
        static string HentUtEnMelding(ref string data)
        {
            string svar = "";

            int posStart = data.IndexOf('$');
            int posSlutt = data.IndexOf('#');

            svar = data.Substring(posStart, (posSlutt - posStart) + 1);

            if (data.Length == posSlutt + 1)
                data = "";
            else
                data = data.Substring(posSlutt + 1);

            return svar;
        }

        static bool EnHelMeldingMotatt(string data)
        {
            bool svar = false;
            int posStart = data.IndexOf('$');
            int posSlutt = data.IndexOf('#');

            if (posStart != -1 && posSlutt != -1)
            {
                if (posStart < posSlutt)
                    svar = true;
            }

            return svar;
        }

        static string MottaData(SerialPort s)
        {
            string svar = "";
            try
            {
                svar = s.ReadExisting();
            }

            catch (Exception u)
            {
                Console.WriteLine($"Feil {u.Message}");
            }

            return svar;
        }

        static void SendMelding(string enMelding, SerialPort s)
        {
            try
            {
                s.Write(enMelding);
            }
            catch (Exception u)
            {
                Console.WriteLine($"Feil {u.Message}");
            }
        }
    }

}




