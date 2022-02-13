CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Planos" (
    "Id" uuid NOT NULL,
    "Nome" varchar(200) NULL,
    "Duracao" integer NOT NULL,
    "Preco" numeric NOT NULL,
    "Descricao" varchar(200) NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Planos" PRIMARY KEY ("Id")
);

CREATE TABLE "Assinaturas" (
    "Id" uuid NOT NULL,
    "DataExpiracao" timestamp without time zone NOT NULL,
    "EstadoAssinatura" integer NOT NULL,
    "Ativa" boolean NOT NULL,
    "AlunoId" uuid NOT NULL,
    "PlanoId" uuid NULL,
    "PlanId" uuid NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Assinaturas" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Assinaturas_Planos_PlanoId" FOREIGN KEY ("PlanoId") REFERENCES "Planos" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Alunos" (
    "Id" uuid NOT NULL,
    "Nome" varchar(200) NOT NULL,
    "Email" varchar(200) NOT NULL,
    "DataAniversario" timestamp without time zone NOT NULL,
    cpf varchar(200) NULL,
    numero_telefone varchar(200) NULL,
    "AssinaturaId" uuid NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Alunos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Alunos_Assinaturas_Id" FOREIGN KEY ("Id") REFERENCES "Assinaturas" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Usuarios" (
    "Id" uuid NOT NULL,
    "Email" varchar(200) NOT NULL,
    "Senha" varchar(200) NOT NULL,
    "DataExpiracao" timestamp without time zone NOT NULL,
    "AlunoId" uuid NOT NULL,
    "AssinaturaId" uuid NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Usuarios" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Usuarios_Alunos_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Alunos" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Usuarios_Assinaturas_AssinaturaId" FOREIGN KEY ("AssinaturaId") REFERENCES "Assinaturas" ("Id") ON DELETE RESTRICT
);

CREATE UNIQUE INDEX "IX_Alunos_Email" ON "Alunos" ("Email");

CREATE INDEX "IX_Assinaturas_PlanoId" ON "Assinaturas" ("PlanoId");

CREATE INDEX "IX_Usuarios_AlunoId" ON "Usuarios" ("AlunoId");

CREATE INDEX "IX_Usuarios_AssinaturaId" ON "Usuarios" ("AssinaturaId");

CREATE UNIQUE INDEX "IX_Usuarios_Email" ON "Usuarios" ("Email");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220206224856_tables', '5.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220213152318_ajuste_relacionamento', '5.0.13');

COMMIT;

