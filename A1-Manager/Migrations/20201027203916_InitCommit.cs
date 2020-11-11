using Microsoft.EntityFrameworkCore.Migrations;

namespace A1_Manager.Migrations
{
    public partial class InitCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Volume = table.Column<float>(nullable: false),
                    VolumeType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Money",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(nullable: false),
                    Amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Money", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Money_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LogoURL = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    DateAddedId = table.Column<int>(nullable: false),
                    PreferredCurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Dates_DateAddedId",
                        column: x => x.DateAddedId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brands_Currencies_PreferredCurrencyId",
                        column: x => x.PreferredCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdfURL = table.Column<string>(nullable: true),
                    SignedDateId = table.Column<int>(nullable: false),
                    ExpirationDateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Dates_ExpirationDateId",
                        column: x => x.ExpirationDateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Dates_SignedDateId",
                        column: x => x.SignedDateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameId = table.Column<int>(nullable: false),
                    IdentityId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    DateAddedId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Dates_DateAddedId",
                        column: x => x.DateAddedId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoneyPerAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoneyId = table.Column<int>(nullable: false),
                    AmountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyPerAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyPerAmount_Amount_AmountId",
                        column: x => x.AmountId,
                        principalTable: "Amount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoneyPerAmount_Money_MoneyId",
                        column: x => x.MoneyId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    DateAddedId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PreferredCurrencyId = table.Column<int>(nullable: false),
                    OccupancyCostId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Branches_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Dates_DateAddedId",
                        column: x => x.DateAddedId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Identities_NameId",
                        column: x => x.NameId,
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Money_OccupancyCostId",
                        column: x => x.OccupancyCostId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Currencies_PreferredCurrencyId",
                        column: x => x.PreferredCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BrandSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    ExpensesId = table.Column<int>(nullable: true),
                    ProfitId = table.Column<int>(nullable: true),
                    RevenueId = table.Column<int>(nullable: true),
                    TaxId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandSales_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrandSales_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrandSales_Money_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrandSales_Money_ProfitId",
                        column: x => x.ProfitId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrandSales_Money_RevenueId",
                        column: x => x.RevenueId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrandSales_Money_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    BarCode = table.Column<string>(nullable: true),
                    BarCodeImageURL = table.Column<string>(nullable: true),
                    DateAddedId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Dates_DateAddedId",
                        column: x => x.DateAddedId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Identities_NameId",
                        column: x => x.NameId,
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    ExpensesId = table.Column<int>(nullable: true),
                    ProfitId = table.Column<int>(nullable: true),
                    RevenueId = table.Column<int>(nullable: true),
                    TaxId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchSales_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchSales_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchSales_Money_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchSales_Money_ProfitId",
                        column: x => x.ProfitId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchSales_Money_RevenueId",
                        column: x => x.RevenueId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchSales_Money_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchSuppliers",
                columns: table => new
                {
                    BranchId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchSuppliers", x => new { x.BranchId, x.SupplierId });
                    table.ForeignKey(
                        name: "FK_BranchSuppliers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchSuppliers_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstNameId = table.Column<int>(nullable: false),
                    LastNameId = table.Column<int>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    LeaveRemaining = table.Column<int>(nullable: false),
                    SalaryId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: true),
                    BranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Identities_FirstNameId",
                        column: x => x.FirstNameId,
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Identities_LastNameId",
                        column: x => x.LastNameId,
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Money_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatus = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    OrderedDateId = table.Column<int>(nullable: false),
                    DeliveryDateId = table.Column<int>(nullable: false),
                    MoneyPerAmountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Dates_DeliveryDateId",
                        column: x => x.DeliveryDateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_MoneyPerAmount_MoneyPerAmountId",
                        column: x => x.MoneyPerAmountId,
                        principalTable: "MoneyPerAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Dates_OrderedDateId",
                        column: x => x.OrderedDateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    DateAddedId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    CostId = table.Column<int>(nullable: false),
                    RetailPriceId = table.Column<int>(nullable: false),
                    TaxPercentage = table.Column<int>(nullable: false),
                    StockId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchProduct_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchProduct_Money_CostId",
                        column: x => x.CostId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchProduct_Dates_DateAddedId",
                        column: x => x.DateAddedId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchProduct_MoneyPerAmount_RetailPriceId",
                        column: x => x.RetailPriceId,
                        principalTable: "MoneyPerAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchProduct_Amount_StockId",
                        column: x => x.StockId,
                        principalTable: "Amount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchProduct_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePresence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    ClockInTimeId = table.Column<int>(nullable: false),
                    ClockOutTimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePresence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePresence_Dates_ClockInTimeId",
                        column: x => x.ClockInTimeId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePresence_Dates_ClockOutTimeId",
                        column: x => x.ClockOutTimeId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePresence_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => new { x.EmployeeId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Roles_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    BranchProductId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => new { x.BranchProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ProductOrders_BranchProduct_BranchProductId",
                        column: x => x.BranchProductId,
                        principalTable: "BranchProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Orders_BranchProductId",
                        column: x => x.BranchProductId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchProductId = table.Column<int>(nullable: false),
                    DateId = table.Column<int>(nullable: false),
                    ExpensesId = table.Column<int>(nullable: true),
                    ProfitId = table.Column<int>(nullable: true),
                    RevenueId = table.Column<int>(nullable: true),
                    TaxId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSales_BranchProduct_BranchProductId",
                        column: x => x.BranchProductId,
                        principalTable: "BranchProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSales_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSales_Money_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSales_Money_ProfitId",
                        column: x => x.ProfitId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSales_Money_RevenueId",
                        column: x => x.RevenueId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSales_Money_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_BrandId",
                table: "Branches",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CityId",
                table: "Branches",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CountryId",
                table: "Branches",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_DateAddedId",
                table: "Branches",
                column: "DateAddedId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_NameId",
                table: "Branches",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_OccupancyCostId",
                table: "Branches",
                column: "OccupancyCostId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_PreferredCurrencyId",
                table: "Branches",
                column: "PreferredCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_BranchId",
                table: "BranchProduct",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_CostId",
                table: "BranchProduct",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_DateAddedId",
                table: "BranchProduct",
                column: "DateAddedId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_ProductId",
                table: "BranchProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_RetailPriceId",
                table: "BranchProduct",
                column: "RetailPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_StockId",
                table: "BranchProduct",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_SupplierId",
                table: "BranchProduct",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSales_BranchId",
                table: "BranchSales",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSales_DateId",
                table: "BranchSales",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSales_ExpensesId",
                table: "BranchSales",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSales_ProfitId",
                table: "BranchSales",
                column: "ProfitId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSales_RevenueId",
                table: "BranchSales",
                column: "RevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSales_TaxId",
                table: "BranchSales",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSuppliers_ContractId",
                table: "BranchSuppliers",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSuppliers_SupplierId",
                table: "BranchSuppliers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_DateAddedId",
                table: "Brands",
                column: "DateAddedId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_PreferredCurrencyId",
                table: "Brands",
                column: "PreferredCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSales_BrandId",
                table: "BrandSales",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSales_DateId",
                table: "BrandSales",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSales_ExpensesId",
                table: "BrandSales",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSales_ProfitId",
                table: "BrandSales",
                column: "ProfitId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSales_RevenueId",
                table: "BrandSales",
                column: "RevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSales_TaxId",
                table: "BrandSales",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ExpirationDateId",
                table: "Contracts",
                column: "ExpirationDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SignedDateId",
                table: "Contracts",
                column: "SignedDateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePresence_ClockInTimeId",
                table: "EmployeePresence",
                column: "ClockInTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePresence_ClockOutTimeId",
                table: "EmployeePresence",
                column: "ClockOutTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePresence_EmployeeId",
                table: "EmployeePresence",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ContractId",
                table: "Employees",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FirstNameId",
                table: "Employees",
                column: "FirstNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastNameId",
                table: "Employees",
                column: "LastNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SalaryId",
                table: "Employees",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Money_CurrencyId",
                table: "Money",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyPerAmount_AmountId",
                table: "MoneyPerAmount",
                column: "AmountId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyPerAmount_MoneyId",
                table: "MoneyPerAmount",
                column: "MoneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BranchId",
                table: "Orders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryDateId",
                table: "Orders",
                column: "DeliveryDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MoneyPerAmountId",
                table: "Orders",
                column: "MoneyPerAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedDateId",
                table: "Orders",
                column: "OrderedDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId",
                table: "Orders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DateAddedId",
                table: "Products",
                column: "DateAddedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_BranchProductId",
                table: "ProductSales",
                column: "BranchProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_DateId",
                table: "ProductSales",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ExpensesId",
                table: "ProductSales",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProfitId",
                table: "ProductSales",
                column: "ProfitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_RevenueId",
                table: "ProductSales",
                column: "RevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_TaxId",
                table: "ProductSales",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_BrandId",
                table: "Roles",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_NameId",
                table: "Roles",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CityId",
                table: "Suppliers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CountryId",
                table: "Suppliers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_DateAddedId",
                table: "Suppliers",
                column: "DateAddedId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_IdentityId",
                table: "Suppliers",
                column: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchSales");

            migrationBuilder.DropTable(
                name: "BranchSuppliers");

            migrationBuilder.DropTable(
                name: "BrandSales");

            migrationBuilder.DropTable(
                name: "EmployeePresence");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "BranchProduct");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "MoneyPerAmount");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Amount");

            migrationBuilder.DropTable(
                name: "Money");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Identities");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
