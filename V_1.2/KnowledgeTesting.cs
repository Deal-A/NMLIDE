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
using Newtonsoft.Json;


namespace V_1._2
{
    public partial class KnowledgeTesting : Form
    {
        private Test test;
        private List<Question> _questions;
        private int currentQuestionIndex = 0;

        private List<RichTextBox> _questionPreviewRTBList;

        private List<object> _currentAnswerControlSet;
        
        private Font _baseRTBFont;

        private Label questionTextLabel;
        private ListBox answersListBox;
        private TextBox directAnswerTextBox;
        private Button nextButton;
        private Button backButton;
        private Button finishButton;
        private ListView questionListView;

        private QuestionImageForm QuestionImageForm;

        private KnowledgeTestProtocolForm KnowledgeTestProtocolForm;

        public delegate void TestFinishedDelegate();

        public event TestFinishedDelegate TestFinished;



        private int totalScore = 0;
        public KnowledgeTesting()
        {
            InitializeComponent();
            _initFields();

            LoadQuestions();

        }

        private void _initFields()
        {
            _questionPreviewRTBList = new List<RichTextBox>();
            test = JsonConvert.DeserializeObject<Test>(File.ReadAllText("D:\\_1Study\\ВКР\\P\\V_1\\V_1.2\\Sources\\KnowledgeTestData_3q.json"));


            _currentAnswerControlSet = new List<object>();


            _baseRTBFont = new Font(
                    new FontFamily("Verdana"),
                    12,
                    FontStyle.Bold,
                    GraphicsUnit.Pixel
                    );

            ///var a = tryJson.Questions[0].Answers.ToArray();

            //test = JsonConvert.DeserializeObject<Test>(File.ReadAllText("D:\\_1Study\\ВКР\\P\\V_1\\V_1.2\\Sources\\TryJson.json"));
            //test = System.Text.Json.JsonSerializer.Deserialize<Test>(File.ReadAllText("D:\\_1Study\\ВКР\\P\\V_1\\V_1.2\\Sources\\KnowledgeTestData.json"));
            _questions = test.Questions;

            KnowledgeTestProtocolForm = new KnowledgeTestProtocolForm();
            KnowledgeTestProtocolForm.Hide();

            KnowledgeTestProtocolForm.NextDelegateClick += FinishTest;

        }

        private void FinishTest()
        {
            Hide();
            KnowledgeTestProtocolForm.Hide();
            QuestionImageForm?.Hide();

            TestFinished();

        }

        private void LoadQuestions()
        {
            foreach (Question question in test.Questions)
            {
                listView1.Items.Add(new ListViewItem(new string[] { question.Number.ToString(), question.QuestionText }));
            }

            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged ;

            _loadQuestions();


            DisplayQuestion(currentQuestionIndex);
        }

        private void _loadQuestions() 
        {
            foreach (Question question in test.Questions)
            {

                RichTextBox richTextBox1 = new RichTextBox();

                StringBuilder sb = new StringBuilder();
                sb.Append(question.Number.ToString() + ". ");
                sb.Append(question.QuestionText);
                richTextBox1.Text = sb.ToString();

                richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
                richTextBox1.WordWrap = true;

                richTextBox1.Size = new Size(200,70);

                richTextBox1.Click += _richTextBox1_Clicked;

                richTextBox1.ReadOnly = true;
                richTextBox1.BorderStyle = BorderStyle.None;

                richTextBox1.Font = _baseRTBFont;

                richTextBox1.BackColor = Color.FromArgb(255,255,255,255);

                richTextBox1.MouseWheel += richTextControl_MouseWheel;

                questionPreviewFlowLayoutPanel.Controls.Add(richTextBox1);

                _questionPreviewRTBList.Add(richTextBox1);

                listView1.Items.Add(new ListViewItem(new string[] { question.Number.ToString(), question.QuestionText }));
            }
        }

        private void _richTextBox1_Clicked(object sender, EventArgs e) 
        {
            int i = _getRTBIndex(((RichTextBox)sender).Text);
            if (-1 == i) 
            {
                return;
            }
            currentQuestionIndex = i;
            DisplayQuestion(i);
        }

        private int _getRTBIndex(string curText) 
        {

            int i = 0;
            foreach (Question curQuestion in _questions) 
            {
                if (curText.Contains(curQuestion.QuestionText)) 
                {
                    return i;
                }

                i++;
            }
            return -1;
        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                currentQuestionIndex = listView1.SelectedItems[0].Index;
                DisplayQuestion(currentQuestionIndex);
            }
        }

        private bool containsIndexInStudentState(int i)
        {
            foreach(var ans in test.Questions[currentQuestionIndex].StudentAnswer)
            {
                if (int.Parse(ans) == i)
                {
                    return true;
                }
            }
            return false;
        }

        private void DisplayQuestion(int index)
        {
            Question question = test.Questions[index];

           
            flowLayoutPanel1.Controls.Clear();

            richTextBox1.Clear();

            QuestionImageForm?.Dispose();
            QuestionImageForm = null;

            richTextBox1.AppendText(question.QuestionText);

            _addImage(question.PictureBase64);


            var answersArr = question.Answers[0];

            _currentAnswerControlSet.Clear();

            int i = 0;
            foreach (Answer answer in question.Answers)
            {
                if (question.Type == "OneFromMulty")
                {

                    RadioButton radioButton = new RadioButton();
                    radioButton.Text = answer.AnswerText;
                    radioButton.Tag = answer.IsRight; 
                    radioButton.Click += RadioButton_Click;

                    // Студент ответил на вопрос.
                    if (question.StudentAnswer.Count > 0)
                    {
                        //Установить если студент выбрал этот вариант
                        radioButton.Checked = (int.Parse(question.StudentAnswer[0]) == i);
                    }

                    flowLayoutPanel1.Controls.Add(radioButton);
                    _currentAnswerControlSet.Add(radioButton);
                }
                else if (question.Type == "MultuFromMulty")
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Text = answer.AnswerText;
                    checkBox.Tag = answer.IsRight;
                    checkBox.Click += CheckBox_Click; ;

                    if (containsIndexInStudentState(i))
                    {
                        checkBox.Checked = true;
                    }

                    //if (question.StudentAnswer.Count > 0 && (i <= question.StudentAnswer.Count - 1))
                    //{
                    //    if (int.Parse(question.StudentAnswer[i]) == i) {
                    //        checkBox.Checked = true;
                    //    }
                    //        //Установить если студент выбрал этот вариант
                    //       // checkBox.Checked = (int.Parse(question.StudentAnswer[i]) == i);
                    //}

                    flowLayoutPanel1.Controls.Add(checkBox);
                    _currentAnswerControlSet.Add(checkBox);
                }
                else if (question.Type == "DirectAnswer")
                {
                    TextBox textBox = new TextBox();
                    textBox.TextChanged += TextBox_TextChanged;

                    flowLayoutPanel1.Controls.Add(textBox);
                    _currentAnswerControlSet.Add(textBox);
                    
                    if (question.StudentAnswer.Count > 0)
                    {
                        textBox.Text = question.StudentAnswer[0];
                    }
                   
                }
                i++;
            }
        }

        private void richTextControl_MouseWheel(object sender, MouseEventArgs e)
        {
            //if (flowLayoutPanel4.ClientRectangle.Contains(flowLayoutPanel4.PointToClient(((RichTextBox)sender).PointToScreen(e.Location))))
            //{
            //    int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            //    flowLayoutPanel4.AutoScrollPosition = new Point(flowLayoutPanel4.AutoScrollPosition.X, flowLayoutPanel4.AutoScrollPosition.Y - numberOfTextLinesToMove * 20);
            //}



            int delta = e.Delta;

            int diff = 20;

            if (delta > 0)
            {
                // Прокручиваем вверх
                questionPreviewFlowLayoutPanel.VerticalScroll.Value = Math.Max(0, questionPreviewFlowLayoutPanel.VerticalScroll.Value - diff);
            }
            else
            {
                // Прокручиваем вниз
                questionPreviewFlowLayoutPanel.VerticalScroll.Value = Math.Min(questionPreviewFlowLayoutPanel.VerticalScroll.Maximum, questionPreviewFlowLayoutPanel.VerticalScroll.Value + diff);
            }

        }

        private void _addImage(string pictureBase64)
        {
            byte[] imageBytes = Convert.FromBase64String(pictureBase64);

            if (0 == imageBytes.Length) 
            {
                pictureBox1.Image = null;
                return;
            }


            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // Загружаем изображение из потока памяти
                Image image = Image.FromStream(ms);

                // Устанавливаем изображение в PictureBox
                pictureBox1.Image = image;
            }
        }

        private void _setAnswerdThisQuestion(int index) 
        {

        //    _baseRTBFont = new Font(
        //new FontFamily("Verdana"),
        //12,
        //FontStyle.Bold,
        //GraphicsUnit.Pixel
        //);

            _questionPreviewRTBList[index].Font = new Font(
                _baseRTBFont.FontFamily,
                _baseRTBFont.Size,
                FontStyle.Regular,
                GraphicsUnit.Pixel
             );

        }

        private void _setNotAnswerdThisQuestion(int index)
        {
            _questionPreviewRTBList[index].Font = _baseRTBFont;
        }


        private void _updatePreviewRTB() 
        {
            int i = 0;
            foreach(Question q in _questions) 
            {
                _setNotAnswerdThisQuestion(i);

                if (q.StudentAnswer.Count != 0)
                {
                    _setAnswerdThisQuestion(i);
                }

                i++;
            }
        }

        #region Обновление внутреннего представления проверки знаний по действию пользователя
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            //todo Несколько полей для каждого ответа
            int i = 0;
            test.Questions[currentQuestionIndex].StudentAnswer.Clear();

            foreach (TextBox control in flowLayoutPanel1.Controls)
            {
                //test.Questions[currentQuestionIndex].StudentAnswer.Clear();

                if (control.Text.Equals("")) 
                {
                    continue;
                }

                test.Questions[currentQuestionIndex].StudentAnswer.Add(control.Text);

                i++;
            }

            _updatePreviewRTB();
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            int i = 0;
            test.Questions[currentQuestionIndex].StudentAnswer.Clear();


            foreach (CheckBox control in flowLayoutPanel1.Controls)
            {
                if (control.Checked)
                {
                    test.Questions[currentQuestionIndex].StudentAnswer.Add(i.ToString());
                }
                i++;
            }

            _updatePreviewRTB();
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            int i = 0;
            test.Questions[currentQuestionIndex].StudentAnswer.Clear();

            foreach (RadioButton control in flowLayoutPanel1.Controls) {

                if (control.Checked) 
                {
                    //test.Questions[currentQuestionIndex].StudentAnswer.Clear();
                    test.Questions[currentQuestionIndex].StudentAnswer.Add(i.ToString());
                }
                i++;
            }

            _updatePreviewRTB();
        }

        #endregion Обновление внутреннего представления теста по действию пользователя

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

        #region Подумать 

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

        #endregion
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
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.LabelWrap = true;

            ImageList imgLst = new ImageList();
            imgLst.ImageSize = new Size(1, 100);
            listView1.SmallImageList = imgLst;

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            QuestionImageForm?.Hide();
            KnowledgeTestProtocolForm?.Hide();
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
            if ( currentQuestionIndex >= _questions.Count-1)
            {
                return;
            }
            currentQuestionIndex++;
            DisplayQuestion(currentQuestionIndex);
        }

        private void finishButton1_Click(object sender, EventArgs e)
        {
            KnowledgeTestProtocolForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_questions[currentQuestionIndex].PictureBase64))
            {
                return;
            }
            if (object.Equals(QuestionImageForm, null)) 
            {
                QuestionImageForm = new QuestionImageForm(Convert.FromBase64String(_questions[currentQuestionIndex].PictureBase64));
                
            }
            QuestionImageForm.Show();
        }

        private void KnowledgeTesting_SizeChanged(object sender, EventArgs e)
        {
            //if (this.Size.Width < 1000)
            //{
            //    flowLayoutPanel2.Size = new Size(Size.Width - 100, 500);
            //    flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            //    flowLayoutPanel3.FlowDirection = FlowDirection.TopDown;
            //}
            //else 
            //{
            //    flowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight;
            //}

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (0 == _currentAnswerControlSet.Count)
            {
                return;
            }

            var firstControl = _currentAnswerControlSet[0];

            if ((typeof(RadioButton) == firstControl.GetType()))
            {
                foreach (var curControl in _currentAnswerControlSet) 
                {
                    ((RadioButton)curControl).Checked = false;
                }
            }

            if ((typeof(CheckBox) == firstControl.GetType())) 
            {
                foreach (var curControl in _currentAnswerControlSet)
                {
                    ((CheckBox)curControl).Checked = false;
                }
            }

            if ((typeof(TextBox) == firstControl.GetType())) 
            {
                ((TextBox)_currentAnswerControlSet[0]).Text = "";
            }

            _clearCurrentStudentAnswer();
            _updatePreviewRTB();
        }

        private void _clearCurrentStudentAnswer()
        {
            test.Questions[currentQuestionIndex].StudentAnswer.Clear();
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

        public List<string> StudentAnswer { get; set; }
    }

    public class Answer
    {
        public string AnswerText { get; set; }

        [JsonIgnore]
        public bool IsRight { get; set; }
    }
}
