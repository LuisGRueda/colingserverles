using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coling.API.Afiliado.Migrations
{
    /// <inheritdoc />
    public partial class Migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradosAcademicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGrado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradosAcademicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreIdioma = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ci = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposEstudios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposEstudios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposInstituciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposInstituciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposSociales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSocial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposSociales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProfesion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdGrado = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profesiones_GradosAcademicos_IdGrado",
                        column: x => x.IdGrado,
                        principalTable: "GradosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Afiliados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    FechaAfiliacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoAfiliado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NroTituloProvisional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afiliados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Afiliados_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direcciones_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    NroTelefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefonos_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoInstitucion = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instituciones_TiposInstituciones_IdTipoInstitucion",
                        column: x => x.IdTipoInstitucion,
                        principalTable: "TiposInstituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonasTiposSociales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoSocial = table.Column<int>(type: "int", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasTiposSociales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonasTiposSociales_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonasTiposSociales_TiposSociales_IdTipoSocial",
                        column: x => x.IdTipoSocial,
                        principalTable: "TiposSociales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AfiliadosIdiomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAfiliado = table.Column<int>(type: "int", nullable: false),
                    IdIdioma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfiliadosIdiomas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AfiliadosIdiomas_Afiliados_IdAfiliado",
                        column: x => x.IdAfiliado,
                        principalTable: "Afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AfiliadosIdiomas_Idiomas_IdIdioma",
                        column: x => x.IdIdioma,
                        principalTable: "Idiomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfesionesAfiliados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAfiliado = table.Column<int>(type: "int", nullable: false),
                    IdProfesion = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NroSelloSIB = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesionesAfiliados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfesionesAfiliados_Afiliados_IdAfiliado",
                        column: x => x.IdAfiliado,
                        principalTable: "Afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesionesAfiliados_Profesiones_IdProfesion",
                        column: x => x.IdProfesion,
                        principalTable: "Profesiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estudios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoEstudio = table.Column<int>(type: "int", nullable: false),
                    IdAfiliado = table.Column<int>(type: "int", nullable: false),
                    IdGrado = table.Column<int>(type: "int", nullable: false),
                    IdInstitucion = table.Column<int>(type: "int", nullable: false),
                    TituloRecibido = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estudios_Afiliados_IdAfiliado",
                        column: x => x.IdAfiliado,
                        principalTable: "Afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudios_GradosAcademicos_IdGrado",
                        column: x => x.IdGrado,
                        principalTable: "GradosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudios_Instituciones_IdInstitucion",
                        column: x => x.IdInstitucion,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudios_TiposEstudios_IdTipoEstudio",
                        column: x => x.IdTipoEstudio,
                        principalTable: "TiposEstudios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciasLaborales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAfiliado = table.Column<int>(type: "int", nullable: false),
                    IdInstitucion = table.Column<int>(type: "int", nullable: false),
                    CargoTitulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciasLaborales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienciasLaborales_Afiliados_IdAfiliado",
                        column: x => x.IdAfiliado,
                        principalTable: "Afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienciasLaborales_Instituciones_IdInstitucion",
                        column: x => x.IdInstitucion,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afiliados_IdPersona",
                table: "Afiliados",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_AfiliadosIdiomas_IdAfiliado",
                table: "AfiliadosIdiomas",
                column: "IdAfiliado");

            migrationBuilder.CreateIndex(
                name: "IX_AfiliadosIdiomas_IdIdioma",
                table: "AfiliadosIdiomas",
                column: "IdIdioma");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_IdPersona",
                table: "Direcciones",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Estudios_IdAfiliado",
                table: "Estudios",
                column: "IdAfiliado");

            migrationBuilder.CreateIndex(
                name: "IX_Estudios_IdGrado",
                table: "Estudios",
                column: "IdGrado");

            migrationBuilder.CreateIndex(
                name: "IX_Estudios_IdInstitucion",
                table: "Estudios",
                column: "IdInstitucion");

            migrationBuilder.CreateIndex(
                name: "IX_Estudios_IdTipoEstudio",
                table: "Estudios",
                column: "IdTipoEstudio");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciasLaborales_IdAfiliado",
                table: "ExperienciasLaborales",
                column: "IdAfiliado");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciasLaborales_IdInstitucion",
                table: "ExperienciasLaborales",
                column: "IdInstitucion");

            migrationBuilder.CreateIndex(
                name: "IX_Instituciones_IdTipoInstitucion",
                table: "Instituciones",
                column: "IdTipoInstitucion");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasTiposSociales_IdPersona",
                table: "PersonasTiposSociales",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasTiposSociales_IdTipoSocial",
                table: "PersonasTiposSociales",
                column: "IdTipoSocial");

            migrationBuilder.CreateIndex(
                name: "IX_Profesiones_IdGrado",
                table: "Profesiones",
                column: "IdGrado");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionesAfiliados_IdAfiliado",
                table: "ProfesionesAfiliados",
                column: "IdAfiliado");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionesAfiliados_IdProfesion",
                table: "ProfesionesAfiliados",
                column: "IdProfesion");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_IdPersona",
                table: "Telefonos",
                column: "IdPersona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfiliadosIdiomas");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "Estudios");

            migrationBuilder.DropTable(
                name: "ExperienciasLaborales");

            migrationBuilder.DropTable(
                name: "PersonasTiposSociales");

            migrationBuilder.DropTable(
                name: "ProfesionesAfiliados");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropTable(
                name: "TiposEstudios");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "TiposSociales");

            migrationBuilder.DropTable(
                name: "Afiliados");

            migrationBuilder.DropTable(
                name: "Profesiones");

            migrationBuilder.DropTable(
                name: "TiposInstituciones");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "GradosAcademicos");
        }
    }
}
