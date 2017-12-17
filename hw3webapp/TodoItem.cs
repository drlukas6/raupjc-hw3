using System;
using System.Collections.Generic;

namespace hw3webapp
{
    public class TodoItem
    {
        public Guid Id { get; set; } 
        public string Text { get; set; }
        
        // Shorter syntax for single line getters in C#6 //public bool IsCompleted => DateCompleted.HasValue; 
        public bool IsCompleted {
            get
            {
                return DateCompleted.HasValue; 
                
            }
        }
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        
        public TodoItem(string text) {
            // Generates new unique identifier
            Id = Guid.NewGuid();
            
            // DateTime.Now returns local time, it wont always be what you expect (depending where the server is).
            // We want to use universal (UTC) time which we can easily convert to local when needed.
            // (usually done in browser on the client side)
            DateCreated = DateTime.UtcNow;
            Text = text; 
        }

        public static bool operator ==(TodoItem item1, TodoItem item2)
        {
            return EqualityComparer<TodoItem>.Default.Equals(item1, item2);
        }

        
        public static bool operator !=(TodoItem item1, TodoItem item2)
        {
            return !(item1 == item2);
        }
        
        public override bool Equals(object obj)
        {
            return Equals(obj as TodoItem);
        }

        public bool Equals(TodoItem other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Text == other.Text &&
                   IsCompleted == other.IsCompleted &&
                   EqualityComparer<DateTime?>.Default.Equals(DateCompleted, other.DateCompleted) &&
                   DateCreated == other.DateCreated;
        }


        public override int GetHashCode()
        {
            var hashCode = -555332867;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + IsCompleted.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(DateCompleted);
            hashCode = hashCode * -1521134295 + DateCreated.GetHashCode();
            return hashCode;
        }

        /// <summary >
        /// User id that owns this TodoItem
        /// </ summary >
        public Guid UserId { get; set; }
        /// <summary >
        /// List of labels associated with TodoItem
        /// </ summary >
        public List<TodoItemLabel> Labels { get; set; }
        /// <summary >
        /// Date due . If null , no date was set by the user
        /// </ summary >
        public DateTime? DateDue { get; set; }
        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }
        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }
    }
}