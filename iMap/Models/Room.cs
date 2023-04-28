﻿namespace iMap.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AppUser Admin { get; set; }
        public ICollection<Message> Messages { get; set; }
        //public MyLocation Location { get; set; }
    }
}
