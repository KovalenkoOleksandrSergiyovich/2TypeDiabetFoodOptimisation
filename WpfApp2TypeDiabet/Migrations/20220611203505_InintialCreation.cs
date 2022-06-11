using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WpfApp2TypeDiabet.Migrations
{
    public partial class InintialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodState",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Carbohydrates = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodState", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Restriction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestrictionName = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Comparator = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restriction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    IsSuperUser = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoodName = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.id);
                    table.ForeignKey(
                        name: "FK_Goods_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodBasket",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodBasket", x => x.id);
                    table.ForeignKey(
                        name: "FK_GoodBasket_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRestrictionList",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestrictionID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRestrictionList", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRestrictionList_Restriction_RestrictionID",
                        column: x => x.RestrictionID,
                        principalTable: "Restriction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRestrictionList_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodInShop",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoodId = table.Column<int>(type: "integer", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    RestrictionID = table.Column<int>(type: "integer", nullable: false),
                    GoodPrice = table.Column<double>(type: "double precision", nullable: false),
                    GoodAmount = table.Column<double>(type: "double precision", nullable: false),
                    GoodUnits = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodInShop", x => x.id);
                    table.ForeignKey(
                        name: "FK_GoodInShop_Goods_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Goods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodInShop_Restriction_RestrictionID",
                        column: x => x.RestrictionID,
                        principalTable: "Restriction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodShopState",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoodStateID = table.Column<int>(type: "integer", nullable: false),
                    GoodInShopID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodShopState", x => x.id);
                    table.ForeignKey(
                        name: "FK_GoodShopState_GoodInShop_GoodInShopID",
                        column: x => x.GoodInShopID,
                        principalTable: "GoodInShop",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodShopState_GoodState_GoodStateID",
                        column: x => x.GoodStateID,
                        principalTable: "GoodState",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGoodList",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoodID = table.Column<int>(type: "integer", nullable: false),
                    GoodInShopid = table.Column<int>(type: "integer", nullable: true),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGoodList", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserGoodList_GoodInShop_GoodInShopid",
                        column: x => x.GoodInShopid,
                        principalTable: "GoodInShop",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGoodList_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodInBasket",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoodBasketID = table.Column<int>(type: "integer", nullable: false),
                    GoodShopStateID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodInBasket", x => x.id);
                    table.ForeignKey(
                        name: "FK_GoodInBasket_GoodBasket_GoodBasketID",
                        column: x => x.GoodBasketID,
                        principalTable: "GoodBasket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodInBasket_GoodShopState_GoodShopStateID",
                        column: x => x.GoodShopStateID,
                        principalTable: "GoodShopState",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodBasket_UserID",
                table: "GoodBasket",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodInBasket_GoodBasketID",
                table: "GoodInBasket",
                column: "GoodBasketID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodInBasket_GoodShopStateID",
                table: "GoodInBasket",
                column: "GoodShopStateID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodInShop_GoodId",
                table: "GoodInShop",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodInShop_RestrictionID",
                table: "GoodInShop",
                column: "RestrictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_CategoryId",
                table: "Goods",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodShopState_GoodInShopID",
                table: "GoodShopState",
                column: "GoodInShopID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodShopState_GoodStateID",
                table: "GoodShopState",
                column: "GoodStateID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGoodList_GoodInShopid",
                table: "UserGoodList",
                column: "GoodInShopid");

            migrationBuilder.CreateIndex(
                name: "IX_UserGoodList_UserID",
                table: "UserGoodList",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRestrictionList_RestrictionID",
                table: "UserRestrictionList",
                column: "RestrictionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRestrictionList_UserID",
                table: "UserRestrictionList",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodInBasket");

            migrationBuilder.DropTable(
                name: "UserGoodList");

            migrationBuilder.DropTable(
                name: "UserRestrictionList");

            migrationBuilder.DropTable(
                name: "GoodBasket");

            migrationBuilder.DropTable(
                name: "GoodShopState");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GoodInShop");

            migrationBuilder.DropTable(
                name: "GoodState");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Restriction");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
