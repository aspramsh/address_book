using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AddressBook.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_state", x => x.id);
                    table.ForeignKey(
                        name: "fk_state_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    time_zone = table.Column<string>(type: "text", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.id);
                    table.ForeignKey(
                        name: "fk_city_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_city_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "zip_code",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_zip_code", x => x.id);
                    table.ForeignKey(
                        name: "fk_zip_code_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_zip_code_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_zip_code_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    street = table.Column<string>(type: "text", nullable: true),
                    building = table.Column<int>(type: "integer", nullable: false),
                    appartment = table.Column<int>(type: "integer", nullable: false),
                    zip_code_id = table.Column<int>(type: "integer", nullable: false),
                    contact_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_address_zip_code_zip_code_id",
                        column: x => x.zip_code_id,
                        principalTable: "zip_code",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contact",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    address_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contact", x => x.id);
                    table.ForeignKey(
                        name: "fk_contact_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phone_number",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: false),
                    contact_id = table.Column<int>(type: "integer", nullable: false),
                    phone_type = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phone_number", x => x.id);
                    table.ForeignKey(
                        name: "fk_phone_number_contact_contact_id",
                        column: x => x.contact_id,
                        principalTable: "contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_address_zip_code_id",
                table: "address",
                column: "zip_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_country_id",
                table: "city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_state_id_name",
                table: "city",
                columns: new[] { "state_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contact_address_id",
                table: "contact",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_country_name_code",
                table: "country",
                columns: new[] { "name", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_phone_number_contact_id",
                table: "phone_number",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "ix_state_country_id",
                table: "state",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_state_name",
                table: "state",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_zip_code_city_id",
                table: "zip_code",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_zip_code_country_id_state_id_city_id_code",
                table: "zip_code",
                columns: new[] { "country_id", "state_id", "city_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_zip_code_state_id",
                table: "zip_code",
                column: "state_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "phone_number");

            migrationBuilder.DropTable(
                name: "contact");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "zip_code");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
