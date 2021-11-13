namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IniitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipoes",
                c => new
                    {
                        EquipoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Adquisicion = c.DateTime(nullable: false),
                        VencimientoGarantia = c.DateTime(nullable: false),
                        ProveedorId = c.Int(nullable: false),
                        OficinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipoId)
                .ForeignKey("dbo.Oficinas", t => t.OficinaId, cascadeDelete: true)
                .ForeignKey("dbo.Proveedors", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.ProveedorId)
                .Index(t => t.OficinaId);
            
            CreateTable(
                "dbo.Oficinas",
                c => new
                    {
                        OficinaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.OficinaId);
            
            CreateTable(
                "dbo.Perifericoes",
                c => new
                    {
                        PerifericoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PerifericoId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.Proveedors",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        CUIT = c.String(),
                        RazonSocial = c.String(),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Registroes",
                c => new
                    {
                        RegistroId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegistroId)
                .ForeignKey("dbo.Equipoes", t => t.EquipoId, cascadeDelete: true)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Password = c.String(),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        EquipoAsignado_EquipoId = c.Int(),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.Equipoes", t => t.EquipoAsignado_EquipoId)
                .Index(t => t.EquipoAsignado_EquipoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "EquipoAsignado_EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.Registroes", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.Equipoes", "ProveedorId", "dbo.Proveedors");
            DropForeignKey("dbo.Perifericoes", "EquipoId", "dbo.Equipoes");
            DropForeignKey("dbo.Equipoes", "OficinaId", "dbo.Oficinas");
            DropIndex("dbo.Users", new[] { "EquipoAsignado_EquipoId" });
            DropIndex("dbo.Registroes", new[] { "EquipoId" });
            DropIndex("dbo.Perifericoes", new[] { "EquipoId" });
            DropIndex("dbo.Equipoes", new[] { "OficinaId" });
            DropIndex("dbo.Equipoes", new[] { "ProveedorId" });
            DropTable("dbo.Users");
            DropTable("dbo.Registroes");
            DropTable("dbo.Proveedors");
            DropTable("dbo.Perifericoes");
            DropTable("dbo.Oficinas");
            DropTable("dbo.Equipoes");
        }
    }
}
