using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MessagingApp.Models
{
    public partial class MessagingAppContext : DbContext
    {
        public MessagingAppContext()
        {
        }

        public MessagingAppContext(DbContextOptions<MessagingAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conversation> TblConversations { get; set; } = null!;
        public virtual DbSet<ConversationAccess> TblConversationAccesses { get; set; } = null!;
        public virtual DbSet<MessageModel> TblMessages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=MessagingApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.PkTblConversation);

                entity.ToTable("tbl_Conversation");

                entity.Property(e => e.PkTblConversation).HasColumnName("pk_tbl_Conversation");

                entity.Property(e => e.FkTblCreator).HasColumnName("fk_tbl_Creator");

                entity.Property(e => e.TblConversationHeader)
                    .HasMaxLength(50)
                    .HasColumnName("tbl_Conversation_Header")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ConversationAccess>(entity =>
            {
                entity.HasKey(e => e.PkTblConversationAccess);

                entity.ToTable("tbl_ConversationAccess");

                entity.Property(e => e.PkTblConversationAccess).HasColumnName("pk_tbl_ConversationAccess");

                entity.Property(e => e.FkTblConversation).HasColumnName("fk_tbl_Conversation");

                entity.Property(e => e.FkTblUser).HasColumnName("fk_tbl_User");
            });

            modelBuilder.Entity<MessageModel>(entity =>
            {
                entity.HasKey(e => e.PkTblMessage);

                entity.ToTable("tbl_Message");

                entity.Property(e => e.PkTblMessage).HasColumnName("pk_tbl_Message");

                entity.Property(e => e.FkTblConversation).HasColumnName("fk_tbl_Conversation");

                entity.Property(e => e.FkTblUser).HasColumnName("fk_tbl_User");

                entity.Property(e => e.TblMessageBody)
                    .HasColumnType("text")
                    .HasColumnName("tbl_Message_Body");

                entity.Property(e => e.TblMessageHeader)
                    .HasMaxLength(50)
                    .HasColumnName("tbl_Message_Header");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
