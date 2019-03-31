using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartApi.Data.Migrations
{
    public partial class SeedDataAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("87b43b40-1198-4f2b-9012-af70746905c6"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 64, DateTimeKind.Unspecified).AddTicks(4089), new TimeSpan(0, 0, 0, 0, 0)), "John", "Smith" },
                    { new Guid("9a59bc83-652a-43c2-96ca-9f37bfd56b8e"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 64, DateTimeKind.Unspecified).AddTicks(5178), new TimeSpan(0, 0, 0, 0, 0)), "Jane", "Brown" },
                    { new Guid("356a2e7c-7810-42fd-9dae-ce77105ee186"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 64, DateTimeKind.Unspecified).AddTicks(5187), new TimeSpan(0, 0, 0, 0, 0)), "Kevin", "Ali" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("61e4e0e1-f85e-44e5-bae6-7977e0605b65"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 81, DateTimeKind.Unspecified).AddTicks(1177), new TimeSpan(0, 0, 0, 0, 0)), null, "Shoes" },
                    { new Guid("207cdf30-6f11-4a49-b561-6b6a606ca33d"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 81, DateTimeKind.Unspecified).AddTicks(2419), new TimeSpan(0, 0, 0, 0, 0)), null, "Wardrobe" },
                    { new Guid("01c32a12-beb6-414a-b8ca-386c08141b73"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 81, DateTimeKind.Unspecified).AddTicks(2424), new TimeSpan(0, 0, 0, 0, 0)), null, "Coffee Cup" },
                    { new Guid("a8794526-9395-4490-80b6-cef537b1fcd8"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 81, DateTimeKind.Unspecified).AddTicks(2429), new TimeSpan(0, 0, 0, 0, 0)), null, "Curtains" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("d0c2319a-a334-4940-8792-dc595dfabe0e"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 78, DateTimeKind.Unspecified).AddTicks(6140), new TimeSpan(0, 0, 0, 0, 0)), new Guid("87b43b40-1198-4f2b-9012-af70746905c6") },
                    { new Guid("719d29b7-774f-4514-9b43-14293266f185"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 78, DateTimeKind.Unspecified).AddTicks(6824), new TimeSpan(0, 0, 0, 0, 0)), new Guid("87b43b40-1198-4f2b-9012-af70746905c6") },
                    { new Guid("388f8500-1ea0-41db-bf22-1bf98cc9150a"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 78, DateTimeKind.Unspecified).AddTicks(6829), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9a59bc83-652a-43c2-96ca-9f37bfd56b8e") },
                    { new Guid("1e5567f9-73ae-4c71-abb6-831b501d71c2"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 78, DateTimeKind.Unspecified).AddTicks(6834), new TimeSpan(0, 0, 0, 0, 0)), new Guid("356a2e7c-7810-42fd-9dae-ce77105ee186") },
                    { new Guid("048ce70d-cba3-4a06-ab93-b497514e8973"), new DateTimeOffset(new DateTime(2019, 3, 30, 20, 53, 30, 78, DateTimeKind.Unspecified).AddTicks(6834), new TimeSpan(0, 0, 0, 0, 0)), new Guid("356a2e7c-7810-42fd-9dae-ce77105ee186") }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "ConcurrencyToken", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("c802d309-8942-422b-ac64-b324c99a94ce"), "9eb3597e-2e6c-4283-b152-ad912308657b", new Guid("d0c2319a-a334-4940-8792-dc595dfabe0e"), new Guid("61e4e0e1-f85e-44e5-bae6-7977e0605b65"), 1 },
                    { new Guid("18abb039-4651-418b-9771-6bbf89483491"), "c0668800-c9a5-4613-b1de-a4d5525ca94d", new Guid("d0c2319a-a334-4940-8792-dc595dfabe0e"), new Guid("01c32a12-beb6-414a-b8ca-386c08141b73"), 22 },
                    { new Guid("76919a98-bb74-4e64-8141-27befe53b9b0"), "c0c7a1d8-b738-4d4b-8fb1-fb97614ef874", new Guid("719d29b7-774f-4514-9b43-14293266f185"), new Guid("207cdf30-6f11-4a49-b561-6b6a606ca33d"), 4 },
                    { new Guid("42e946bc-87fd-45e0-a2ff-c8658916ef70"), "424b9ba0-f19e-439a-8c18-baa3e137a006", new Guid("719d29b7-774f-4514-9b43-14293266f185"), new Guid("01c32a12-beb6-414a-b8ca-386c08141b73"), 3 },
                    { new Guid("e20e3557-5322-42a4-949f-5d17ea17778d"), "4f271561-210b-415c-9b9e-3ab96ec130b2", new Guid("388f8500-1ea0-41db-bf22-1bf98cc9150a"), new Guid("a8794526-9395-4490-80b6-cef537b1fcd8"), 17 },
                    { new Guid("34369397-dd92-4dae-8bb4-934312249fe0"), "7ff3666a-1c84-44d0-b79e-5c8c29221251", new Guid("1e5567f9-73ae-4c71-abb6-831b501d71c2"), new Guid("207cdf30-6f11-4a49-b561-6b6a606ca33d"), 8 },
                    { new Guid("ebba711f-9e29-4291-be1a-e887a6b89855"), "3601969a-fbb0-446e-bf0b-fd65cc45a7c9", new Guid("048ce70d-cba3-4a06-ab93-b497514e8973"), new Guid("207cdf30-6f11-4a49-b561-6b6a606ca33d"), 1 },
                    { new Guid("dd523186-47cc-4f10-97c0-2a486ae802c4"), "f0887ed8-5f02-4184-9a5e-ec87422f0fc4", new Guid("048ce70d-cba3-4a06-ab93-b497514e8973"), new Guid("01c32a12-beb6-414a-b8ca-386c08141b73"), 1 },
                    { new Guid("6c50c454-c166-4436-9fa9-3e8d26a4566c"), "83251f3a-a7c4-4520-9076-f7ed461482d9", new Guid("048ce70d-cba3-4a06-ab93-b497514e8973"), new Guid("a8794526-9395-4490-80b6-cef537b1fcd8"), 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("18abb039-4651-418b-9771-6bbf89483491"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("34369397-dd92-4dae-8bb4-934312249fe0"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("42e946bc-87fd-45e0-a2ff-c8658916ef70"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("6c50c454-c166-4436-9fa9-3e8d26a4566c"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("76919a98-bb74-4e64-8141-27befe53b9b0"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("c802d309-8942-422b-ac64-b324c99a94ce"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("dd523186-47cc-4f10-97c0-2a486ae802c4"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("e20e3557-5322-42a4-949f-5d17ea17778d"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("ebba711f-9e29-4291-be1a-e887a6b89855"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("048ce70d-cba3-4a06-ab93-b497514e8973"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("1e5567f9-73ae-4c71-abb6-831b501d71c2"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("388f8500-1ea0-41db-bf22-1bf98cc9150a"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("719d29b7-774f-4514-9b43-14293266f185"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("d0c2319a-a334-4940-8792-dc595dfabe0e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("01c32a12-beb6-414a-b8ca-386c08141b73"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("207cdf30-6f11-4a49-b561-6b6a606ca33d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("61e4e0e1-f85e-44e5-bae6-7977e0605b65"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a8794526-9395-4490-80b6-cef537b1fcd8"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("356a2e7c-7810-42fd-9dae-ce77105ee186"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("87b43b40-1198-4f2b-9012-af70746905c6"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("9a59bc83-652a-43c2-96ca-9f37bfd56b8e"));
        }
    }
}
