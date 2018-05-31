namespace ProjectExpenseControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountingAccounts",
                c => new
                    {
                        ACC_IDE_ACCOUNT = c.String(nullable: false, maxLength: 6),
                        ACC_DES_ACCOUNT = c.String(nullable: false, maxLength: 70),
                        ACC_FH_CREATED = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ACC_IDE_ACCOUNT);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ARE_IDE_AREA = c.String(nullable: false, maxLength: 10),
                        ARE_DES_NAME = c.String(nullable: false, maxLength: 60),
                        ARE_FH_CREATED = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ARE_IDE_AREA);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        BUD_IDE_BUDGET = c.Int(nullable: false, identity: true),
                        BUD_IDE_USER = c.Int(nullable: false),
                        BUD_IDE_ACCOUNT = c.String(nullable: false, maxLength: 6),
                        BUD_IDE_AREA = c.String(nullable: false, maxLength: 10),
                        BUD_DES_QUANTITY = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BUD_DES_PERIOD = c.String(nullable: false, maxLength: 12),
                        BUD_FH_CREATED = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BUD_IDE_BUDGET);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        INV_ID_INVOICE = c.Int(nullable: false, identity: true),
                        INV_DES_SERIE = c.String(maxLength: 10),
                        INV_DES_FOLIO = c.String(maxLength: 40),
                        INV_FH_FECHA = c.DateTime(nullable: false),
                        INV_DES_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        INV_DES_LUGAR_EXPEDICION = c.String(nullable: false, maxLength: 6),
                        INV_DES_EMISOR_RFC = c.String(nullable: false, maxLength: 13),
                        INV_DES_EMISOR_NOMBRE = c.String(nullable: false, maxLength: 254),
                        INV_DES_UUID = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.INV_ID_INVOICE);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        REQ_IDE_REQUEST = c.Int(nullable: false, identity: true),
                        REQ_IDE_USER = c.Int(nullable: false),
                        REQ_DES_TYPE_GASTO = c.Boolean(nullable: false),
                        REQ_DES_CONCEPT = c.String(maxLength: 100),
                        REQ_DES_QUANTITY = c.Decimal(nullable: false, precision: 18, scale: 2),
                        REQ_DES_OBSERVATIONS = c.String(maxLength: 200),
                        REQ_IDE_STATUS_APROV = c.Int(nullable: true),
                        REQ_IDE_PERIOD = c.Int(nullable : false),
                        REQ_FH_CREATED = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.REQ_IDE_REQUEST);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        TUSR_IDE_RESOURCE = c.Int(nullable: false, identity: true),
                        TUSR_DES_TYPE = c.String(nullable: false, maxLength: 70),
                        TUSR_FH_CREATED = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TUSR_IDE_RESOURCE);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        USR_IDE_USER = c.Int(nullable: false, identity: true),
                        USR_IDE_AREA = c.String(nullable: false, maxLength: 100),
                        USR_DES_POSITION = c.String(nullable: false, maxLength: 60),
                        USR_DES_NAME = c.String(nullable: false, maxLength: 100),
                        USR_DES_FIRST_NAME = c.String(nullable: false, maxLength: 60),
                        USR_DES_LAST_NAME = c.String(maxLength: 60),
                        USR_DES_PASSWORD = c.String(nullable: false, maxLength: 50),
                        USR_DES_PHONE = c.String(maxLength: 20),
                        USR_DES_EMAIL = c.String(nullable: false, maxLength: 40),
                        USR_FH_CREATED = c.DateTime(nullable: false),
                        USR_LAST_LOGIN = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.USR_IDE_USER);
            
            CreateTable(
                "dbo.StatusAprovs",
                c => new
                    {
                        STA_IDE_STATUS_APROV = c.Int(nullable: false, identity: true),
                        STA_DES_STATUS = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.STA_IDE_STATUS_APROV);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        USR_IDE_USER = c.Int(nullable: false),
                        TUSR_IDE_RESOURCE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.USR_IDE_USER, t.TUSR_IDE_RESOURCE })
                .ForeignKey("dbo.Users", t => t.USR_IDE_USER, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.TUSR_IDE_RESOURCE, cascadeDelete: true)
                .Index(t => t.USR_IDE_USER)
                .Index(t => t.TUSR_IDE_RESOURCE);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "TUSR_IDE_RESOURCE", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "USR_IDE_USER", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "TUSR_IDE_RESOURCE" });
            DropIndex("dbo.UserRoles", new[] { "USR_IDE_USER" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.StatusAprovs");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Requests");
            DropTable("dbo.Invoices");
            DropTable("dbo.Budgets");
            DropTable("dbo.Areas");
            DropTable("dbo.AccountingAccounts");
        }
    }
}
