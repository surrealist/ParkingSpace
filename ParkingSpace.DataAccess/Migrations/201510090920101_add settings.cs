namespace ParkingSpace.DataAccess.Migrations {
  using System;
  using System.Data.Entity.Migrations;

  public partial class addsettings : DbMigration {
    public override void Up() {
      CreateTable(
          "dbo.Settings",
          c => new {
            Id = c.Int(nullable: false, identity: true),
            GateId = c.Int(nullable: false),
          })
          .PrimaryKey(t => t.Id);

    }

    public override void Down() {
      DropTable("dbo.Settings");
    }
  }
}
