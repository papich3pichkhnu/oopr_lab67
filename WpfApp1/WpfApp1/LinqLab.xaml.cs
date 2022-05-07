using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LinqLab.xaml
    /// </summary>
    public partial class LinqLab : Window
    {
        public LinqLab()
        {
            InitializeComponent();
        }

        
        private void WhereButton_Click(object sender, RoutedEventArgs e)
        {
            using(ApplicationContext context = new ApplicationContext())
            {
                var whereLinq = from s in context.Students
                                where s.Sex == "Female"
                                select s;
                StudentsQueryList.Items.Clear();

                foreach(var s in whereLinq)
                {
                    StudentsQueryList.Items.Add(s);
                }
            }
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var groupLinq = from s in context.Students.ToList()
                                group s by s.Sex into g
                                select new
                                {
                                    Name = g.Key,
                                    Count = g.Count(),
                                    Students = from p in g select p
               
                                };
                                

                StudentsQueryList.Items.Clear();

                foreach (var studgroups in groupLinq)
                {
                    foreach(var stud in studgroups.Students)
                    {
                        StudentsQueryList.Items.Add(stud);
                    }
                    MessageBox.Show(studgroups.Name + " Count = "+studgroups.Count);

                }
                

            }
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var joinLinq = from s in context.Students
                                join f in context.Faculties
                                on s.FacultyId equals f.Id
                                select new
                                {
                                    s,
                                    f
                                };


                StudentsQueryList.Items.Clear();

                foreach(var s in joinLinq)
                {
                    StudentsQueryList.Items.Add(new
                    {
                        LastName = s.s.LastName,
                        FirstName = s.s.FirstName,
                        MiddleName = s.s.MiddleName,
                        Sex = s.s.Sex,
                        FacultyName = s.f.Name
                    });
                }


            }

        }

        private void ExtensionButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var whereLinq = context.Students.Where(s => s.Sex == "Female");

                StudentsQueryList.Items.Clear();
                foreach (var s in whereLinq)
                {
                    StudentsQueryList.Items.Add(s);
                }
            }
        }

        private void ExtensionCountButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var whereCountLinq = context.Students.Where(s => s.Sex == "Male").Count();

                MessageBox.Show("Male count = " + whereCountLinq);
                StudentsQueryList.Items.Clear();                
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var orderLinq = context.Students.OrderBy(p => p.LastName);

                StudentsQueryList.Items.Clear();
                foreach (var s in orderLinq)
                {
                    StudentsQueryList.Items.Add(s);
                }

            }
        }

        private void UnionButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var unionLinq = context.Students.Where(x=>x.Sex=="Male").
                                        Union(context.Students.Where(x=>x.Sex=="Female"));

                StudentsQueryList.Items.Clear();
                foreach (var s in unionLinq)
                {
                    StudentsQueryList.Items.Add(s);
                }

            }
        }

        private void IntersectButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var unionLinq = context.Students.Where(x => x.Sex == "Male").
                                        Intersect(context.Students.Where(x => x.FacultyId == 5));

                StudentsQueryList.Items.Clear();
                foreach (var s in unionLinq)
                {
                    StudentsQueryList.Items.Add(s);
                }

            }
        }

        private void ExceptButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var unionLinq = context.Students.Where(x => x.Sex == "Male").
                                        Except(context.Students.Where(x => x.FacultyId == 5));

                StudentsQueryList.Items.Clear();
                foreach (var s in unionLinq)
                {
                    StudentsQueryList.Items.Add(s);
                }

            }
        }
    }
}
