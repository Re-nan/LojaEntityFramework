using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

/* Migrations
 * É uma forma de criar diferentes versões do banco de dados assim podemos facilmente mudar de uma
 * versão para outra, toda vez que criamos uma migração é gerado uma classe que herda de Migration
 * com dois métodos, o primeiro é chamado Up e o segundo Down, no método Up é definido quais as 
 * alterações serão executadas no banco e o Down como desfazer essas operações.
 * 
 * Ao utilizar migrations conseguimos controlar a versão do nosso banco de dados além de conseguir 
 * desfazer de forma fácil qualquer alteração. Mantendo um histórico de como o banco de dados evoluiu
 * e quais alterações foram executadas.
 */

namespace LojaEntityFramework.Migrations
{
    /* Cria a classe com o nome da minha migração e extende a Migration para ela */
public partial class CriaTbUsuario : Migration
    {
        /* O metodo Up da um create table com id, nome e senha */
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                });
        }

        /* O Down dropa a tabela */
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Usuario");
        }
    }
}
