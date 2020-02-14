--
-- PostgreSQL database dump
--

-- Dumped from database version 10.5
-- Dumped by pg_dump version 12.0

-- Started on 2020-02-13 20:18:28 CST

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
-- Data for Name: Categories; Type: TABLE DATA; Schema: public; Owner: Christopher
--

INSERT INTO public."Categories" (id, name, description, year) VALUES (2, 'WaterFalls', 'Hooved and dangerous.', 2019);
INSERT INTO public."Categories" (id, name, description, year) VALUES (1, 'Jump Jump', 'Terrible to behold.', 2019);
INSERT INTO public."Categories" (id, name, description, year) VALUES (3, 'Tag Team', NULL, 2019);
INSERT INTO public."Categories" (id, name, description, year) VALUES (4, 'CAT-e-Gory', NULL, 2019);
INSERT INTO public."Categories" (id, name, description, year) VALUES (5, 'Wild Card', NULL, 2019);


--
-- TOC entry 3197 (class 0 OID 27037)
-- Dependencies: 199
-- Data for Name: Animals; Type: TABLE DATA; Schema: public; Owner: Christopher
--

INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (1, 1, 'Bengal Tiger', 1);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (4, 1, 'Bharal', 4);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (2, 1, 'Markhor', 2);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (3, 1, 'Spinner Dolphin', 3);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (5, 1, 'Serval', 5);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (6, 1, 'Impala', 6);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (7, 1, 'Sifaka', 7);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (8, 1, 'Springhare', 8);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (9, 1, 'Jackrabbit', 9);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (10, 1, '9-Banded Armadillo', 10);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (11, 1, 'Klipspringer', 11);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (12, 1, 'Rock Wallaby', 12);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (13, 1, 'Stoat', 13);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (14, 1, 'Spinifex Hopping Mouse', 14);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (15, 1, 'Streaked Tenrec', 15);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (16, 1, 'Ringtail Cat', 16);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (85, 2, 'Moose', 1);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (86, 2, 'Manatee', 2);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (87, 2, 'Lowland Tapir', 3);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (88, 2, 'White-lipped Peccary', 4);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (89, 2, 'Beaver', 5);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (90, 2, 'Crab-eating Fox', 6);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (91, 2, 'Marine Otter', 7);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (92, 2, 'Flat-headed Cat', 8);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (93, 2, 'Water Chevrotain', 9);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (94, 2, 'Aquatic Genet', 10);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (95, 2, 'Moonrat', 11);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (96, 2, 'Mink', 12);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (97, 2, 'Rakali', 13);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (98, 2, 'Water Opossum', 14);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (99, 2, 'Vontsira', 15);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (100, 2, 'Bulldog Bat', 16);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (101, 3, 'Zebra & Oxpecker', 1);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (102, 3, 'Coyote & Badger', 2);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (103, 3, 'Banded Mongoose & Warthog', 3);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (104, 3, 'Wrasse & Moray Eel', 4);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (105, 3, 'Crab & Sea Turtle', 5);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (106, 3, 'Diana Monkey & Red Colobus', 6);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (107, 3, 'Wattled Jacana & Capybara', 7);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (108, 3, 'Aardvark & Aardvark Cucumber', 8);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (109, 3, 'Goeldi''s Monkey & Saddleback Tamarin & White-lipped Tamarin', 9);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (110, 3, 'Fork-tailed Drongos & Sociable Weaver', 10);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (111, 3, 'Bornean Bat & Pitcher Plant', 11);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (112, 3, 'Clown Fish & Sea Anemone', 12);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (113, 3, 'Fire Coral & Algae', 13);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (114, 3, 'Burying Beetles & Phoretic Mites', 14);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (115, 3, 'Ants & Aphids', 15);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (116, 3, 'Batfly & Gammaproteobacteria', 16);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (117, 4, 'Sea Lion', 1);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (118, 4, 'Nimravid', 2);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (119, 4, 'Bearcat', 3);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (120, 4, 'Fisher Cat', 4);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (121, 4, 'Tiger Owl', 5);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (122, 4, 'Lion-Tailed Macaque', 6);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (123, 4, 'Tiger Quoll', 7);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (124, 4, 'Small Spotted Cat Shark', 8);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (125, 4, 'Stonecat', 9);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (126, 4, 'Green Catbird', 10);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (127, 4, 'Cat Snake', 11);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (128, 4, 'Panther Chameleon', 12);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (129, 4, 'Tiger Salamander', 13);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (130, 4, 'Leopard Frog', 14);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (131, 4, 'Dandelion', 15);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (132, 4, 'Wild Card Winner', 16);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (133, 5, 'Antlion', 16);
INSERT INTO public."Animals" (id, category_id, name, initial_seed) VALUES (134, 5, 'Tiger Beetle', 16);


--
-- TOC entry 3199 (class 0 OID 27048)
-- Dependencies: 201
-- Data for Name: Battles; Type: TABLE DATA; Schema: public; Owner: Christopher
--

INSERT INTO public."Battles" (id, "timestamp", location, round_number) VALUES (1, '2019-04-22 14:31:42.533946-05', 'Beach', 1);
INSERT INTO public."Battles" (id, "timestamp", location, round_number) VALUES (3, '2019-07-08 16:18:41.601641-05', 'Forest Edge', 1);


--
-- TOC entry 3201 (class 0 OID 27059)
-- Dependencies: 203
-- Data for Name: Participants; Type: TABLE DATA; Schema: public; Owner: Christopher
--

INSERT INTO public."Participants" (id, animal_id, battle_id, is_winner) VALUES (3, 2, 3, true);
INSERT INTO public."Participants" (id, animal_id, battle_id, is_winner) VALUES (1, 3, 1, false);
INSERT INTO public."Participants" (id, animal_id, battle_id, is_winner) VALUES (4, 1, 3, false);
INSERT INTO public."Participants" (id, animal_id, battle_id, is_winner) VALUES (2, 4, 1, true);


--
-- TOC entry 3211 (class 0 OID 0)
-- Dependencies: 198
-- Name: Animals_id_seq; Type: SEQUENCE SET; Schema: public; Owner: Christopher
--

SELECT pg_catalog.setval('public."Animals_id_seq"', 134, true);


--
-- TOC entry 3212 (class 0 OID 0)
-- Dependencies: 200
-- Name: Battles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: Christopher
--

SELECT pg_catalog.setval('public."Battles_id_seq"', 3, true);


--
-- TOC entry 3213 (class 0 OID 0)
-- Dependencies: 196
-- Name: Categories_id_seq; Type: SEQUENCE SET; Schema: public; Owner: Christopher
--

SELECT pg_catalog.setval('public."Categories_id_seq"', 5, true);


--
-- TOC entry 3214 (class 0 OID 0)
-- Dependencies: 202
-- Name: Participants_id_seq; Type: SEQUENCE SET; Schema: public; Owner: Christopher
--

SELECT pg_catalog.setval('public."Participants_id_seq"', 4, true);


--
-- TOC entry 3215 (class 0 OID 0)
-- Dependencies: 204
-- Name: Users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: Christopher
--

SELECT pg_catalog.setval('public."Users_id_seq"', 1, true);


--
-- TOC entry 3216 (class 0 OID 0)
-- Dependencies: 207
-- Name: bracket_picks_seq; Type: SEQUENCE SET; Schema: public; Owner: Christopher
--

SELECT pg_catalog.setval('public.bracket_picks_seq', 1, false);


-- Completed on 2020-02-13 20:18:28 CST

--
-- PostgreSQL database dump complete
--

