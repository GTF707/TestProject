PGDMP         )    
    	        {            Metrix    14.2    14.2 
    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    78361    Metrix    DATABASE     e   CREATE DATABASE "Metrix" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "Metrix";
                postgres    false            �            1259    78402    disk_spaces    TABLE     �  CREATE TABLE public.disk_spaces (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    ip_address character varying(250) NOT NULL,
    disk_name character varying(250),
    total double precision,
    free double precision,
    is_deleted boolean,
    create_date timestamp without time zone,
    update_date timestamp without time zone,
    delete_date timestamp without time zone
);
    DROP TABLE public.disk_spaces;
       public         heap    postgres    false            �            1259    78388    metrix    TABLE     n  CREATE TABLE public.metrix (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    ip_address character varying(250),
    cpu double precision,
    ram_total double precision,
    ram_free double precision,
    is_deleted boolean,
    create_date timestamp without time zone,
    update_date timestamp without time zone,
    delete_date timestamp without time zone
);
    DROP TABLE public.metrix;
       public         heap    postgres    false            �          0    78402    disk_spaces 
   TABLE DATA           �   COPY public.disk_spaces (id, ip_address, disk_name, total, free, is_deleted, create_date, update_date, delete_date) FROM stdin;
    public          postgres    false    210   �       �          0    78388    metrix 
   TABLE DATA           }   COPY public.metrix (id, ip_address, cpu, ram_total, ram_free, is_deleted, create_date, update_date, delete_date) FROM stdin;
    public          postgres    false    209   �       d           2606    78409    disk_spaces disk_spaces_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.disk_spaces
    ADD CONSTRAINT disk_spaces_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.disk_spaces DROP CONSTRAINT disk_spaces_pkey;
       public            postgres    false    210            b           2606    78393    metrix metrics_pkey 
   CONSTRAINT     Q   ALTER TABLE ONLY public.metrix
    ADD CONSTRAINT metrics_pkey PRIMARY KEY (id);
 =   ALTER TABLE ONLY public.metrix DROP CONSTRAINT metrics_pkey;
       public            postgres    false    209            �      x������ � �      �      x������ � �     