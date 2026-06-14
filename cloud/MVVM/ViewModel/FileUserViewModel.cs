using cloud.Core;
using cloud.MVVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace cloud.MVVM.ViewModel
{
    class FileUserViewModel : ObservableObject
    {
        private List<CloudFile> cloudFiles;
        public List<CloudFile> CloudFiles
        {
            get { return cloudFiles; }
            set
            {
                cloudFiles = value;
                OnPropertyChanged(nameof(CloudFiles));
            }
        }

        private int _userid;

        public int Userid
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
                OnPropertyChanged();
            }
        }

        private int _idtodom;

        public int IdToDOM
        {
            get 
            { 
                return _idtodom;
            }
            set 
            { 
                _idtodom = value;
                OnPropertyChanged(nameof(IdToDOM));
            }
        }

        private string _newname;

        public string NewName
        {
            get 
            { 
                return _newname; 
            }
            set { 
                _newname = value; 
                OnPropertyChanged(nameof(NewName));
            }
        }

        private string _alertfile;

        public string AlertFile
        {
            get 
            { 
                return _alertfile; 
            }
            set
            {
                _alertfile = value;
                OnPropertyChanged(nameof(AlertFile));
            }
        }

        private Brush _alertFileForeground;

        public Brush AlertFileForeground
        {
            get { return _alertFileForeground; }
            set
            {
                _alertFileForeground = value;
                OnPropertyChanged(nameof(AlertFileForeground));
            }
        }



        public RelayCommand AddFIleCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand ModificationCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand DownloadCommand { get;set; }

        public FileUserViewModel(int id)
        {

            Userid = id;
            ShowFile();
            AddFIleCommand = new RelayCommand(AddFile);
            RefreshCommand = new RelayCommand(o => ShowFile());
            ModificationCommand = new RelayCommand(ModFile);
            DeleteCommand = new RelayCommand(DelFile);
            DownloadCommand = new RelayCommand(DownloadFIle);
        }

        private void AddFile(object parametr)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Pliki tekstowe (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                string fileName = Path.GetFileName(filePath);
                long fileSize = new FileInfo(filePath).Length;
                byte[] fileData = File.ReadAllBytes(filePath);

                using CloudFileContext context = new CloudFileContext();

                CloudFile file = new CloudFile()
                {
                    FileName = fileName,
                    Date_add = DateTime.Now,
                    Size = fileSize,
                    FileData = fileData,
                    UserId = Userid

                };

                try
                {

                    context.Add(file);
                    context.SaveChanges();
                    AlertFile = "Dodano do bazy";
                    AlertFileForeground = Brushes.Green;
                }
                catch (Exception ex)
                {
                    AlertFile = "Coś się wyjebało: " +ex.Message;
                    AlertFileForeground = Brushes.Red;
                }


            }
        }

        private void ShowFile()
        {
            using (CloudFileContext context = new CloudFileContext())
            {
                var files = context.CloudFiles.Where(f => f.UserId == Userid).ToList();
                CloudFiles = new List<CloudFile>(files);
            }
        }

        private void ModFile(object parametr)
        {
            if ( !string.IsNullOrEmpty(NewName)) {
                using CloudFileContext context = new CloudFileContext();

                var file = context.CloudFiles.Where(f => f.Id == IdToDOM).FirstOrDefault();

                if (file != null)
                {
                    if (file is CloudFile)
                    {
                        //MessageBox.Show(IdToDOM.ToString() + NewName);
                        file.FileName = NewName;
                        AlertFile = "Zmieniono nazwe: " + IdToDOM;
                        AlertFileForeground = Brushes.Green;
                        context.SaveChanges();
                        ShowFile();
                        NewName = "";
                        IdToDOM = 0;
                        
                    }
                }
                else
                {
                    AlertFile = "Podaj odpowiedni numer indeksu";
                    AlertFileForeground = Brushes.Red;
                }
            }else
            {
                AlertFile = "Wypełnij nową nazwe!";
                AlertFileForeground = Brushes.Red;
            }
        }

        private void DelFile(object parametr)
        {
            using CloudFileContext context = new CloudFileContext();

            var file = context.CloudFiles.Where(f => f.Id == IdToDOM).FirstOrDefault();

            if (file != null)
            {
                if (file is CloudFile)
                {
                    //MessageBox.Show(IdToDOM.ToString());
                    AlertFile = "Usunięto plik z bazy: " + IdToDOM;
                    AlertFileForeground = Brushes.Green;
                    context.Remove(file);
                    context.SaveChanges();
                    ShowFile();
                    IdToDOM = 0;
                    
                }
            }
            else
            {
                AlertFile = "Podaj odpowiedni numer indeksu";
                AlertFileForeground = Brushes.Red;
            }
        }

        private void DownloadFIle(object parametr)
        {
            using CloudFileContext context = new CloudFileContext();

            var file = context.CloudFiles.Where(f => f.Id == IdToDOM).FirstOrDefault();

            if (file != null)
            {
                if (file is CloudFile)
                {
                    //MessageBox.Show(IdToDOM.ToString());

                    var saveFileDialog = new SaveFileDialog();

                    saveFileDialog.FileName = file.FileName;

                    if ( saveFileDialog.ShowDialog() == true )
                    {
                        byte[] fileData = file.FileData;

                        string filePath = saveFileDialog.FileName;

                        File.WriteAllBytes(filePath, fileData );

                        AlertFile = "Pobrano plik o id: " + IdToDOM;
                        AlertFileForeground = Brushes.Green;
                        IdToDOM = 0;

                    }
                    else
                    {
                        AlertFile = "anulowano pobieranie pliku";
                        AlertFileForeground = Brushes.Red;
                    }
                }
            }
            else
            {
                AlertFile = "Podaj odpowiedni numer indeksu";
                AlertFileForeground = Brushes.Red;

            }
        }


        
    }
    
}
