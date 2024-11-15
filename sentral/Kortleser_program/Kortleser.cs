using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
        

        //test
        static bool e5;
        static bool e6;
        static bool e7;
        static bool alarm_;
        static bool alarm_door_open;
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
            OpenSerialPort("COM4", 9600);

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
                Console.WriteLine("1. Scan kort");
                Console.Write("Tast inn kommando: ");
                string dataFraBruker = "";
                dataFraBruker = Console.ReadLine();

                //lage en switch struktur med forventet innputs?

               
                switch (dataFraBruker.ToLower())
                {
                    //legge inn spesifike kommandoer som skal sendes til seriel og ikkje server default er å sende til server
                    case "avslutt":
                        ferdig = true;
                        break;

                    case "1":

                        Console.Write("Tast inn kort ID: ");
                        string KortID = Console.ReadLine();

                        Console.Write("Tast inn PIN: ");
                        string PIN = Console.ReadLine();

                        string Kortscann = $"{KortID}-{PIN}";
                        bool altOK = SendData(Kortscann, clientSocket);

                        if (altOK)
                        {
                            dataFraServer = MottaData(clientSocket);


                            Console.Write("Svar fra server: ");
                            Console.WriteLine(dataFraServer);
                            BehandleDataFraServer(dataFraServer);

                        }

                        break;

                    default:
                        Console.WriteLine("Ugyldig kommando");
                        
                        break;
                        
                }
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
                if (e5 == false)
                SendMelding("$O51", serialPort);

                Thread dør = new Thread(DørÅpen);
                dør.Start();

            }
            else
                Console.WriteLine("Tilgang Avslått");
        }

        static void DørÅpen()
        {
            bool opened = false;
            alarm_door_open = false;
            bool ferdig = false;
            bool meldingsendt = false;
            while (!ferdig)
            {
                Thread.Sleep(500);

                while (e5 == true)
                {
                    Tidgått.Start();
                    //Console.WriteLine("Dør låst opp");
                    //Venter på at døren skal bli åpnet
                    while (e6 == true)
                    {
                        if (!opened)
                        {
                            Tidgått.Restart();
                            opened = true;
                        }
                        //Console.WriteLine("Dør Åpent åpnet");
                        //Setter av alarm om dør er åpen lenger en gitt tid
                        if (Tidgått.ElapsedMilliseconds >= 5000 && !alarm_door_open)
                        {
                            //Console.WriteLine("Alarm har gått");
                            //Console.BackgroundColor;

                            alarm_door_open = true;
                        }

                    }

                    //Resetter alarm når dør er lukket
                    alarm_door_open = false;

                    //Sender melding om å låse dør igjen visst den har vært åpenet etter den er låst opp
                    if (opened == true)
                    {
                        if (!meldingsendt)
                        {
                            SendMelding("$O50", serialPort);
                            meldingsendt = true;
                            Console.WriteLine("Låser dør......");
                        }
                        
                        Thread.Sleep(1000);
                        ferdig = true;
                    }

                  
                    //Låser dør igjen visst den ikkje er åpnet ila 10 sekunder fra den er låst opp
                    if (Tidgått.ElapsedMilliseconds >= 10000 && e6 == false)
                    {
                        if (!meldingsendt)
                        {
                            SendMelding("$O50", serialPort);
                            meldingsendt = true;
                            Console.WriteLine("Låser dør......");
                        }
                        Thread.Sleep(1000);
                        ferdig = true;
                    }
                    //skal gi alarm om døren er åpen for lenge
                }
                Tidgått.Restart();
                Tidgått.Stop();
            }
        }


        //Kode for å lese av verdier til de 3 digital I/O vi skal bruke
        static bool Avlest_e5(string s)
        {
            bool svar = false;
            int avlesseriellstreng = s.IndexOf('E');

            int avlest = Convert.ToInt32(s.Substring(avlesseriellstreng + 6, 1));

            

            if (avlest == 1)
            
                svar = true;

            return svar;
        }

        static bool Avlest_e6(string s)
        {
            bool svar = false;
            int indeksTempStart = s.IndexOf('E');

            int avlest = Convert.ToInt32(s.Substring(indeksTempStart + 7, 1));

           

            if (avlest == 1)
            
                svar = true;
            return svar;

        }

        static bool Avlest_e7(string s)
        {
            bool svar = false;
            int indeksTempStart = s.IndexOf('E');

            int avlest = Convert.ToInt32(s.Substring(indeksTempStart + 8, 1));

            

            if (avlest == 1)
            
                svar = true;

            return svar;
        }

        //KOden som skal returner en alarm visst slideren viser en verdi over 500
        static bool alarm_slider(string s)
        {
            bool svar = false;
            int indeksTempStart = s.IndexOf('F');

            int avlest = Convert.ToInt32(s.Substring(indeksTempStart + 1, 4));

            

            if (avlest > 500)
                svar = true;
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
            bool alarmsendt = false;
            try
            {
                while (serialPort.IsOpen)
                {
                    //string serialData = serialPort.ReadLine();
                    string serialData = "";
                    

                    Thread.Sleep(200);
                    serialData = serialData + MottaData(serialPort);

                    if (EnHelMeldingMotatt(serialData))
                    {
                        string enMelding = HentUtEnMelding(ref serialData);
                        //Console.WriteLine("Data fra SimSim: " + enMelding);


                        if (Avlest_e5(enMelding) == true)
                            e5 = true;
                        else
                            e5 = false;

                        if (Avlest_e6(enMelding))
                            e6 = true;
                        else
                            e6 = false;

                        if (Avlest_e7(enMelding))
                            e7 = true;
                        else
                            e7 = false;

                        if (alarm_slider(enMelding))
                            alarm_ = true;
                        else
                            alarm_ = false;
                        
                    }
                    // Send the data to the server
                    //Bytte denne koden ut slik at kortleser program håndtere data og sender kun alarmer til server
                    
                    if ((e7 || alarm_ || alarm_door_open == true) && alarmsendt == false)
                    {
                        if (clientSocket.Connected)
                        {
                            Console.WriteLine("Alarm aktiv");
                            SendData("Alarm",clientSocket);
                            alarmsendt = true;
                        }
                    }
                    else if (!e7 && !alarm_ && !alarm_door_open && alarmsendt == true)
                    {
                        Console.WriteLine("Alarm deaktivert");
                        alarmsendt = false;
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




