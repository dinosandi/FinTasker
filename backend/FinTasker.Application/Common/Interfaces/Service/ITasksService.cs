using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface ITasksService
    {
        // Task CreateTasksAsync(Tasks Tasks);
        // Task<Tasks> GetTasksByIdAsync(Guid TasksId); // mengambil data Tasks berdasarkan ID

        // Task UpdateTasksAsync(Tasks Tasks); // memperbarui data Tasks yang sudah ada
        // Task DeleteTasksAsync(Guid TasksId); // menghapus data Tasks berdasarkan ID

        // Untuk Bulk delete atau hapus banyak data Tasks sekaligus dalam satu operasi
        // Task BulkDeleteTasksAsync(List<Guid> TasksIds); // menghapus banyak data Tasks berdasarkan daftar ID
    }
}

