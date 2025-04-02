using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MidtermLabExam_AquinoJohnNelson;
using MySql.Data.MySqlClient;

namespace MidtermLabExam_AquinoJohnNelson
{
    public partial class Student_Page : Form
    {
        // connection string to connect to the MySQL database
        private string ConnectionString = "Server=localhost;Database=StudentInfoDB;Uid=root;Pwd=;";

        public Student_Page()
        {
            InitializeComponent(); // initialize the form components
        }

        private void Student_Page_Load(object sender, EventArgs e)
        {
            loadStudents(); // load student records when the form loads
        }

        private void loadStudents()
        {
            StudentsFlowPanel.Controls.Clear(); // clear existing controls in the flow panel
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                // query to select student records ordered by last name and first name
                string query = "SELECT studentId, firstName, lastName, middleName FROM StudentRecordTB ORDER BY lastName, firstName";
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open(); // open the database connection
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            // show message if no student records are found
                            MessageBox.Show("No student records found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        while (reader.Read())
                        {
                            // read student details from the database
                            int studentId = reader.GetInt32("studentId");
                            string firstName = reader.GetString("firstName");
                            string lastName = reader.GetString("lastName");
                            string middleName = reader.IsDBNull(reader.GetOrdinal("middleName")) ? "" : reader.GetString("middleName");

                            // format the full name of the student
                            string fullName = $"{lastName}, {firstName} {middleName}".Trim();

                            // create a new student entry in the flow panel
                            createStudentEntry(studentId, fullName);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // show message if a database error occurs
                    MessageBox.Show($"Database Error: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // show message if an unexpected error occurs
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void createStudentEntry(int studentId, string fullName)
        {
            // create a new panel for the student entry
            Panel studentPanel = new Panel
            {
                Width = StudentsFlowPanel.ClientSize.Width - 25,
                Height = 35,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(3)
            };

            // create a label to display the student's ID and full name
            Label studentLabel = new Label
            {
                Text = $"{studentId} - {fullName}",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(5, 8),
                Width = studentPanel.Width - 90
            };

            // create a button to view the student's details
            Button viewButton = new Button
            {
                Text = "VIEW",
                Tag = studentId,
                Location = new Point(studentPanel.Width - 80, 5),
                Size = new Size(70, 25)
            };

            // add click event handler for the view button
            viewButton.Click += viewButton_Click;

            // add the label and button to the student panel
            studentPanel.Controls.Add(studentLabel);
            studentPanel.Controls.Add(viewButton);

            // add the student panel to the flow panel
            StudentsFlowPanel.Controls.Add(studentPanel);
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag != null)
            {
                // get the student ID from the button's tag
                int selectedStudentId = (int)clickedButton.Tag;

                // open the individual student detail form
                StudentPage_Individual studentDetailForm = new StudentPage_Individual(selectedStudentId, ConnectionString);
                studentDetailForm.ShowDialog();
            }
        }
    }
}