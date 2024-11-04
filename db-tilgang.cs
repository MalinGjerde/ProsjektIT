using System;
using Npgsql;

//Installere PostgreSQL-støtte: dotnet add package Npgsql

//Tilkoblingsparametere til PostgreSQL database
var cs = "Host=20.56.240.122;Username=h123456;Password=pass;Database=h123456";

//Bruke tilkoblingsparameterene til å opprette en forbindelse
using var con = new NpgsqlConnection(cs);
con.Open();

//Opprette "spørreobjekt"
using var cmd = new NpgsqlCommand();
cmd.Connection = con;

//Sette SQL-kommando vi ønsker å utføre
cmd.CommandText = "drop table if exists ansatt;";
//Be om at den blir utført
cmd.ExecuteNonQuery();

cmd.CommandText = "create table ansatt (ansattnr int primary key, etternavn varchar(15), fornavn varchar(15), ansattdato date, stilling varchar(15), lonn int);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (1, 'Veum', 'Varg', '1996-01-01', 'Løpegutt', 383000);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (2, 'Stein', 'Trude', '2004-10-19', 'DBA', 470700);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (3, 'Dudal', 'Inger-Lise', '2012-12-24', 'Sekretær', 490000);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (4, 'Hansen', 'Hans', '2010-08-23', 'Programmerer', 525000);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (5, 'Bjørnsen', 'Henrik', '2014-01-01', 'Tekstforfatter', 575000);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (6, 'Gredelin', 'Sofie', '2012-05-18', 'Underdirektør', 825850);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (7, 'Zimmermann', 'Robert', '1999-05-17', 'Regnskapsfører', 575000);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (8, 'Nilsen', 'Lise', '2016-04-03', 'Direktør', 875340);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (11, 'Fosheim', 'Katinka', '2015-09-13', 'Selger', 620000);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (13, 'Lovløs', 'Ada', '2009-08-12', 'Programmerer', 584250);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (16, 'Ibsen', 'Bjørnstjerne', '2012-01-02', 'Tekstforfatter', 546000);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (17, 'Fleksnes', 'Marve', '2013-05-17', 'Lagerleder', 520120);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (20, 'Felgen', 'Reodor', '2005-12-12', 'Sykkelreperatør', 479500);";
cmd.ExecuteNonQuery();

cmd.CommandText = "insert into ansatt values (23, 'Karius', 'Jens', '2015-12-13', 'Salgssekretær', 480390);";
cmd.ExecuteNonQuery();

//Som ovefor, men basert på parametrisering
cmd.CommandText = "insert into ansatt values (@ansattnr, @etternavn, @fornavn, @ansattdato::date, @stilling, @lonn);";
cmd.Parameters.AddWithValue("ansattnr", 29);
cmd.Parameters.AddWithValue("etternavn", "Wirkola");
cmd.Parameters.AddWithValue("fornavn", "Gabriel");
cmd.Parameters.AddWithValue("ansattdato", "2018-04-21");
cmd.Parameters.AddWithValue("stilling", "Sekretær");
cmd.Parameters.AddWithValue("lonn", 455000);
//Vi er ferdige med å sette parametere
cmd.Prepare();
cmd.ExecuteNonQuery();

//Vi ønsker å lese ut det vi nettopp har lagt inn i databasen
cmd.CommandText = "select * from ansatt;";

//Opprette ett "leseobjekt"
using NpgsqlDataReader rdr = cmd.ExecuteReader();

//Hente navn på kolonner
Console.WriteLine($"{rdr.GetName(0),9} {rdr.GetName(1),-10} {rdr.GetName(2),-12} {rdr.GetName(3),-10} {rdr.GetName(4),-15} {rdr.GetName(5),7}");

//Hente ut data for hver rad
while (rdr.Read()){
  Console.WriteLine($"{rdr.GetInt32(0),9} {rdr.GetString(1),-10} {rdr.GetString(2),-12} {rdr.GetDate(3),-10} {rdr.GetString(4),-15} {rdr.GetInt32(5),7}");
}

