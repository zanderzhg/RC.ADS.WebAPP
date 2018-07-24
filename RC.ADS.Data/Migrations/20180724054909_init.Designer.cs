﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RC.ADS.Data;

namespace RC.ADS.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180724054909_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Article.Article", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleContent");

                    b.Property<string>("ArticleIco");

                    b.Property<string>("ArticleImage");

                    b.Property<int>("ArticleIndex");

                    b.Property<string>("ArticleName");

                    b.Property<string>("ArticleTypeEntityId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleTypeEntityId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Article.ArticleType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Describe");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ArticleTypes");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Article.Article", b =>
                {
                    b.HasOne("RC.ADS.Data.Entity.AD_Article.ArticleType", "ArticleTypeEntity")
                        .WithMany()
                        .HasForeignKey("ArticleTypeEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
