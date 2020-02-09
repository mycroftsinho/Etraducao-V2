using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace etraducao.Migrations
{
    public partial class InitContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdExterno = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Cep = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControleDeValores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorDaLauda = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleDeValores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<Guid>(nullable: false),
                    PagamentoId = table.Column<Guid>(nullable: false),
                    IdiomaDestino = table.Column<string>(nullable: true),
                    IdiomaOrigem = table.Column<string>(nullable: true),
                    ValorTotal = table.Column<decimal>(nullable: false),
                    ValorPorLauda = table.Column<decimal>(nullable: false),
                    ValorDoReconhecimentoDeFirma = table.Column<decimal>(nullable: false),
                    ValorDaApostilaDeHaia = table.Column<decimal>(nullable: false),
                    DataDaEntrega = table.Column<DateTime>(nullable: false),
                    QuantidadeDeLaudas = table.Column<decimal>(nullable: false),
                    ApostilaDeHaia = table.Column<bool>(nullable: false),
                    QuantidadeDeDocumentosParaApostilar = table.Column<int>(nullable: false),
                    QuantidadeDeDocumentosParaReconhecerFirma = table.Column<int>(nullable: false),
                    TipoDeSolicitacao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SolicitacaoId = table.Column<int>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    Arquivo = table.Column<byte[]>(nullable: true),
                    QuantidadeDeLaudas = table.Column<decimal>(nullable: false),
                    QuantidadeDeCaracteres = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SolicitacaoId = table.Column<int>(nullable: false),
                    FormaDePagamento = table.Column<string>(nullable: false),
                    DataDeVencimento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    InvoiceUrl = table.Column<string>(nullable: true),
                    BankSlipUrl = table.Column<string>(nullable: true),
                    DevePostarEncomenda = table.Column<bool>(nullable: false),
                    StatusDePagamento = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamento_Solicitacao_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "Unique Email",
                table: "Cliente",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_SolicitacaoId",
                table: "Documento",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_SolicitacaoId",
                table: "Pagamento",
                column: "SolicitacaoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_ClienteId",
                table: "Solicitacao",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleDeValores");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
