using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface IProjectsService
    {
        
        Task CreateProjectsAsync(Projects Projects);
        Task<Projects> GetProjectsByIdAsync(Guid ProjectId); // mengambil data Projects berdasarkan ID

        // Task UpdateProjectsAsync(Projects Projects); // memperbarui data Projects yang sudah ada
        // Task DeleteProjectsAsync(Projects Projects); // menghapus data Projects berdasarkan ID

        // // // Untuk Bulk delete atau hapus banyak data Projects sekaligus dalam satu operasi
        // // Task BulkDeleteProjectsAsync(List<Guid> ProjectsIds); // menghapus banyak data Projects berdasarkan daftar ID
       
    }

}

