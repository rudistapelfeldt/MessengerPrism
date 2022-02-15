using System;
using MessengerPrism.Constants;
using SQLite;
using MessengerPrism.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MessengerPrism.Sqlite
{
    public class ItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ItemDatabase> Instance = new AsyncLazy<ItemDatabase>(async () =>
        {
            var instance = new ItemDatabase();
            await Database.CreateTableAsync<Group>();
            await Database.CreateTableAsync<Image>();
            await Database.CreateTableAsync<Text>();
            return instance;
        });

        public ItemDatabase()
        {
            Database = new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        }

        // Group
        public Task<List<Group>> GetGroupAsync()
        {
            return Database.Table<Group>().ToListAsync();
        }

        public Task<Group> GetItemAsync(string id)
        {
            return Database.Table<Group>().Where(i => i.GroupId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveGroupAsync(Group item)
        {
            if (string.IsNullOrEmpty(item.GroupId))
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Group item)
        {
            return Database.DeleteAsync(item);
        }

        // Text
        public Task<List<Text>> GetTextAsync()
        {
            return Database.Table<Text>().ToListAsync();
        }

        public Task<List<Text>> GetTextByMsgIdAsync(string id)
        {
            return Database.Table<Text>().Where(i => i.MsgId == id).ToListAsync();
        }

        public Task<List<Text>> GetTextByGroupIdAsync(string id)
        {
            return Database.Table<Text>().Where(i => i.GroupId == id).ToListAsync();
        }

        public Task<int> SaveTextAsync(Text item)
        {
            if (string.IsNullOrEmpty(item.MsgId))
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteTextAsync(Text item)
        {
            return Database.DeleteAsync(item);
        }

        // Image
        public Task<List<Image>> GetImageAsync()
        {
            return Database.Table<Image>().ToListAsync();
        }

        public Task<List<Image>> GetImageByMsgIdAsync(string id)
        {
            return Database.Table<Image>().Where(i => i.MsgId == id).ToListAsync();
        }

        public Task<List<Image>> GetImageByGroupIdAsync(string id)
        {
            return Database.Table<Image>().Where(i => i.GroupId == id).ToListAsync();
        }

        public Task<int> SaveImageAsync(Image item)
        {
            if (string.IsNullOrEmpty(item.MsgId))
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteImageAsync(Image item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
