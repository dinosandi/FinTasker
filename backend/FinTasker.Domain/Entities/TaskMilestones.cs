using System;
using FinTasker.Domain.Enums;

namespace FinTasker.Domain.Entities
{
    public class TaskMilestones // untuk menyimpan informasi tentang tonggak pencapaian dalam sebuah tugas, seperti tanggal penyelesaian, deskripsi, atau status pencapaian. Ini bisa membantu dalam melacak kemajuan tugas dan memastikan bahwa semua langkah penting telah dicapai sesuai jadwal.
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset TargetDate { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public MilestonesStatus Status { get; set; }
        public DateTimeOffset CreadtedAt { get; set; }

        // Navigation property
        public Projects Project { get; set; }

    }
}
