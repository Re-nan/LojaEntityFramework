using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using LojaEntityFramework.EntidadesEntity;

namespace LojaEntityFramework.Migrations
{
    [DbContext(typeof(EntidadesContext))]
    [Migration("20171006183954_CriaTbProdutoVenda")]
    partial class CriaTbProdutoVenda
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.Categoria", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.Produto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoriaID");

                    b.Property<string>("Nome");

                    b.Property<decimal>("Preco");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.ProdutoVenda", b =>
                {
                    b.Property<int>("ProdutoID");

                    b.Property<int>("VendaID");

                    b.HasKey("ProdutoID", "VendaID");
                });

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.Venda", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClienteID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.Produto", b =>
                {
                    b.HasOne("LojaEntityFramework.EntidadesClass.Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID");
                });

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.ProdutoVenda", b =>
                {
                    b.HasOne("LojaEntityFramework.EntidadesClass.Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoID");

                    b.HasOne("LojaEntityFramework.EntidadesClass.Venda")
                        .WithMany()
                        .HasForeignKey("VendaID");
                });

            modelBuilder.Entity("LojaEntityFramework.EntidadesClass.Venda", b =>
                {
                    b.HasOne("LojaEntityFramework.EntidadesClass.Usuario")
                        .WithMany()
                        .HasForeignKey("ClienteID");
                });
        }
    }
}
