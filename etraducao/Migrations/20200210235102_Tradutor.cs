using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace etraducao.Migrations
{
    public partial class Tradutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TradutorId",
                table: "Solicitacao",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tradutor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tradutor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_TradutorId",
                table: "Solicitacao",
                column: "TradutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Tradutor_TradutorId",
                table: "Solicitacao",
                column: "TradutorId",
                principalTable: "Tradutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Tradutor_TradutorId",
                table: "Solicitacao");

            migrationBuilder.DropTable(
                name: "Tradutor");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacao_TradutorId",
                table: "Solicitacao");

            migrationBuilder.DropColumn(
                name: "TradutorId",
                table: "Solicitacao");
        }
    }
}
