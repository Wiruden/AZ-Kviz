using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using az_kviz.ViewModels;

namespace az_kviz.ViewModels
{
    public class AboutViewModel
    {
        // Properties for the About page
        public string Title => "About AZ Kvíz Desktop";
        public string Version => "Version 1.0.4 (Phase 3 Build)";
        public string Author => "Developer: David Mihók";

        public string Description =>
            "This application is a digital recreation of the popular Czech television quiz show 'AZ Kvíz'. " +
            "Developed as a school project for 2026, it features a hexagonal game board, " +
            "integrated timer services, and a localized question database. " +
            "The goal is to connect three sides of the triangular board by answering questions correctly.";

        public string Technologies => "Built using C#, WPF (Windows Presentation Foundation), and the MVVM Pattern.";

        public AboutViewModel()
        {
            
        }
    }
}