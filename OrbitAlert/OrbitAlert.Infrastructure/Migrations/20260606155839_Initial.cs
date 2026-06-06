using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitAlert.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // tabelas já existem no banco Oracle — criadas pelos scripts SQL do Java
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // rollback não aplicável
        }
    }
}
