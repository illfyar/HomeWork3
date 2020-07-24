namespace WpfTestMailSender
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MailsAndSendersModel : DbContext
    {
        public MailsAndSendersModel(): base ("MailsAndSendersConnectionString")            
        {
        }

        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Letter> Letter { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .Property(e => e.Adress)
                .IsFixedLength();

            modelBuilder.Entity<Email>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Email>()
                .HasMany(e => e.Letter)
                .WithRequired(e => e.RecipientEmail)
                .HasForeignKey(e => e.id_RecipientEmail);

            modelBuilder.Entity<Email>()
                .HasMany(e => e.Letter1)
                .WithRequired(e => e.SenderEmail)
                .HasForeignKey(e => e.id_SenderEmail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasMany(e => e.Letter)
                .WithRequired(e => e.Message)
                .HasForeignKey(e => e.id_Message);
        }
    }
}
