using Microsoft.VisualBasic;
using System;

namespace cloud.MVVM.Model
{
    public class CloudFile
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public DateTime Date_add { get; set; } 
        public long Size { get; set; } 
        public byte[] FileData { get; set; } = null!;

        public int UserId { get; set; }  // Klucz obcy do identyfikatora użytkownika
        public User Users { get; set; } = null!; // Właściwość nawigacyjna do użytkownika
    }
}