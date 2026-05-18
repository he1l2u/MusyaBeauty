using BeautySalonApp.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeautySalonApp.Migrations;

[DbContext(typeof(SalonDbContext))]
[Migration("20260514000000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clients",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FullName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Masters",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FullName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Specialization = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Masters", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Services",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Category = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                DurationMinutes = table.Column<int>(type: "integer", nullable: false),
                Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Services", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Appointments",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                AppointmentDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                ClientId = table.Column<int>(type: "integer", nullable: false),
                MasterId = table.Column<int>(type: "integer", nullable: false),
                TotalPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false, defaultValue: "Новая")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Appointments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Appointments_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Appointments_Masters_MasterId",
                    column: x => x.MasterId,
                    principalTable: "Masters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "AppointmentServices",
            columns: table => new
            {
                AppointmentId = table.Column<int>(type: "integer", nullable: false),
                ServiceId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppointmentServices", x => new { x.AppointmentId, x.ServiceId });
                table.ForeignKey(
                    name: "FK_AppointmentServices_Appointments_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AppointmentServices_Services_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Services",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AppointmentServices_ServiceId",
            table: "AppointmentServices",
            column: "ServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_ClientId",
            table: "Appointments",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_MasterId_AppointmentDateTime",
            table: "Appointments",
            columns: new[] { "MasterId", "AppointmentDateTime" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Clients_Email",
            table: "Clients",
            column: "Email",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "AppointmentServices");
        migrationBuilder.DropTable(name: "Appointments");
        migrationBuilder.DropTable(name: "Services");
        migrationBuilder.DropTable(name: "Clients");
        migrationBuilder.DropTable(name: "Masters");
    }
}
