--
-- PostgreSQL database dump
--

-- Dumped from database version 10.5
-- Dumped by pg_dump version 12.0

-- Started on 2020-02-13 19:36:06 CST

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

--
-- TOC entry 199 (class 1259 OID 27037)
-- Name: Animals; Type: TABLE; Schema: public; Owner: Christopher
--

CREATE TABLE public."Animals" (
    id integer NOT NULL,
    category_id integer NOT NULL,
    name text NOT NULL,
    initial_seed integer NOT NULL
);


ALTER TABLE public."Animals" OWNER TO "Christopher";

--
-- TOC entry 198 (class 1259 OID 27035)
-- Name: Animals_id_seq; Type: SEQUENCE; Schema: public; Owner: Christopher
--

CREATE SEQUENCE public."Animals_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Animals_id_seq" OWNER TO "Christopher";

--
-- TOC entry 3199 (class 0 OID 0)
-- Dependencies: 198
-- Name: Animals_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: Christopher
--

ALTER SEQUENCE public."Animals_id_seq" OWNED BY public."Animals".id;


--
-- TOC entry 201 (class 1259 OID 27048)
-- Name: Battles; Type: TABLE; Schema: public; Owner: Christopher
--

CREATE TABLE public."Battles" (
    id integer NOT NULL,
    "timestamp" timestamp with time zone NOT NULL,
    location text NOT NULL,
    round_number integer NOT NULL
);


ALTER TABLE public."Battles" OWNER TO "Christopher";

--
-- TOC entry 200 (class 1259 OID 27046)
-- Name: Battles_id_seq; Type: SEQUENCE; Schema: public; Owner: Christopher
--

CREATE SEQUENCE public."Battles_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Battles_id_seq" OWNER TO "Christopher";

--
-- TOC entry 3200 (class 0 OID 0)
-- Dependencies: 200
-- Name: Battles_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: Christopher
--

ALTER SEQUENCE public."Battles_id_seq" OWNED BY public."Battles".id;


--
-- TOC entry 207 (class 1259 OID 27107)
-- Name: bracket_picks_seq; Type: SEQUENCE; Schema: public; Owner: Christopher
--

CREATE SEQUENCE public.bracket_picks_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.bracket_picks_seq OWNER TO "Christopher";

--
-- TOC entry 206 (class 1259 OID 27077)
-- Name: Bracket_Picks; Type: TABLE; Schema: public; Owner: Christopher
--

CREATE TABLE public."Bracket_Picks" (
    user_id integer NOT NULL,
    winner_id integer NOT NULL,
    id integer DEFAULT nextval('public.bracket_picks_seq'::regclass) NOT NULL
);


ALTER TABLE public."Bracket_Picks" OWNER TO "Christopher";

--
-- TOC entry 197 (class 1259 OID 27026)
-- Name: Categories; Type: TABLE; Schema: public; Owner: Christopher
--

CREATE TABLE public."Categories" (
    id integer NOT NULL,
    name text NOT NULL,
    description text,
    year integer NOT NULL
);


ALTER TABLE public."Categories" OWNER TO "Christopher";

--
-- TOC entry 196 (class 1259 OID 27024)
-- Name: Categories_id_seq; Type: SEQUENCE; Schema: public; Owner: Christopher
--

CREATE SEQUENCE public."Categories_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Categories_id_seq" OWNER TO "Christopher";

--
-- TOC entry 3201 (class 0 OID 0)
-- Dependencies: 196
-- Name: Categories_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: Christopher
--

ALTER SEQUENCE public."Categories_id_seq" OWNED BY public."Categories".id;


--
-- TOC entry 203 (class 1259 OID 27059)
-- Name: Participants; Type: TABLE; Schema: public; Owner: Christopher
--

CREATE TABLE public."Participants" (
    id integer NOT NULL,
    animal_id integer NOT NULL,
    battle_id integer NOT NULL,
    is_winner boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Participants" OWNER TO "Christopher";

--
-- TOC entry 202 (class 1259 OID 27057)
-- Name: Participants_id_seq; Type: SEQUENCE; Schema: public; Owner: Christopher
--

CREATE SEQUENCE public."Participants_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Participants_id_seq" OWNER TO "Christopher";

--
-- TOC entry 3202 (class 0 OID 0)
-- Dependencies: 202
-- Name: Participants_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: Christopher
--

ALTER SEQUENCE public."Participants_id_seq" OWNED BY public."Participants".id;


--
-- TOC entry 205 (class 1259 OID 27068)
-- Name: Users; Type: TABLE; Schema: public; Owner: Christopher
--

CREATE TABLE public."Users" (
    id integer NOT NULL,
    username text NOT NULL,
    password text NOT NULL,
    email text NOT NULL,
    first_name text DEFAULT '"Pardner"'::text NOT NULL,
    is_admin text DEFAULT 'false'::text NOT NULL,
    refresh_token text DEFAULT 'NULL'::text
);


ALTER TABLE public."Users" OWNER TO "Christopher";

--
-- TOC entry 204 (class 1259 OID 27066)
-- Name: Users_id_seq; Type: SEQUENCE; Schema: public; Owner: Christopher
--

CREATE SEQUENCE public."Users_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Users_id_seq" OWNER TO "Christopher";

--
-- TOC entry 3203 (class 0 OID 0)
-- Dependencies: 204
-- Name: Users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: Christopher
--

ALTER SEQUENCE public."Users_id_seq" OWNED BY public."Users".id;


--
-- TOC entry 3047 (class 2604 OID 27040)
-- Name: Animals id; Type: DEFAULT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Animals" ALTER COLUMN id SET DEFAULT nextval('public."Animals_id_seq"'::regclass);


--
-- TOC entry 3048 (class 2604 OID 27051)
-- Name: Battles id; Type: DEFAULT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Battles" ALTER COLUMN id SET DEFAULT nextval('public."Battles_id_seq"'::regclass);


--
-- TOC entry 3046 (class 2604 OID 27029)
-- Name: Categories id; Type: DEFAULT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Categories" ALTER COLUMN id SET DEFAULT nextval('public."Categories_id_seq"'::regclass);


--
-- TOC entry 3049 (class 2604 OID 27062)
-- Name: Participants id; Type: DEFAULT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Participants" ALTER COLUMN id SET DEFAULT nextval('public."Participants_id_seq"'::regclass);


--
-- TOC entry 3051 (class 2604 OID 27071)
-- Name: Users id; Type: DEFAULT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Users" ALTER COLUMN id SET DEFAULT nextval('public."Users_id_seq"'::regclass);


--
-- TOC entry 3067 (class 2606 OID 27106)
-- Name: Bracket_Picks Bracket_Picks_pkey; Type: CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Bracket_Picks"
    ADD CONSTRAINT "Bracket_Picks_pkey" PRIMARY KEY (id);


--
-- TOC entry 3059 (class 2606 OID 27045)
-- Name: Animals animals_pk; Type: CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Animals"
    ADD CONSTRAINT animals_pk PRIMARY KEY (id);


--
-- TOC entry 3061 (class 2606 OID 27056)
-- Name: Battles battles_pk; Type: CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Battles"
    ADD CONSTRAINT battles_pk PRIMARY KEY (id);


--
-- TOC entry 3057 (class 2606 OID 27034)
-- Name: Categories categories_pk; Type: CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT categories_pk PRIMARY KEY (id);


--
-- TOC entry 3063 (class 2606 OID 27065)
-- Name: Participants participants_pk; Type: CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Participants"
    ADD CONSTRAINT participants_pk PRIMARY KEY (id);


--
-- TOC entry 3065 (class 2606 OID 27076)
-- Name: Users users_pk; Type: CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT users_pk PRIMARY KEY (id);


--
-- TOC entry 3068 (class 2606 OID 27080)
-- Name: Animals Animals_fk0; Type: FK CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Animals"
    ADD CONSTRAINT "Animals_fk0" FOREIGN KEY (category_id) REFERENCES public."Categories"(id);


--
-- TOC entry 3071 (class 2606 OID 27095)
-- Name: Bracket_Picks Bracket_Picks_fk0; Type: FK CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Bracket_Picks"
    ADD CONSTRAINT "Bracket_Picks_fk0" FOREIGN KEY (user_id) REFERENCES public."Users"(id);


--
-- TOC entry 3072 (class 2606 OID 27100)
-- Name: Bracket_Picks Bracket_Picks_fk1; Type: FK CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Bracket_Picks"
    ADD CONSTRAINT "Bracket_Picks_fk1" FOREIGN KEY (winner_id) REFERENCES public."Participants"(id);


--
-- TOC entry 3069 (class 2606 OID 27085)
-- Name: Participants Participants_fk0; Type: FK CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Participants"
    ADD CONSTRAINT "Participants_fk0" FOREIGN KEY (animal_id) REFERENCES public."Animals"(id);


--
-- TOC entry 3070 (class 2606 OID 27090)
-- Name: Participants Participants_fk1; Type: FK CONSTRAINT; Schema: public; Owner: Christopher
--

ALTER TABLE ONLY public."Participants"
    ADD CONSTRAINT "Participants_fk1" FOREIGN KEY (battle_id) REFERENCES public."Battles"(id);


-- Completed on 2020-02-13 19:36:07 CST

--
-- PostgreSQL database dump complete
--

