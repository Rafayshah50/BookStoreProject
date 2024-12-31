using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Ryan Holiday", "The Ego is the Enemy draws on a vast array of stories and examples, from literature to philosophy to history. We meet fascinating figures like Howard Hughes, Katharine Graham, Bill Belichick, and Eleanor Roosevelt, all of whom reached the highest levels of power and success by conquering their own egos. Their strategies and tactics can be ours as well. ", "SWD9999001", 99.0, 90.0, 80.0, 85.0, "Ego is the Enemy" },
                    { 2, "Sun Tzu", "Twenty-Five Hundred years ago, Sun Tzu wrote this classic book of military strategy based on Chinese warfare and military thought. Since that time, all levels of military have used the teaching on Sun Tzu to warfare and civilization have adapted these teachings for use in politics, business and everyday life. ", "CAW777777701", 70.0, 80.0, 70.0, 75.0, "The Art of War" },
                    { 3, "Albert Camus", "Through this story of an ordinary man unwittingly drawn into a senseless murder on a sundrenched Algerian beach, Camus explores what he termed \"the nakedness of man faced with the absurd. ", "RITO5555501", 60.0, 55.0, 40.0, 45.0, "The Stranger" },
                    { 4, "Fyodor Dostoevsky", "Raskolnikov, a destitute and desperate former student, wanders through the slums of St Petersburg and commits a random murder without remorse or regret. He imagines himself to be a great man, a Napoleon: acting for a higher purpose beyond conventional moral law. But as he embarks on a dangerous game of cat and mouse with a suspicious police investigator, Raskolnikov is pursued by the growing voice of his conscience and finds the noose of his own guilt tightening around his neck.", "WS3333333301", 70.0, 65.0, 55.0, 60.0, "Crime and Punishment" },
                    { 5, "Hamza Andreas", "In The Divine Reality, Hamza Andreas Tzortzis provides a compelling case for the rational and spiritual foundations of Islam, whilst intelligently and compassionately deconstructing atheism. Join him on an existential, spiritual and rational journey that articulates powerful arguments for the existence of God, the Qur'an, the Prophethood of Muhammad and why we must know, love and worship God.", "SOTJ1111111101", 40.0, 35.0, 25.0, 30.0, "The Divine Reality: God, Islam and the Mirage of Atheism" },
                    { 6, "Robert Greene", "Some laws teach the need for prudence (“Law 1: Never Outshine the Master”), others teach the value of confidence (“Law 28: Enter Action with Boldness”), and many recommend absolute self-preservation (“Law 15: Crush Your Enemy Totally”). Every law, though, has one thing in common: an interest in total domination. In a bold and arresting two-color package, The 48 Laws of Power is ideal whether your aim is conquest, self-defense, or simply to understand the rules of the game.", "FOT000000001", 80.0, 70.0, 60.0, 65.0, "The 48 Laws of Power" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
