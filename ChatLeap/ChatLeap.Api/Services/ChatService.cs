namespace ChatLeap.Api.Services
{
    public class ChatService
    {
        private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

        public bool AddUsersToList(string userToAdd)
        {
            lock (Users)
            {
                foreach(var user in Users)
                {
                    if (user.Key.ToLower() == userToAdd.ToLower())
                    {
                        return false;
                    }
                }

                Users.Add(userToAdd, null);
                return true;
            }
        }

        public void AddUserByConnectionId(string user, string connectionId)
        {
            lock (Users)
            {
                if(Users.ContainsKey(user))
                {
                    Users[user] = connectionId;
                }
            }
        }

        public string GetUserByConnectionId(string connectionId)
        {
            lock (Users)
            {
                return Users.Where(x => x.Value == connectionId)
                    .Select(p => p.Key).FirstOrDefault();
            }
        }

        public string GetConnectionIdByUser(string user)
        {
            lock (Users)
            {
                return Users.Where(x => x.Key == user)
                    .Select(p => p.Value).FirstOrDefault();
            }
        }

        public void RemoveUserFromList(string user)
        {
            lock (Users)
            {
                if (Users.ContainsKey(user))
                {
                    Users.Remove(user);
                }
            }
        }

        public string[] GetOnlineUsers()
        {
            lock (Users)
            {
                return Users.OrderBy(x => x.Key).Select(k => k.Key).ToArray();
            }
        }
    }
}
