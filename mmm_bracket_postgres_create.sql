CREATE TABLE "Categories" (
	"id" serial,
	"name" varchar,
	"description" varchar,
	"year" integer,
	CONSTRAINT Categories_pk PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Animals" (
	"id" serial,
	"category_id" integer NOT NULL,
	"name" VARCHAR(255),
	"initial_seed" integer NOT NULL,
	CONSTRAINT Animals_pk PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Battles" (
	"id" serial,
	"datetime" DATETIME NOT NULL,
	"location" varchar NOT NULL,
	"round_number" integer NOT NULL,
	CONSTRAINT Battles_pk PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Participants" (
	"id" serial NOT NULL,
	"animal_id" integer NOT NULL,
	"battle_id" integer NOT NULL,
	"is_winner" BOOLEAN NOT NULL DEFAULT 'false',
	CONSTRAINT Participants_pk PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Users" (
	"id" serial NOT NULL,
	"username" varchar NOT NULL,
	"password" varchar NOT NULL,
	"email" varchar NOT NULL,
	"first_name" varchar NOT NULL,
	"isAdmin" varchar NOT NULL,
	CONSTRAINT Users_pk PRIMARY KEY ("id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Bracket_Picks" (
	"user_id" integer NOT NULL,
	"winner_id" integer NOT NULL
) WITH (
  OIDS=FALSE
);




ALTER TABLE "Animals" ADD CONSTRAINT "Animals_fk0" FOREIGN KEY ("category_id") REFERENCES "Categories"("id");


ALTER TABLE "Participants" ADD CONSTRAINT "Participants_fk0" FOREIGN KEY ("animal_id") REFERENCES "Animals"("id");
ALTER TABLE "Participants" ADD CONSTRAINT "Participants_fk1" FOREIGN KEY ("battle_id") REFERENCES "Battles"("id");


ALTER TABLE "Bracket_Picks" ADD CONSTRAINT "Bracket_Picks_fk0" FOREIGN KEY ("user_id") REFERENCES "Users"("id");
ALTER TABLE "Bracket_Picks" ADD CONSTRAINT "Bracket_Picks_fk1" FOREIGN KEY ("winner_id") REFERENCES "Participants"("id");

