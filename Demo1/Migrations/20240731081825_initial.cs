using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatricMarksheetPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatricCertificatePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterMarksheetPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterCertificatePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CnicPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastQualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectAreaExpertise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BachelorsDegreePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MastersDegreePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
