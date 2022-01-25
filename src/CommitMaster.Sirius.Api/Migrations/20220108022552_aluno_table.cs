using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommitMaster.Sirius.Api.Migrations
{
    public partial class aluno_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataAniversario = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    cpf = table.Column<string>(type: "varchar(20)", nullable: true),
                    numero_telefone = table.Column<string>(type: "varchar(20)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");
        }
    }
}
