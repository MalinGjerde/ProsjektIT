PGDMP                  
    |            h599034    14.1    16.4     I           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            J           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            K           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            L           1262    511019    h599034    DATABASE     }   CREATE DATABASE h599034 WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Norwegian_Norway.1252';
    DROP DATABASE h599034;
                h599034    false                        2615    700060 
   ProsjektIT    SCHEMA        CREATE SCHEMA "ProsjektIT";
    DROP SCHEMA "ProsjektIT";
                h599034    false            �            1259    700096    Alarm    TABLE       CREATE TABLE "ProsjektIT"."Alarm" (
    antall integer NOT NULL,
    alarmtype character varying(50) NOT NULL,
    nummer character(4) NOT NULL,
    kortid character(4) NOT NULL,
    tidspunkt timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);
 !   DROP TABLE "ProsjektIT"."Alarm";
    
   ProsjektIT         heap    h599034    false    7            �            1259    700095    Alarm_antall_seq    SEQUENCE     �   CREATE SEQUENCE "ProsjektIT"."Alarm_antall_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE "ProsjektIT"."Alarm_antall_seq";
    
   ProsjektIT          h599034    false    237    7            M           0    0    Alarm_antall_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE "ProsjektIT"."Alarm_antall_seq" OWNED BY "ProsjektIT"."Alarm".antall;
       
   ProsjektIT          h599034    false    236            �            1259    700061    Bruker    TABLE     W  CREATE TABLE "ProsjektIT"."Bruker" (
    etternamn character varying(100) NOT NULL,
    fornamn character varying(100) NOT NULL,
    epost character varying(255) NOT NULL,
    kortid character(4) NOT NULL,
    pin character(4) NOT NULL,
    startgyldighet timestamp without time zone DEFAULT '2024-01-01 00:00:00'::timestamp without time zone,
    sluttgyldighet timestamp without time zone DEFAULT '2025-12-31 23:59:59'::timestamp without time zone,
    CONSTRAINT "Bruker_kortid_check" CHECK ((kortid ~ '^[0-9]{4}$'::text)),
    CONSTRAINT "Bruker_pin_check" CHECK ((pin ~ '^[0-9]{4}$'::text))
);
 "   DROP TABLE "ProsjektIT"."Bruker";
    
   ProsjektIT         heap    h599034    false    7            �            1259    700073    Innpassering    TABLE       CREATE TABLE "ProsjektIT"."Innpassering" (
    ident integer NOT NULL,
    kortid character(4) NOT NULL,
    pin character(4) NOT NULL,
    godkjent boolean NOT NULL,
    nummer character(4) NOT NULL,
    dato timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);
 (   DROP TABLE "ProsjektIT"."Innpassering";
    
   ProsjektIT         heap    h599034    false    7            �            1259    700072    Innpassering_ident_seq    SEQUENCE     �   CREATE SEQUENCE "ProsjektIT"."Innpassering_ident_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 5   DROP SEQUENCE "ProsjektIT"."Innpassering_ident_seq";
    
   ProsjektIT          h599034    false    7    234            N           0    0    Innpassering_ident_seq    SEQUENCE OWNED BY     a   ALTER SEQUENCE "ProsjektIT"."Innpassering_ident_seq" OWNED BY "ProsjektIT"."Innpassering".ident;
       
   ProsjektIT          h599034    false    233            �            1259    700090 	   Kortleser    TABLE     r   CREATE TABLE "ProsjektIT"."Kortleser" (
    nummer character(4) NOT NULL,
    plassering character(4) NOT NULL
);
 %   DROP TABLE "ProsjektIT"."Kortleser";
    
   ProsjektIT         heap    h599034    false    7            �           2604    700099    Alarm antall    DEFAULT     |   ALTER TABLE ONLY "ProsjektIT"."Alarm" ALTER COLUMN antall SET DEFAULT nextval('"ProsjektIT"."Alarm_antall_seq"'::regclass);
 C   ALTER TABLE "ProsjektIT"."Alarm" ALTER COLUMN antall DROP DEFAULT;
    
   ProsjektIT          h599034    false    236    237    237            �           2604    700076    Innpassering ident    DEFAULT     �   ALTER TABLE ONLY "ProsjektIT"."Innpassering" ALTER COLUMN ident SET DEFAULT nextval('"ProsjektIT"."Innpassering_ident_seq"'::regclass);
 I   ALTER TABLE "ProsjektIT"."Innpassering" ALTER COLUMN ident DROP DEFAULT;
    
   ProsjektIT          h599034    false    233    234    234            F          0    700096    Alarm 
   TABLE DATA           U   COPY "ProsjektIT"."Alarm" (antall, alarmtype, nummer, kortid, tidspunkt) FROM stdin;
 
   ProsjektIT          h599034    false    237   �%       A          0    700061    Bruker 
   TABLE DATA           p   COPY "ProsjektIT"."Bruker" (etternamn, fornamn, epost, kortid, pin, startgyldighet, sluttgyldighet) FROM stdin;
 
   ProsjektIT          h599034    false    232   �%       C          0    700073    Innpassering 
   TABLE DATA           Z   COPY "ProsjektIT"."Innpassering" (ident, kortid, pin, godkjent, nummer, dato) FROM stdin;
 
   ProsjektIT          h599034    false    234   �%       D          0    700090 	   Kortleser 
   TABLE DATA           ?   COPY "ProsjektIT"."Kortleser" (nummer, plassering) FROM stdin;
 
   ProsjektIT          h599034    false    235   &       O           0    0    Alarm_antall_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('"ProsjektIT"."Alarm_antall_seq"', 1, false);
       
   ProsjektIT          h599034    false    236            P           0    0    Innpassering_ident_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('"ProsjektIT"."Innpassering_ident_seq"', 1, false);
       
   ProsjektIT          h599034    false    233            �           2606    700102    Alarm Alarm_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY "ProsjektIT"."Alarm"
    ADD CONSTRAINT "Alarm_pkey" PRIMARY KEY (antall);
 D   ALTER TABLE ONLY "ProsjektIT"."Alarm" DROP CONSTRAINT "Alarm_pkey";
    
   ProsjektIT            h599034    false    237            �           2606    700071    Bruker Bruker_epost_key 
   CONSTRAINT     ]   ALTER TABLE ONLY "ProsjektIT"."Bruker"
    ADD CONSTRAINT "Bruker_epost_key" UNIQUE (epost);
 K   ALTER TABLE ONLY "ProsjektIT"."Bruker" DROP CONSTRAINT "Bruker_epost_key";
    
   ProsjektIT            h599034    false    232            �           2606    700069    Bruker Bruker_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY "ProsjektIT"."Bruker"
    ADD CONSTRAINT "Bruker_pkey" PRIMARY KEY (kortid);
 F   ALTER TABLE ONLY "ProsjektIT"."Bruker" DROP CONSTRAINT "Bruker_pkey";
    
   ProsjektIT            h599034    false    232            �           2606    700079    Innpassering Innpassering_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY "ProsjektIT"."Innpassering"
    ADD CONSTRAINT "Innpassering_pkey" PRIMARY KEY (ident);
 R   ALTER TABLE ONLY "ProsjektIT"."Innpassering" DROP CONSTRAINT "Innpassering_pkey";
    
   ProsjektIT            h599034    false    234            �           2606    700094    Kortleser Kortleser_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY "ProsjektIT"."Kortleser"
    ADD CONSTRAINT "Kortleser_pkey" PRIMARY KEY (nummer);
 L   ALTER TABLE ONLY "ProsjektIT"."Kortleser" DROP CONSTRAINT "Kortleser_pkey";
    
   ProsjektIT            h599034    false    235            �           2606    700103    Alarm Alarm_kortid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY "ProsjektIT"."Alarm"
    ADD CONSTRAINT "Alarm_kortid_fkey" FOREIGN KEY (kortid) REFERENCES public.bruker(kortid) ON UPDATE CASCADE ON DELETE RESTRICT;
 K   ALTER TABLE ONLY "ProsjektIT"."Alarm" DROP CONSTRAINT "Alarm_kortid_fkey";
    
   ProsjektIT          h599034    false    237            �           2606    700108    Alarm Alarm_nummer_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY "ProsjektIT"."Alarm"
    ADD CONSTRAINT "Alarm_nummer_fkey" FOREIGN KEY (nummer) REFERENCES public.kortleser(nummer) ON UPDATE CASCADE ON DELETE RESTRICT;
 K   ALTER TABLE ONLY "ProsjektIT"."Alarm" DROP CONSTRAINT "Alarm_nummer_fkey";
    
   ProsjektIT          h599034    false    237            �           2606    700080 %   Innpassering Innpassering_kortid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY "ProsjektIT"."Innpassering"
    ADD CONSTRAINT "Innpassering_kortid_fkey" FOREIGN KEY (kortid) REFERENCES public.bruker(kortid) ON UPDATE CASCADE ON DELETE RESTRICT;
 Y   ALTER TABLE ONLY "ProsjektIT"."Innpassering" DROP CONSTRAINT "Innpassering_kortid_fkey";
    
   ProsjektIT          h599034    false    234            �           2606    700085 "   Innpassering Innpassering_pin_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY "ProsjektIT"."Innpassering"
    ADD CONSTRAINT "Innpassering_pin_fkey" FOREIGN KEY (pin) REFERENCES public.bruker(kortid) ON UPDATE CASCADE ON DELETE RESTRICT;
 V   ALTER TABLE ONLY "ProsjektIT"."Innpassering" DROP CONSTRAINT "Innpassering_pin_fkey";
    
   ProsjektIT          h599034    false    234            F      x������ � �      A      x������ � �      C      x������ � �      D      x������ � �     