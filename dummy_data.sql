--
-- PostgreSQL database dump
--

-- Dumped from database version 10.5
-- Dumped by pg_dump version 12.0

-- Started on 2020-02-13 20:06:49 CST

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

--
-- TOC entry 3195 (class 0 OID 27026)
-- Dependencies: 197
-- Data for Name: Categories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Categories" (id, name, description, year) FROM stdin;
2	WaterFalls	Hooved and dangerous.	2019
1	Jump Jump	Terrible to behold.	2019
3	Tag Team	\N	2019
4	CAT-e-Gory	\N	2019
5	Wild Card	\N	2019
\.


--
-- TOC entry 3197 (class 0 OID 27037)
-- Dependencies: 199
-- Data for Name: Animals; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Animals" (id, category_id, name, initial_seed) FROM stdin;
1	1	Bengal Tiger	1
4	1	Bharal	4
2	1	Markhor	2
3	1	Spinner Dolphin	3
5	1	Serval	5
6	1	Impala	6
7	1	Sifaka	7
8	1	Springhare	8
9	1	Jackrabbit	9
10	1	9-Banded Armadillo	10
11	1	Klipspringer	11
12	1	Rock Wallaby	12
13	1	Stoat	13
14	1	Spinifex Hopping Mouse	14
15	1	Streaked Tenrec	15
16	1	Ringtail Cat	16
85	2	Moose	1
86	2	Manatee	2
87	2	Lowland Tapir	3
88	2	White-lipped Peccary	4
89	2	Beaver	5
90	2	Crab-eating Fox	6
91	2	Marine Otter	7
92	2	Flat-headed Cat	8
93	2	Water Chevrotain	9
94	2	Aquatic Genet	10
95	2	Moonrat	11
96	2	Mink	12
97	2	Rakali	13
98	2	Water Opossum	14
99	2	Vontsira	15
100	2	Bulldog Bat	16
101	3	Zebra & Oxpecker	1
102	3	Coyote & Badger	2
103	3	Banded Mongoose & Warthog	3
104	3	Wrasse & Moray Eel	4
105	3	Crab & Sea Turtle	5
106	3	Diana Monkey & Red Colobus	6
107	3	Wattled Jacana & Capybara	7
108	3	Aardvark & Aardvark Cucumber	8
109	3	Goeldi's Monkey & Saddleback Tamarin & White-lipped Tamarin	9
110	3	Fork-tailed Drongos & Sociable Weaver	10
111	3	Bornean Bat & Pitcher Plant	11
112	3	Clown Fish & Sea Anemone	12
113	3	Fire Coral & Algae	13
114	3	Burying Beetles & Phoretic Mites	14
115	3	Ants & Aphids	15
116	3	Batfly & Gammaproteobacteria	16
117	4	Sea Lion	1
118	4	Nimravid	2
119	4	Bearcat	3
120	4	Fisher Cat	4
121	4	Tiger Owl	5
122	4	Lion-Tailed Macaque	6
123	4	Tiger Quoll	7
124	4	Small Spotted Cat Shark	8
125	4	Stonecat	9
126	4	Green Catbird	10
127	4	Cat Snake	11
128	4	Panther Chameleon	12
129	4	Tiger Salamander	13
130	4	Leopard Frog	14
131	4	Dandelion	15
132	4	Wild Card Winner	16
133	5	Antlion	16
134	5	Tiger Beetle	16
\.


--
-- TOC entry 3199 (class 0 OID 27048)
-- Dependencies: 201
-- Data for Name: Battles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Battles" (id, "timestamp", location, round_number) FROM stdin;
1	2019-04-22 14:31:42.533946-05	Beach	1
3	2019-07-08 16:18:41.601641-05	Forest Edge	1
\.


--
-- TOC entry 3201 (class 0 OID 27059)
-- Dependencies: 203
-- Data for Name: Participants; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Participants" (id, animal_id, battle_id, is_winner) FROM stdin;
3	2	3	t
1	3	1	f
4	1	3	f
2	4	1	t
\.


--
-- TOC entry 3204 (class 0 OID 27077)
-- Dependencies: 206
-- Data for Name: Bracket_Picks; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Bracket_Picks" (user_id, winner_id, id) FROM stdin;
\.


--
-- TOC entry 3211 (class 0 OID 0)
-- Dependencies: 198
-- Name: Animals_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Animals_id_seq"', 134, true);


--
-- TOC entry 3212 (class 0 OID 0)
-- Dependencies: 200
-- Name: Battles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Battles_id_seq"', 3, true);


--
-- TOC entry 3213 (class 0 OID 0)
-- Dependencies: 196
-- Name: Categories_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Categories_id_seq"', 5, true);


--
-- TOC entry 3214 (class 0 OID 0)
-- Dependencies: 202
-- Name: Participants_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Participants_id_seq"', 4, true);


--
-- TOC entry 3215 (class 0 OID 0)
-- Dependencies: 204
-- Name: Users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Users_id_seq"', 1, true);


--
-- TOC entry 3216 (class 0 OID 0)
-- Dependencies: 207
-- Name: bracket_picks_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bracket_picks_seq', 1, false);


-- Completed on 2020-02-13 20:06:49 CST

--
-- PostgreSQL database dump complete
--

