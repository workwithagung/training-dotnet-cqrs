using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PegawaiJabatanRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JabatanId",
                table: "hris_pegawai",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_hris_pegawai_JabatanId",
                table: "hris_pegawai",
                column: "JabatanId");

            migrationBuilder.AddForeignKey(
                name: "FK_hris_pegawai_Jabatans_JabatanId",
                table: "hris_pegawai",
                column: "JabatanId",
                principalTable: "Jabatans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hris_pegawai_Jabatans_JabatanId",
                table: "hris_pegawai");

            migrationBuilder.DropIndex(
                name: "IX_hris_pegawai_JabatanId",
                table: "hris_pegawai");

            migrationBuilder.DropColumn(
                name: "JabatanId",
                table: "hris_pegawai");
        }
    }
}
