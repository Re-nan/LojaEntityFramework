using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

/* Migrations
 * � uma forma de criar diferentes vers�es do banco de dados assim podemos facilmente mudar de uma
 * vers�o para outra, toda vez que criamos uma migra��o � gerado uma classe que herda de Migration
 * com dois m�todos, o primeiro � chamado Up e o segundo Down, no m�todo Up � definido quais as 
 * altera��es ser�o executadas no banco e o Down como desfazer essas opera��es.
 * 
 * Ao utilizar migrations conseguimos controlar a vers�o do nosso banco de dados al�m de conseguir 
 * desfazer de forma f�cil qualquer altera��o. Mantendo um hist�rico de como o banco de dados evoluiu
 * e quais altera��es foram executadas.
 */

namespace LojaEntityFramework.Migrations
{
    /* Cria a classe com o nome da minha migra��o e extende a Migration para ela */
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
