using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Core.Entities;

namespace Project.Infra.Persistent.Configurations
{
	public class ModuleConfiguration : IEntityTypeConfiguration<Module>
	{
		public void Configure(EntityTypeBuilder<Module> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired().ValueGeneratedOnAdd();
		}
	}
}
