using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

public partial class Form1 : Form
{
    private Test test;
    private int currentQuestionIndex = 0;
    private int totalScore = 0;

    public Form1()
    {
        InitializeComponent();
        LoadQuestions();
    }

    private void LoadQuestions()
    {
        string filePath = "path/to/your/file.json";
        string json = File.ReadAllText(filePath);
        test = JsonConvert.DeserializeObject<Test>(json);

        // Заполнение ListView
        foreach (Question question in test.Questions)
        {
            listView1.Items.Add(question.Number.ToString());
        }

        // Отображение первого вопроса
        DisplayQuestion(currentQuestionIndex);
    }

    private void DisplayQuestion(int index)
    {
        Question question = test.Questions[index];
        
        // Очистка FlowLayoutPanel
        flowLayoutPanel1.Controls.Clear();

        // Отображение текста вопроса
        Label questionLabel = new Label();
        questionLabel.Text = question.QuestionText;
        flowLayoutPanel1.Controls.Add(questionLabel);

        // Отображение ответов в зависимости от типа вопроса
        foreach (Answer answer in question.Answers)
        {
            if (question.Type == "OneFromMulty")
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = answer.AnswerText;
                radioButton.Tag = answer.IsRight; // Сохраняем информацию о правильном ответе
                flowLayoutPanel1.Controls.Add(radioButton);
            }
            else if (question.Type == "MultuFromMulty")
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = answer.AnswerText;
                checkBox.Tag = answer.IsRight; // Сохраняем информацию о правильном ответе
                flowLayoutPanel1.Controls.Add(checkBox);
            }
            else if (question.Type == "DirectAnswer")
            {
                TextBox textBox = new TextBox();
                flowLayoutPanel1.Controls.Add(textBox);
            }
        }
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 0)
        {
            currentQuestionIndex = listView1.SelectedItems[0].Index;
            DisplayQuestion(currentQuestionIndex);
        }
    }

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
}

public class Test
{
    public int QuestionCount { get; set; }
    public List<Question> Questions { get; set; }
}

public class Question
{
    public int Number { get; set; }
    public string Type { get; set; }
    public string QuestionText { get; set; }
    public List<Answer> Answers { get; set; }
}

public class Answer
{
    public string AnswerText { get; set; }
    public bool IsRight { get; set; }
}