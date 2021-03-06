﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RC.ADS.Data;

namespace RC.ADS.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180816152404_addfiledlastlo")]
    partial class addfiledlastlo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Account.AccountInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountInfoChangeTpyeId");

                    b.Property<decimal>("AfterMoney");

                    b.Property<decimal>("BeforeMoney");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Describe");

                    b.Property<decimal>("Money");

                    b.Property<string>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("AccountInfoChangeTpyeId");

                    b.HasIndex("OwnerId");

                    b.ToTable("AccountInfos");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Account.AccountInfoChangeType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Describe");

                    b.Property<string>("Name");

                    b.Property<int>("PlusOrMinus");

                    b.HasKey("Id");

                    b.ToTable("AccountInfoChangeTpyes");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Article.Article", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleContent");

                    b.Property<string>("ArticleIco");

                    b.Property<string>("ArticleImage");

                    b.Property<int>("ArticleIndex");

                    b.Property<string>("ArticleName");

                    b.Property<string>("ArticleTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleTypeId");

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

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Integral.IntegralInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AfterScore");

                    b.Property<int>("BeforeScore");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Describe");

                    b.Property<string>("IntegralInfoChangeTypeId");

                    b.Property<string>("OwnerId");

                    b.Property<int>("Score");

                    b.HasKey("Id");

                    b.HasIndex("IntegralInfoChangeTypeId");

                    b.HasIndex("OwnerId");

                    b.ToTable("IntegralInfos");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Integral.IntegralInfoChangeType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Describe");

                    b.Property<string>("Name");

                    b.Property<int>("PlusOrMinus");

                    b.HasKey("Id");

                    b.ToTable("IntegralInfoChangeType");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Menber.Menber", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AccountSum");

                    b.Property<int>("IntegralSum");

                    b.Property<string>("LastLoginGuidCode");

                    b.Property<string>("ManberName");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("ReferrerId");

                    b.Property<DateTime>("RegisterTime");

                    b.HasKey("Id");

                    b.HasIndex("ReferrerId");

                    b.ToTable("Menbers");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Order.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdateTime");

                    b.Property<string>("OrderName");

                    b.Property<string>("OrderStatusId");

                    b.Property<string>("OwnerId");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Order.OrderStatus", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChineseName");

                    b.Property<string>("Describe");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Order.OrderStatusChange", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<string>("OrderId");

                    b.Property<string>("OrderStatusId");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("OrderStatusChanges");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_SMS.SendSMSLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Appid");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("Month");

                    b.Property<int>("QuantityAfterSend");

                    b.Property<int>("QuantityBeforeSend");

                    b.Property<string>("SMSAppTemplateId");

                    b.Property<int>("Year");

                    b.Property<string>("phoneNumbers");

                    b.HasKey("Id");

                    b.ToTable("SendSMSLogs");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_SMS.SMSApp", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Describe");

                    b.Property<string>("SMSAppName");

                    b.Property<string>("appid");

                    b.Property<string>("appkey");

                    b.HasKey("Id");

                    b.ToTable("SMSApp");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_SMS.SMSAppTemplate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppName");

                    b.Property<string>("Appid");

                    b.Property<string>("Appkey");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Describe");

                    b.Property<string>("SMSAppTemplateName");

                    b.Property<string>("TemplateId");

                    b.Property<int>("TemplateType");

                    b.HasKey("Id");

                    b.ToTable("SMSAppTemplates");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Account.AccountInfo", b =>
                {
                    b.HasOne("RC.ADS.Data.Entity.AD_Account.AccountInfoChangeType", "AccountInfoChangeTpye")
                        .WithMany()
                        .HasForeignKey("AccountInfoChangeTpyeId");

                    b.HasOne("RC.ADS.Data.Entity.AD_Menber.Menber", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Article.Article", b =>
                {
                    b.HasOne("RC.ADS.Data.Entity.AD_Article.ArticleType", "ArticleTypeEntity")
                        .WithMany()
                        .HasForeignKey("ArticleTypeId");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Integral.IntegralInfo", b =>
                {
                    b.HasOne("RC.ADS.Data.Entity.AD_Integral.IntegralInfoChangeType", "IntegralInfoChangeType")
                        .WithMany()
                        .HasForeignKey("IntegralInfoChangeTypeId");

                    b.HasOne("RC.ADS.Data.Entity.AD_Menber.Menber", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Menber.Menber", b =>
                {
                    b.HasOne("RC.ADS.Data.Entity.AD_Menber.Menber", "Referrer")
                        .WithMany()
                        .HasForeignKey("ReferrerId");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Order.Order", b =>
                {
                    b.HasOne("RC.ADS.Data.Entity.AD_Order.OrderStatus", "orderStatu")
                        .WithMany()
                        .HasForeignKey("OrderStatusId");

                    b.HasOne("RC.ADS.Data.Entity.AD_Menber.Menber", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("RC.ADS.Data.Entity.AD_Order.OrderStatusChange", b =>
                {
                    b.HasOne("RC.ADS.Data.Entity.AD_Order.Order", "OrderEntity")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("RC.ADS.Data.Entity.AD_Order.OrderStatus", "orderStatu")
                        .WithMany()
                        .HasForeignKey("OrderStatusId");
                });
#pragma warning restore 612, 618
        }
    }
}
