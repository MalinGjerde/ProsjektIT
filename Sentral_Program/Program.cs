using System.Net.Sockets;

namespace Sentral_Program
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Socket lytteSokkel = StartServer("127.0.0.1", 9050);


            while (true)
            {
                Console.WriteLine("Venter på en klient ...");
                Socket enKlientSokkel = lytteSokkel.Accept(); // blokkerende metode

                Thread t = new Thread(KlientKommunikasjon);
                t.Start(enKlientSokkel);
            }

            lytteSokkel.Close();

            //Tilstand maskin?
            //Legge inn bruker data
            //Slette bruker
            //endre bruker
            //Logg utskrift


            //Lagring til database
            //Legge inn nye personer i database





            //lese fra database

            //Logg for bruk av dør?


            //Utskift av rapporter
            //• liste brukerdata på grunnlag av brukernavn 
            //• liste adgangslogg(inkludert forsøk på adgang) på grunnlag av brukernavn mellom to datoer
            //• liste alle innpasseringsforsøk for en dør med ikke - godkjent adgang(uansett bruker) mellom to
            //  datoer 
            //• liste alle brukere med over 10 ikke godkjente adgangsforsøk(for testing kan du redusere tallet
            //  til et mindre antall)
            //• liste av alarmer mellom to datoer




            //Del av programet som skal være en server for kortleseren

            //valdier om kort og kode er godkjent

            //Registrer alarmer


        }//Main

        static Socket StartServer(string IP, int port)
        {
            Socket svar = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(IP), port);

            svar.Bind(serverEP);
            svar.Listen(10);

            return svar;
        }

        static void KlientKommunikasjon(object o)
        {

            Socket komSokkel = o as Socket;

            IPEndPoint l = komSokkel.LocalEndPoint as IPEndPoint;
            IPEndPoint r = komSokkel.RemoteEndPoint as IPEndPoint;
            VisKommunikasjonsinformasjon(l, r);


            string hilsen = "Velkommen til en enkel testserver";

            SendData(hilsen, komSokkel);

            bool ferdig = false;

            while (!ferdig)
            {


                string dataFraKlent = MottaData(komSokkel);
                ;
                if (dataFraKlent.Length > 0)
                {
                    Console.Write("Har fått data fra klient: ");
                    Console.WriteLine(dataFraKlent);


                    string svarTilKlient = ReverserTekst(dataFraKlent);

                    SendData(svarTilKlient, komSokkel);

                }
                else
                {
                    Console.WriteLine("kommunikajons med {0}:{1} avsluttes. ", r.Address, r.Port);
                    ferdig = true;
                }

            } // av while
            // Console.WriteLine("Forbindelsen med {0} er brutt", klientEP.Address);
            komSokkel.Close();

        }

        static string ReverserTekst(string tekst)
        {
            string svar = "";

            Char[] mottattArray = tekst.ToCharArray();
            Array.Reverse(mottattArray);
            StringBuilder sb = new StringBuilder();
            foreach (char ch in mottattArray)
                sb.Append(ch);

            svar = sb.ToString();
            return svar;
        }

        static void VisKommunikasjonsinformasjon(IPEndPoint l, IPEndPoint r)
        {
            Console.WriteLine("Kommuniserer med klient {0}:{1}, min nettverksinfo er {2}:{3}", r.Address, r.Port, l.Address, l.Port);
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
                if (svar == "avslutt") svar = "";
            }
            catch (Exception unntak)
            {
                Console.WriteLine("Feil uunder mottak av data: " + unntak.Message);
            }
            return svar;
        }
    }
}
