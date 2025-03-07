using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrefour.Desafio.ORM.Migrations
{
    /// <inheritdoc />
    public partial class CreateLancamentoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_LANCAMENTO",
                columns: table => new
                {
                    LAN_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DT_LANCAMENTO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    TP_LANCAMENTO = table.Column<string>(type: "varchar(10)", nullable: false),
                    VLR_LANCAMENTO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DES_LANCAMENTO = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CAT_LANCAMENTO = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_LANCAMENTO", x => x.LAN_ID);
                    table.CheckConstraint("CHK_TP_LANCAMENTO", "TP_LANCAMENTO IN ('DEBITO', 'CREDITO')");
                    table.CheckConstraint("CHK_VLR_LANCAMENTO", "VLR_LANCAMENTO > 0");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TBL_LANCAMENTO");
        }
    }
}
