using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSaloonAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Token", "Username" },
                values: new object[] { 1, new byte[] { 0, 23, 121, 251, 134, 134, 208, 177, 3, 14, 123, 73, 191, 195, 249, 55, 198, 62, 218, 82, 98, 214, 227, 39, 0, 94, 17, 3, 226, 204, 149, 164, 73, 180, 17, 232, 170, 119, 141, 41, 222, 170, 26, 141, 111, 32, 148, 70, 6, 173, 105, 12, 186, 11, 236, 92, 24, 35, 95, 168, 231, 68, 139, 123 }, new byte[] { 218, 120, 55, 222, 161, 21, 56, 182, 129, 185, 55, 144, 109, 233, 211, 78, 91, 63, 205, 30, 162, 71, 250, 11, 124, 63, 188, 120, 35, 167, 203, 80, 86, 193, 153, 134, 149, 165, 12, 206, 254, 54, 57, 52, 235, 94, 224, 249, 216, 121, 121, 77, 83, 94, 221, 235, 136, 79, 216, 61, 172, 181, 246, 38, 33, 34, 241, 20, 103, 132, 63, 6, 21, 166, 218, 254, 113, 233, 127, 172, 68, 98, 16, 244, 226, 7, 130, 76, 233, 160, 70, 204, 59, 37, 22, 90, 219, 118, 226, 17, 126, 254, 143, 82, 241, 227, 127, 2, 99, 237, 83, 183, 5, 2, 44, 244, 171, 8, 117, 239, 88, 231, 16, 10, 110, 79, 17, 43 }, "106DAE1AD0DFA92F1E9D246B1DA2943C3F96AE9AD41FCFB5DC4E9EDD9C33BA95A79A4429937FA39F4E1FCD1BB8BB3F3D238057D9A1A68D52F31045BE829BFC73", "admin" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "EmailAddress", "FirstName", "Gender", "LastName", "PhoneNumber", "Type", "UserId" },
                values: new object[] { 1, "hd@test.com", "hair", 0, "dresser", "0101", 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
