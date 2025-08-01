﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QuizApp.Contexts;

#nullable disable

namespace QuizApp.Migrations
{
    [DbContext(typeof(QuizAppContext))]
    [Migration("20250608171017_feildsUpdate")]
    partial class feildsUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QuizApp.Models.CompletedQuiz", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalScore")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_CompletedQuizzes");

                    b.HasIndex("StudentId");

                    b.ToTable("CompletedQuizzes");
                });

            modelBuilder.Entity("QuizApp.Models.Option", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OptionText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isCorrect")
                        .HasColumnType("boolean");

                    b.HasKey("Id")
                        .HasName("PK_Options");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("QuizApp.Models.Question", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Mark")
                        .HasColumnType("integer");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QuizId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PK_Questions");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizApp.Models.Quiz", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalMarks")
                        .HasColumnType("integer");

                    b.Property<string>("UploadedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PK_Quizzes");

                    b.HasIndex("UploadedBy");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("QuizApp.Models.Student", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HighestQualification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PK_Students");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("QuizApp.Models.Teacher", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PK_Teachers");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("QuizApp.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email")
                        .HasName("PK_Users");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QuizApp.Models.CompletedQuiz", b =>
                {
                    b.HasOne("QuizApp.Models.Student", "Student")
                        .WithMany("CompletedQuizzes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CompletedQuiz_Student");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("QuizApp.Models.Option", b =>
                {
                    b.HasOne("QuizApp.Models.Question", "question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Option_Question");

                    b.Navigation("question");
                });

            modelBuilder.Entity("QuizApp.Models.Question", b =>
                {
                    b.HasOne("QuizApp.Models.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Question_Quiz");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("QuizApp.Models.Quiz", b =>
                {
                    b.HasOne("QuizApp.Models.Teacher", "Teacher")
                        .WithMany("Quizzes")
                        .HasForeignKey("UploadedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Quiz_Teacher");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("QuizApp.Models.Student", b =>
                {
                    b.HasOne("QuizApp.Models.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("QuizApp.Models.Student", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Student_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuizApp.Models.Teacher", b =>
                {
                    b.HasOne("QuizApp.Models.User", "User")
                        .WithOne("Teacher")
                        .HasForeignKey("QuizApp.Models.Teacher", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Teacher_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuizApp.Models.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("QuizApp.Models.Quiz", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QuizApp.Models.Student", b =>
                {
                    b.Navigation("CompletedQuizzes");
                });

            modelBuilder.Entity("QuizApp.Models.Teacher", b =>
                {
                    b.Navigation("Quizzes");
                });

            modelBuilder.Entity("QuizApp.Models.User", b =>
                {
                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
