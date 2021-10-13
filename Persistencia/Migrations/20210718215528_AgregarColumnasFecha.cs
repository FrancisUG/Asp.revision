using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class AgregarColumnasFecha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Instructor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Curso",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Comentario",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Precio_CursoId",
                table: "Precio",
                column: "CursoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CursoInstructor_CursoId",
                table: "CursoInstructor",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_CursoId",
                table: "Comentario",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Curso_CursoId",
                table: "Comentario",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CursoInstructor_Curso_CursoId",
                table: "CursoInstructor",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Precio_Curso_CursoId",
                table: "Precio",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Curso_CursoId",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_CursoInstructor_Curso_CursoId",
                table: "CursoInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Precio_Curso_CursoId",
                table: "Precio");

            migrationBuilder.DropIndex(
                name: "IX_Precio_CursoId",
                table: "Precio");

            migrationBuilder.DropIndex(
                name: "IX_CursoInstructor_CursoId",
                table: "CursoInstructor");

            migrationBuilder.DropIndex(
                name: "IX_Comentario_CursoId",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Comentario");
        }
    }
}
