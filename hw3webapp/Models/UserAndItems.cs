using System.Collections.Generic;

namespace hw3webapp.Models
{
    public class UserAndItems
    {
        public User user;
        public List<TodoItem> items;

        public UserAndItems(User user, List<TodoItem> items)
        {
            this.user = user;
            this.items = items;
        }
    }
}