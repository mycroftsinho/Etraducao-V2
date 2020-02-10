using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace etraducao.Migrations
{
    public partial class DataDaSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDaSolicitacao",
                table: "Solicitacao",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDaSolicitacao",
                table: "Solicitacao");
        }
    }
}
