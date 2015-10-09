namespace ParkingSpace.DataAccess.Migrations {
  using System;
  using System.Data.Entity.Migrations;

  public partial class InitialCreate : DbMigration {
    public override void Up() {
      CreateTable(
          "dbo.ParkingTickets",
          c => new {
            Id = c.String(nullable: false, maxLength: 128),
            PlateNumber = c.String(),
            GateId = c.Int(nullable: false),
            DateIn = c.DateTime(nullable: false),
            DateOut = c.DateTime(),
          })
          .PrimaryKey(t => t.Id);

    }

    public override void Down() {
      DropTable("dbo.ParkingTickets");
    }
  }
}
