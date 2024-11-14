using System.Net.Sockets;
using System.Net;
using System.Text;
using Npgsql;

namespace sentral
{
    class Sentral
    {
        static List<Kort> gyldigeKort = new List<Kort>
        {
            new Kort { KortID = "1111", PIN = "0000" },
            new Kort { KortID = "2222", PIN = "1111" },
            new Kort { KortID = "3333", PIN = "2222" }
        };
        static string connString;
        static bool isRunning;
        static bool Kort_validering = false;
        static void Main(string[] args)
        {

            connString = "Host=ider-database.westeurope.cloudapp.azure.com;Username=h599034;Password=Breezed29;Database=h599034";

            Thread Server = new Thread(StartServer);
            Server.Start();


            Thread Bruker = new Thread(Bruker_input);
            Bruker.Start();


            //Bruker_input(innput);

        }


        static void StartServer()
        {
            // Oppretter en socket for å lytte etter innkommende tilkoblinger
            Socket lytteSokkel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Definerer IP-adressen (localhost) og porten som serveren skal lytte på (port 9050)
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            // Binder socketen til IP-adressen og porten, slik at den kan motta tilkoblinger
            lytteSokkel.Bind(serverEP);

            // Lytter etter innkommende tilkoblinger, med en kø på inntil 10 tilkoblinger
            lytteSokkel.Listen(10);

            isRunning = true;
            //Console.SetCursorPosition(0,0);
            Console.WriteLine("Server started on " + "127.0.0.1" + ":" + "9050");
            while (isRunning)
            {
                // Accept an incoming connection
                Socket clientSocket = lytteSokkel.Accept();

                //Console.SetCursorPosition(0, 1);
                Console.WriteLine("Client connected");

                // Handle client in a new thread to keep the server responsive
                Thread clientThread = new Thread(KlientKommunikasjon);
                clientThread.Start(clientSocket);
            }
        }


        // Denne metoden kjøres i en egen tråd for hver tilkoblet klient
        static void KlientKommunikasjon(object o)
        {
            // Caster objektet som en Socket, slik at vi kan bruke socketen til å kommunisere
            Socket kommSokkel = o as Socket;

            // Henter lokal og ekstern (klientens) nettverksinformasjon
            IPEndPoint l = kommSokkel.LocalEndPoint as IPEndPoint;
            IPEndPoint r = kommSokkel.RemoteEndPoint as IPEndPoint;

            Console.WriteLine($"Kommuniserer med klient {r.Address}:{r.Port}");

            // Sender en velkomstmelding til klienten
            string melding = "Send KortID-PIN for å sjekke tilgang.";
            SendData(melding, kommSokkel);

            // Loop som holder kommunikasjonen med klienten åpen så lenge klienten er tilkoblet
            while (true)
            {
                try
                {
                    // Mottar data fra klienten
                    string dataFraKlient = MottaData(kommSokkel);

                    // Hvis mottatt data er tom, betyr det at klienten har koblet fra
                    if (string.IsNullOrEmpty(dataFraKlient))
                    {
                        Console.WriteLine("Klient har koblet fra.");
                        break;
                    }



                    // Sjekker KortID og PIN
                    var deler = dataFraKlient.Split('-');
                    if (deler.Length == 2)
                    {
                        try
                        {
                            // Forsøk å konvertere første delen til en integer (KortID) med Convert.ToInt32
                            string kortID = deler[0];

                            // Sjekk at PIN (andre del) ikke er null eller tom
                            if (!string.IsNullOrEmpty(deler[1]))
                            {
                               
                                string pin = deler[1];
                                bool tilgang = ValiderKortIDPin(connString,kortID,pin);
                                LoggInnpasseringsforesporsel(connString, kortID, pin, tilgang);
                                string svar = (tilgang ? "Tilgang godkjent" : "Tilgang Avslått");
                                SendData(svar, kommSokkel);
                                //if (tilgang == true)
                                //{
                                //    string svar = "Tilgang godkjent";
                                //    SendData(svar, kommSokkel);
                                //}
                                //else if (tilgang == false)
                                //{
                                //    string svar = "tilgang ikkje godkjent";
                                //    SendData(svar, kommSokkel);
                                //}

                            }
                            else
                            {
                                SendData("Feil format. PIN kan ikke være tom.", kommSokkel);
                            }
                        }
                        catch (FormatException)
                        {
                            // Håndterer feil hvis konverteringen til int mislykkes
                            SendData("Feil format. KortID må være et tall.", kommSokkel);
                        }
                    }
                    else
                    {
                        SendData("Feil format. Bruk KortID-PIN.", kommSokkel);
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Forbindelsen ble brutt.");
                    break;
                }
            }

            // Lukker socketen når kommunikasjonen med klienten er ferdig
            kommSokkel.Close();
        }

        // Metode for å sjekke om en KortID og PIN er gyldig
        static bool SjekkTilgang(string kortID, string pin)
        {
            foreach (var kort in gyldigeKort)
            {
                if (kort.KortID == kortID && kort.PIN == pin)
                {
                    Console.WriteLine($"Tilgang godkjent for KortID {kortID}.");
                    return true;
                }
            }

            Console.WriteLine($"Tilgang avslått for KortID {kortID}.");
            return false;
        }

        // Metode for å sende data til klienten
        static bool SendData(string data, Socket s)
        {
            try
            {
                byte[] dataSomBytes = Encoding.ASCII.GetBytes(data);
                s.Send(dataSomBytes);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Feil under sending av data: " + ex.Message);
                return false;
            }
        }

        // Metode for å motta data fra klienten
        static string MottaData(Socket s)
        {
            try
            {
                byte[] dataSomBytes = new byte[1024];
                int nMottatt = s.Receive(dataSomBytes);

                if (nMottatt == 0) return null;

                return Encoding.ASCII.GetString(dataSomBytes, 0, nMottatt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Feil under mottak av data: " + ex.Message);
                return null;
            }
        }
        static void Bruker_input(object o)
        {

            while (true)
            {

                Console.WriteLine("1. Valider KortID/PIN");
                Console.WriteLine("2. Motta Alarm fra Kortleser");
                Console.WriteLine("3. Administrer Brukere");
                Console.WriteLine("4. Administrer Kortlesere");
                Console.WriteLine("Velg et alternativ:");

                //Tekstplassering();
                Console.Write("Skriv inn kommando: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("KortID:");
                        var kortId = Console.ReadLine();
                        Console.WriteLine("PIN:");
                        var pinKode = Console.ReadLine();
                        bool godkjent = ValiderKortIDPin(connString, kortId, pinKode);
                        LoggInnpasseringsforesporsel(connString, kortId, pinKode, godkjent);
                        Console.WriteLine(godkjent ? "Tilgang godkjent" : "Tilgang Avslått");
                        break;
                    case "2":
                        MottaAlarm(connString);
                        break;
                    case "3":
                        AdministrerBrukere(connString);
                        break;
                    case "4":
                        AdministrerKortlesere(connString);
                        break;
                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }
            }


        }
        
        static bool ValiderKortIDPin(string connString, string kortId, string pinKode)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT PIN FROM Bruker WHERE KortID = @kortId", conn))
                {
                    cmd.Parameters.AddWithValue("kortId", kortId);
                    string pin = cmd.ExecuteScalar().ToString();
                    if (pinKode == pin)
                    {
                        //Console.WriteLine("Godkjent");
                        return true;
                    }
                    else 
                    {
                        return false;
                    } 
                    //må legge inn noe som sjekker om kortID og PIN stemmer overns med det som er i databasen og gir en true tilbake
                }
            }
        }

        static void LoggInnpasseringsforesporsel(string connString, string kortId, string pinKode, bool godkjent)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO Innpassering (KortID, PIN, Godkjent) VALUES (@kortId, @pinKode, @godkjent)", conn))
                {
                    cmd.Parameters.AddWithValue("kortId", kortId);
                    cmd.Parameters.AddWithValue("pinKode", pinKode);
                    cmd.Parameters.AddWithValue("godkjent", godkjent);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void MottaAlarm(string connString)
        {
            Console.WriteLine("Alarmtype:");
            var alarmType = Console.ReadLine();
            Console.WriteLine("Kortleser Nummer:");
            var nummer = Console.ReadLine();
            Console.WriteLine("KortID:");
            var kortId = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO Alarmer (Alarmtype, Nummer, KortID) VALUES (@alarmType, @nummer, @kortId)", conn))
                {
                    cmd.Parameters.AddWithValue("alarmType", alarmType);
                    cmd.Parameters.AddWithValue("nummer", nummer);
                    cmd.Parameters.AddWithValue("kortId", kortId);
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Alarmtype: {alarmType}, Kortleser: {nummer}, Tidspunkt: {DateTime.Now}, KortID: {kortId}");
        }

        static void AdministrerBrukere(string connString)
        {
            Console.WriteLine("1. Legg til Bruker");
            Console.WriteLine("2. Slett Bruker");
            Console.WriteLine("3. Endre Bruker");
            Console.WriteLine("Velg et alternativ:");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    LeggTilBruker(connString);
                    break;
                case "2":
                    SlettBruker(connString);
                    break;
                case "3":
                    EndreBruker(connString);
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }

        static void LeggTilBruker(string connString)
        {
            Console.WriteLine("Etternavn:");
            var etternavn = Console.ReadLine();
            Console.WriteLine("Fornavn:");
            var fornavn = Console.ReadLine();
            Console.WriteLine("E-post:");
            var epost = Console.ReadLine();
            Console.WriteLine("KortID:");
            var kortId = Console.ReadLine();
            Console.WriteLine("PIN:");
            var pinKode = Console.ReadLine();
            Console.WriteLine("Startgyldighet (YYYY-MM-DD hh:mm:ss):");
            var startGyldighet = Console.ReadLine();
            Console.WriteLine("Sluttgyldighet (YYYY-MM-DD hh:mm:ss):");
            var sluttGyldighet = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO Bruker (Etternamn, Fornamn, Epost, KortID, PIN, Startgyldighet, Sluttgyldighet) VALUES (@etternavn, @fornavn, @epost, @kortId, @pinKode, @startGyldighet, @sluttGyldighet)", conn))
                {
                    cmd.Parameters.AddWithValue("etternavn", etternavn);
                    cmd.Parameters.AddWithValue("fornavn", fornavn);
                    cmd.Parameters.AddWithValue("epost", epost);
                    cmd.Parameters.AddWithValue("kortId", kortId);
                    cmd.Parameters.AddWithValue("pinKode", pinKode);
                    cmd.Parameters.AddWithValue("startGyldighet", startGyldighet == "" ? (object)DBNull.Value : DateTime.Parse(startGyldighet));
                    cmd.Parameters.AddWithValue("sluttGyldighet", sluttGyldighet == "" ? (object)DBNull.Value : DateTime.Parse(sluttGyldighet));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void SlettBruker(string connString)
        {
            Console.WriteLine("KortID:");
            var kortId = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM Bruker WHERE KortID = @kortId", conn))
                {
                    cmd.Parameters.AddWithValue("kortId", kortId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void EndreBruker(string connString)
        {
            Console.WriteLine("KortID:");
            var kortId = Console.ReadLine();
            Console.WriteLine("Nytt PIN:");
            var pinKode = Console.ReadLine();
            Console.WriteLine("Ny startgyldighet (YYYY-MM-DD hh:mm:ss):");
            var startGyldighet = Console.ReadLine();
            Console.WriteLine("Ny sluttgyldighet (YYYY-MM-DD hh:mm:ss):");
            var sluttGyldighet = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE Bruker SET PIN = @pinKode, Startgyldighet = @startGyldighet, Sluttgyldighet = @sluttGyldighet WHERE KortID = @kortId", conn))
                {
                    cmd.Parameters.AddWithValue("pinKode", pinKode);
                    cmd.Parameters.AddWithValue("startGyldighet", startGyldighet == "" ? (object)DBNull.Value : DateTime.Parse(startGyldighet));
                    cmd.Parameters.AddWithValue("sluttGyldighet", sluttGyldighet == "" ? (object)DBNull.Value : DateTime.Parse(sluttGyldighet));
                    cmd.Parameters.AddWithValue("kortId", kortId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void AdministrerKortlesere(string connString)
        {
            Console.WriteLine("1. Legg til Kortleser");
            Console.WriteLine("2. Slett Kortleser");
            Console.WriteLine("3. Endre Kortleser");
            Console.WriteLine("Velg et alternativ:");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    LeggTilKortleser(connString);
                    break;
                case "2":
                    SlettKortleser(connString);
                    break;
                case "3":
                    EndreKortleser(connString);
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }

        static void LeggTilKortleser(string connString)
        {
            Console.WriteLine("Nummer:");
            var Nummer = Console.ReadLine();
            Console.WriteLine("Plassering:");
            var Plassering = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO Kortleser (Nummer, Plassering) VALUES (@Nummer, @Plassering)", conn))
                {
                    cmd.Parameters.AddWithValue("Nummer", Nummer);
                    cmd.Parameters.AddWithValue("Plassering", Plassering);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void SlettKortleser(string connString)
        {
            Console.WriteLine("Nummer:");
            var Nummer = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM Kortleser WHERE Nummer = @Nummer", conn))
                {
                    cmd.Parameters.AddWithValue("Nummer", Nummer);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static void EndreKortleser(string connString)
        {
            Console.WriteLine("Nummer:");
            var Nummer = Console.ReadLine();
            Console.WriteLine("Ny plassering:");
            var Plassering = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE Kortleser SET Plassering = @Plassering WHERE Nummer = @Nummer", conn))
                {
                    cmd.Parameters.AddWithValue("Plassering", Plassering);
                    cmd.Parameters.AddWithValue("Nummer", Nummer);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
