using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityMapper
{
    public class DatasetMap : IEntityTypeConfiguration<Dataset>
    {
        public void Configure(EntityTypeBuilder<Dataset> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_datasetid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasColumnType("UniqueIdentifier");
            builder.Property(x => x.Label).ValueGeneratedOnAdd()
                .HasColumnName("label")
                .HasColumnType("INT");
            builder.Property(x => x.ImageMatrix).ValueGeneratedOnAdd()
                .HasColumnName("image")
                .HasColumnType("BLOB");
            builder.Property(x => x.IsTest).ValueGeneratedOnAdd()
                .HasColumnName("is_test")
                .HasColumnType("INT");
        }
    }
}
