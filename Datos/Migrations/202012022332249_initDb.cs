namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Financiaciones",
                c => new
                    {
                        IdFinanciacion = c.Int(nullable: false, identity: true),
                        Monto = c.Double(nullable: false),
                        Fecha = c.DateTime(nullable: false, storeType: "date"),
                        IdInversor = c.Int(nullable: false),
                        IdProyecto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdFinanciacion)
                .ForeignKey("dbo.Inversores", t => t.IdInversor, cascadeDelete: true)
                .ForeignKey("dbo.Proyectos", t => t.IdProyecto, cascadeDelete: true)
                .Index(t => t.IdInversor)
                .Index(t => t.IdProyecto);
            
            CreateTable(
                "dbo.Inversores",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        MaxInvPorProyecto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PresentacionInversor = c.String(maxLength: 500),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Pass = c.String(nullable: false),
                        FechaDeNacimiento = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(nullable: false, maxLength: 200),
                        Cell = c.String(nullable: false),
                        TienePassTemporal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Proyectos",
                c => new
                    {
                        IdProyecto = c.Int(nullable: false),
                        Estado = c.String(nullable: false, maxLength: 1),
                        TipoDeEquipo = c.String(nullable: false, maxLength: 1),
                        Titulo = c.String(nullable: false),
                        Descripcion = c.String(nullable: false),
                        ImgURL = c.String(nullable: false),
                        FechaDePresentacion = c.DateTime(nullable: false, storeType: "date"),
                        CantidadDeIntegrantes = c.Int(nullable: false),
                        ExperienciaPersonal = c.String(),
                        Cuotas = c.Int(nullable: false),
                        PrecioPorCuota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontoSolicitado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcentajeDeInteres = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontoConseguido = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdSolicitante = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProyecto)
                .ForeignKey("dbo.Solicitantes", t => t.IdSolicitante, cascadeDelete: true)
                .Index(t => t.IdSolicitante);
            
            CreateTable(
                "dbo.Solicitantes",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Pass = c.String(nullable: false),
                        FechaDeNacimiento = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(nullable: false, maxLength: 200),
                        Cell = c.String(nullable: false),
                        TienePassTemporal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Financiaciones", "IdProyecto", "dbo.Proyectos");
            DropForeignKey("dbo.Proyectos", "IdSolicitante", "dbo.Solicitantes");
            DropForeignKey("dbo.Financiaciones", "IdInversor", "dbo.Inversores");
            DropIndex("dbo.Solicitantes", new[] { "Email" });
            DropIndex("dbo.Proyectos", new[] { "IdSolicitante" });
            DropIndex("dbo.Inversores", new[] { "Email" });
            DropIndex("dbo.Financiaciones", new[] { "IdProyecto" });
            DropIndex("dbo.Financiaciones", new[] { "IdInversor" });
            DropTable("dbo.Solicitantes");
            DropTable("dbo.Proyectos");
            DropTable("dbo.Inversores");
            DropTable("dbo.Financiaciones");
        }
    }
}
