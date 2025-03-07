using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrefour.Desafio.ORM.Migrations
{
    /// <inheritdoc />
    public partial class CreateConsolidadoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CONSOLIDADO",
                columns: table => new
                {
                    CON_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DT_CONSOLIDADO = table.Column<DateTime>(type: "date", nullable: false),
                    VLR_DEB = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0),
                    VLR_CRE = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0),
                    VLR_SLD_FINAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0),
                    CREATED_AT = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CONSOLIDADO", x => x.CON_ID);
                    table.UniqueConstraint("UQ_TBL_CONSOLIDADO_DT", x => x.DT_CONSOLIDADO);
                    table.CheckConstraint("CHK_VLR_DEB", "VLR_DEB >= 0");
                    table.CheckConstraint("CHK_VLR_CRE", "VLR_CRE >= 0");
                    table.CheckConstraint("CHK_VLR_SLD_FINAL", "VLR_SLD_FINAL >= 0");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TBL_CONSOLIDADO");
        }
    }
}
