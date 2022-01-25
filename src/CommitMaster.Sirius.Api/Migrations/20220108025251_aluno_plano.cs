using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommitMaster.Sirius.Api.Migrations
{
    public partial class aluno_plano : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Duracao = table.Column<int>(type: "integer", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planos");
        }
    }
}
