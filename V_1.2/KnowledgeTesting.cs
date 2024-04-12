using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
//using Newtonsoft.Json;



namespace V_1._2
{
    public partial class KnowledgeTesting : Form
    {
        private Test test;
        private List<Question> questions;
        private int currentQuestionIndex = 0;

        private Label questionTextLabel;
        private ListBox answersListBox;
        private TextBox directAnswerTextBox;
        private Button nextButton;
        private Button backButton;
        private Button finishButton;
        private ListView questionListView;

        private QuestionImage QuestionImage;



        private int totalScore = 0;
        public KnowledgeTesting()
        {
            InitializeComponent();

            //test = JsonConvert.DeserializeObject<Test>(File.ReadAllText("D:\\_1Study\\ВКР\\P\\V_1\\V_1.2\\Sources\\KnowledgeTestData.json"));
            test = System.Text.Json.JsonSerializer.Deserialize<Test>(File.ReadAllText("D:\\_1Study\\ВКР\\P\\V_1\\V_1.2\\Sources\\KnowledgeTestData.json"));
            questions = test.Questions;

            //this.flowLayoutPanel1.Controls.Add(listView1);

            LoadQuestions();

            //CreateControls();
            //DisplayQuestion();
        }

        private void LoadQuestions()
        {

            // Заполнение ListView
            foreach (Question question in test.Questions)
            {
                listView1.Items.Add(new ListViewItem(new string[] { question.Number.ToString(), question.QuestionText }));
            }

            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged  ;


            // Отображение первого вопроса
            DisplayQuestion(currentQuestionIndex);
        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                currentQuestionIndex = listView1.SelectedItems[0].Index;
                DisplayQuestion(currentQuestionIndex);
            }
        }

        private void DisplayQuestion(int index)
        {
            Question question = test.Questions[index];

            // Очистка FlowLayoutPanel
            flowLayoutPanel1.Controls.Clear();

            richTextBox1.Lines[0] = question.QuestionText;

            var answersArr = question.Answers.ToArray();


            for (int i = 0; i< answersArr.Length; i++)
            {
                if (question.Type == "OneFromMulty")
                {
                    RadioButton radioButton = new RadioButton();
                    radioButton.Text = question.Answers[i].AnswerText;
                    radioButton.Tag = question.Answers[i].IsRight; // Сохраняем информацию о правильном ответе
                    flowLayoutPanel1.Controls.Add(radioButton);
                }
                else if (question.Type == "MultuFromMulty")
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Text = question.Answers[i].AnswerText;
                    checkBox.Tag = question.Answers[i].IsRight; // Сохраняем информацию о правильном ответе
                    flowLayoutPanel1.Controls.Add(checkBox);
                }
                else if (question.Type == "DirectAnswer")
                {
                    TextBox textBox = new TextBox();
                    flowLayoutPanel1.Controls.Add(textBox);
                }
            }

            //// Отображение ответов в зависимости от типа вопроса
            //foreach (Answer answer in question.Answers)
            //{

            //}
        }

        //private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (listView1.SelectedItems.Count > 0)
        //    {
        //        currentQuestionIndex = listView1.SelectedItems[0].Index;
        //        DisplayQuestion(currentQuestionIndex);
        //    }
        //}

        private void buttonNext_Click(object sender, EventArgs e)
        {
            UpdateScore(); // Обновляем баллы перед переходом к следующему вопросу

            if (currentQuestionIndex < test.QuestionCount - 1)
            {
                currentQuestionIndex++;
                DisplayQuestion(currentQuestionIndex);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            UpdateScore(); // Обновляем баллы перед переходом к предыдущему вопросу

            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayQuestion(currentQuestionIndex);
            }
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            UpdateScore(); // Обновляем баллы перед завершением тестирования

            MessageBox.Show($"Ваш итоговый балл: {totalScore}/{test.QuestionCount}");

            // Дополнительные действия при завершении тестирования
        }

        private void UpdateScore()
        {
            Question question = test.Questions[currentQuestionIndex];

            if (question.Type == "OneFromMulty")
            {
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is RadioButton radioButton && radioButton.Checked)
                    {
                        bool isRight = (bool)radioButton.Tag;
                        if (isRight)
                        {
                            totalScore++;
                        }
                        break; // Выходим из цикла после проверки одного ответа
                    }
                }
            }
            else if (question.Type == "MultuFromMulty")
            {
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is CheckBox checkBox)
                    {
                        bool isRight = (bool)checkBox.Tag;
                        if ((isRight && checkBox.Checked) || (!isRight && !checkBox.Checked))
                        {
                            totalScore++;
                        }
                    }
                }
            }
        }



        //private void CreateControls()
        //{
        //    questionTextLabel = new Label
        //    {
        //        Location = new System.Drawing.Point(10, 10),
        //        AutoSize = true
        //    };
        //    flowLayoutPanel1.Controls.Add(questionTextLabel);

        //    answersListBox = new ListBox
        //    {
        //        Location = new System.Drawing.Point(10, 40),
        //        Size = new System.Drawing.Size(200, 100)
        //    };
        //    answersListBox.SelectedIndexChanged += (sender, e) =>
        //    {
        //        // Дополнительная логика для выбора ответов
        //    };
        //    flowLayoutPanel1.Controls.Add(answersListBox);

        //    directAnswerTextBox = new TextBox
        //    {
        //        Location = new System.Drawing.Point(10, 40),
        //        Size = new System.Drawing.Size(200, 20),
        //        Visible = false
        //    };
        //    flowLayoutPanel1.Controls.Add(directAnswerTextBox);

        //    nextButton = new Button
        //    {
        //        Text = "Next",
        //        Location = new System.Drawing.Point(10, 150)
        //    };
        //    nextButton.Click += nextButton_Click;
        //    flowLayoutPanel1.Controls.Add(nextButton);

        //    backButton = new Button
        //    {
        //        Text = "Back",
        //        Location = new System.Drawing.Point(80, 150)
        //    };
        //    backButton.Click += backButton_Click;
        //    flowLayoutPanel1.Controls.Add(backButton);

        //    finishButton = new Button
        //    {
        //        Text = "Finish",
        //        Location = new System.Drawing.Point(150, 150)
        //    };
        //    finishButton.Click += finishButton_Click;
        //    flowLayoutPanel1.Controls.Add(finishButton);

        //    //    questionListView = new ListView
        //    //    {
        //    //        Location = new System.Drawing.Point(220, 10),
        //    //        Size = new System.Drawing.Size(150, 200),
        //    //        View = View.Details,
        //    //        Columns = { "Number", "Question" }
        //    //    };
        //    //    foreach (var question in questions)
        //    //    {
        //    //        questionListView.Items.Add(new ListViewItem(new string[] { question.Number.ToString(), question.QuestionText }));
        //    //    }
        //    //    flowLayoutPanel1.Controls.Add(questionListView);

        //    //    listView1.SelectedIndexChanged += (sender, e) =>
        //    //    {
        //    //        if (listView1.SelectedItems.Count > 0)
        //    //        {
        //    //            Question selectedQuestion = questions[listView1.SelectedItems[0].Index];
        //    //            MessageBox.Show(selectedQuestion.QuestionText);
        //    //        }
        //    //    };

        //    //}

        //    //private void DisplayQuestion()
        //    //{
        //    //    Question currentQuestion = questions[currentQuestionIndex];
        //    //    questionTextLabel.Text = currentQuestion.QuestionText;

        //    //    answersListBox.Items.Clear();
        //    //    foreach (var answer in currentQuestion.Answers)
        //    //    {
        //    //        answersListBox.Items.Add(answer.AnswerText);
        //    //    }

        //    //    if (currentQuestion.Type == "OneFromMulty")
        //    //    {
        //    //        answersListBox.SelectionMode = SelectionMode.One;
        //    //    }
        //    //    else if (currentQuestion.Type == "MultuFromMulty")
        //    //    {
        //    //        answersListBox.SelectionMode = SelectionMode.MultiSimple;
        //    //    }
        //    //    else if (currentQuestion.Type == "DirectAnswer")
        //    //    {
        //    //        answersListBox.Visible = false;

        //    //        directAnswerTextBox.Visible = true;
        //    //    }
        //    //}
        //}


        private void finishButton_Click(object sender, EventArgs e)
        {
            // Дополнительная логика для завершения тестирования
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void KnowledgeTesting_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void backButton1_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex <= 0) {
                return;
            }
            
            currentQuestionIndex--;
            DisplayQuestion(currentQuestionIndex);
        }

        private void nextButton1_Click(object sender, EventArgs e)
        {
            if ( currentQuestionIndex >= questions.Count-1)
            {
                return;
            }
            currentQuestionIndex++;
            DisplayQuestion(currentQuestionIndex);
        }

        private void finishButton1_Click(object sender, EventArgs e)
        {
            
        }

        private int _qetQuestionImage() {
            return 0;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            QuestionImage = new QuestionImage(_qetQuestionImage());
        }
    }

    public class Test
    {
        public int QuestionCount { get; set; }
        public int TimeSeconds { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public string QuestionText { get; set; }
        public string PictureBase64 { get; set; }
        public List<Answer> Answers { get; set; }

        //public string StudentAnswer { get; set; }
    }

    public class Answer
    {
        public string AnswerText { get; set; }
        public bool IsRight { get; set; }
    }
}
