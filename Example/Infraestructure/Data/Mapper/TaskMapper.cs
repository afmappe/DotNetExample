using Example.Infraestructure.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Example.Infraestructure.Data.Mapper
{
    /// <summary>
    ///
    /// </summary>
    internal class TaskMapper : EntityTypeConfiguration<TaskInfo>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="schema"></param>
        public TaskMapper(string schema)
        {
            HasKey(x => x.Id);

            Property(x => x.Description)
                .HasMaxLength(256);

            Property(x => x.Name)
               .HasMaxLength(50);

            ToTable("EX_TASK", schema);
            Property(x => x.Id).HasColumnName("ID");
            Property(x => x.Description).HasColumnName("DESCRIPTION");
            Property(x => x.Name).HasColumnName("NAME");
            Property(x => x.CreationDate).HasColumnName("CREATION_DATE");
            Property(x => x.CompletDate).HasColumnName("COMPLETE_DATE");
            Property(x => x.CategoryId).HasColumnName("CATEGORY_ID");
            Property(x => x.Status).HasColumnName("STATUS_ID");
        }
    }
}