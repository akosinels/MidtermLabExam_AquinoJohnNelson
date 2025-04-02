using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MidtermLabExam_AquinoJohnNelson
{
    public partial class StudentPage_Individual : Form
    {
        // stores the selected student ID
        private int SelectedStudentId;
        // stores the connection string to the database
        private string ConnectionString;

        public StudentPage_Individual(int studentId, string connectionString)
        {
            InitializeComponent(); // initialize the form components
            this.SelectedStudentId = studentId; // set the selected student ID
            this.ConnectionString = connectionString; // set the connection string
        }

        private void StudentPage_Individual_Load(object sender, EventArgs e)
        {
            loadStudentDetails(); // load student details when the form loads
        }

        private void loadStudentDetails()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                // query to select student details along with course and year information
                string query = @"
                    SELECT s.*, c.courseName, y.yearLvl
                    FROM StudentRecordTB s
                    JOIN CourseTB c ON s.courseId = c.courseId
                    JOIN YearTB y ON s.yearId = y.yearId
                    WHERE s.studentId = @studentId";

                MySqlCommand command = new MySqlCommand(query, connection);

                // add the selected student ID as a parameter to the query
                command.Parameters.AddWithValue("@studentId", this.SelectedStudentId);

                try
                {
                    connection.Open(); // open the database connection
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // set the student ID label
                            StudentIdValueLabel.Text = reader["studentId"].ToString();

                            // get and set the student's full name
                            string firstName = reader["firstName"].ToString();
                            string lastName = reader["lastName"].ToString();
                            string middleName = reader.IsDBNull(reader.GetOrdinal("middleName")) ? "" : reader["middleName"].ToString();
                            FullNameValueLabel.Text = $"{lastName}, {firstName} {middleName}".Trim();

                            // get and set the student's address
                            string houseNo = reader.IsDBNull(reader.GetOrdinal("houseNo")) ? "" : reader["houseNo"].ToString();
                            string brgy = reader["brgyName"].ToString();
                            string municipality = reader["municipality"].ToString();
                            string province = reader["province"].ToString();
                            string region = reader["region"].ToString();
                            string country = reader["country"].ToString();
                            AddressValueLabel.Text = $"{houseNo} {brgy}, {municipality}, {province}, {region}, {country}"
                                                      .Replace("  ", " ")
                                                      .TrimStart(' ');

                            // set the student's birthdate, age, contact number, and email address
                            BirthdateValueLabel.Text = reader["birthdate"].ToString();
                            AgeValueLabel.Text = reader["age"].ToString();
                            ContactValueLabel.Text = reader["studContactNo"].ToString();
                            EmailValueLabel.Text = reader["emailAddress"].ToString();

                            // get and set the guardian's full name
                            string guardFirstName = reader["guardianFirstName"].ToString();
                            string guardLastName = reader["guardianLastName"].ToString();
                            GuardianValueLabel.Text = $"{guardFirstName} {guardLastName}";

                            // set the student's hobbies and nickname
                            HobbiesValueLabel.Text = reader["hobbies"].ToString();
                            NicknameValueLabel.Text = reader["nickname"].ToString();

                            // set the student's course and year level
                            CourseValueLabel.Text = reader["courseName"].ToString();
                            YearValueLabel.Text = reader["yearLvl"].ToString();
                        }
                        else
                        {
                            // show message if the student is not found and close the form
                            MessageBox.Show($"Student with ID {this.SelectedStudentId} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
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
    }
}
