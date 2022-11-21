using System;
using System.Collections.Generic;
using System.Text;
using Ejercicio2_4.Models;
using SQLite;
using System.Threading.Tasks;

namespace Ejercicio2_4.Controllers
{
    public class VideoDB
    {
        readonly SQLiteAsyncConnection databaseVideo;

        public VideoDB(string pathdb)
        {
            databaseVideo = new SQLiteAsyncConnection(pathdb);
            databaseVideo.CreateTableAsync<VideoModel>().Wait();
        }

        //Metodo para guardar video
        public Task<int> SaveVideoRecord(VideoModel videos)
        {
            if (videos.Id != 0)
            {
                return databaseVideo.UpdateAsync(videos);
            }
            else
            {
                return databaseVideo.InsertAsync(videos);
            }
        }

        //Mostrar lista de videos guardados
        public Task<List<VideoModel>> GetVideoList()
        {
            return databaseVideo.Table<VideoModel>().ToListAsync();
        }

        public Task<VideoModel> GetVideoForId(int pcodigo)
        {
            return databaseVideo.Table<VideoModel>()
                .Where(i => i.Id == pcodigo)
                .FirstOrDefaultAsync();
        }



        //Eliminar videos
        /*
        public Task<int> DeleteVideo(VideoModel videos)
        {
            return databaseVideo.DeleteAsync(videos);
        }
        */
    }
}


