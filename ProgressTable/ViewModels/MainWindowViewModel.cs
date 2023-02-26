using Avalonia.Controls;
using Avalonia.Media;
using ProgressTable.Models;
using ReactiveUI;
using System;
using System.Reactive;


namespace ProgressTable.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Student[] students;

        private SolidColorBrush checkColor(float num)
        {
            if (num < 1) return new SolidColorBrush(Colors.Red);
            if (num < 1.5) return new SolidColorBrush(Colors.Yellow);
            else return new SolidColorBrush(Colors.Green);
        }

        private void CheckAverage(Student[] students)
        {
            for (int i = 0; i < 8; i++)
            {
                sc_scores[i] = 0;
            }
            for (int i = 0; i < students.Length; i++)
            {
                ScoreVisualSr += students[i].Visual;
                ScoreProbabilityTheorySr += students[i].ProbabilityTheory;
                ScoreEESSr += students[i].EES;
                ScoreCompAnalysisSr += students[i].CompAnalysis;
                ScoreArchitecComputerSr += students[i].ArchitecComputer;
                ScoreMathAnalysisSr += students[i].MathAnalysis;
                ScoreNetworcComputerSr += students[i].NetworcComputer;
                ScoreAverageSr += students[i].Average_Score;
            }
            ScoreVisualSr /= students.Length;
            ColorVisualSr = checkColor(ScoreVisualSr);
            ScoreProbabilityTheorySr /= students.Length;
            ColorProbabilityTheorySr = checkColor(ScoreProbabilityTheorySr);
            ScoreEESSr /= students.Length;
            ColorEESSr = checkColor(ScoreEESSr);
            ScoreCompAnalysisSr /= students.Length;
            ColorCompAnalysisSr = checkColor(ScoreCompAnalysisSr);
            ScoreArchitecComputerSr /= students.Length;
            ColorArchitecComputerSr = checkColor(ScoreArchitecComputerSr);
            ScoreMathAnalysisSr /= students.Length;
            ColorMathAnalysisSr = checkColor(ScoreMathAnalysisSr);
            ScoreNetworcComputerSr /= students.Length;
            ColorNetworcComputerSr = checkColor(ScoreNetworcComputerSr);
            ScoreAverageSr /= students.Length;
            ColorAverageSr = checkColor(ScoreAverageSr);
        }

        public MainWindowViewModel()
        {
            AddStudent = ReactiveCommand.Create(() =>
            {
                if (newName != "")
                {
                    Student[] temp = students;
                    Array.Resize(ref temp, temp.Length + 1);
                    temp[temp.Length - 1] = new Student { Name = newName, Visual = scores[0], ProbabilityTheory = scores[1], EES = scores[2], CompAnalysis = scores[3], 
                                                          ArchitecComputer = scores[4], MathAnalysis = scores[5], NetworcComputer = scores[6] };
                    Students = temp;
                    NewName = "";
                    ScoreVisual = 0;
                    ScoreProbabilityTheory = 0;
                    ScoreEES = 0;
                    ScoreCompAnalysis = 0;
                    ScoreArchitecComputer = 0;
                    ScoreMathAnalysis = 0;
                    ScoreNetworcComputer = 0;
                    CheckAverage(students);
                }
            });
            DeleteStudent = ReactiveCommand.Create(() =>
            {
                if (index < students.Length)
                {
                    Student[] temp = students;
                    for (int i = index; i < temp.Length - 1; i++)
                    {
                        temp[i] = temp[i + 1];
                    }
                    Array.Resize(ref temp, temp.Length - 1);
                    Students = temp;
                    Index = 5000;
                    CheckAverage(students);
                }
            });
            Save = ReactiveCommand.Create(() =>
            {
                Serializer<Student[]>.Save("data.dat", students);
            });
            Load = ReactiveCommand.Create(() =>
            {
                Students = Serializer<Student[]>.Load("data.dat");
                CheckAverage(students);
            });
            Students = new Student[]
            {
                new Student{Name="Дорохов Денис Иванович", Visual=2, ProbabilityTheory=1, EES=1, CompAnalysis=2, ArchitecComputer=2, MathAnalysis=1, NetworcComputer=2},
                new Student{Name="Шастун Антон Андреевич", Visual=2, ProbabilityTheory=1, EES=0, CompAnalysis=0, ArchitecComputer=1, MathAnalysis=2, NetworcComputer=1},
                new Student{Name="Позов Дмитрий Темурович", Visual=2, ProbabilityTheory=2, EES=2, CompAnalysis=0, ArchitecComputer=2, MathAnalysis=1, NetworcComputer=2},
            };
            CheckAverage(students);

        }

        public Student[] Students
        {
            get => students;
            set => this.RaiseAndSetIfChanged(ref students, value);
        }

        public ReactiveCommand<Unit, Unit> AddStudent { get; }
        public ReactiveCommand<Unit, Unit> DeleteStudent { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public ReactiveCommand<Unit, Unit> Load { get; }

        private ushort[] scores = { 0, 0, 0, 0, 0, 0, 0 };
        private string newName = "";
        private ushort index = 5000;
        private float[] sc_scores = { 0, 0, 0, 0, 0, 0, 0, 0 };
        private SolidColorBrush[] colorBrush = new SolidColorBrush[8];
        public ushort Index { get => index; set => this.RaiseAndSetIfChanged(ref index, value); }
        public string NewName { get => newName; set => this.RaiseAndSetIfChanged(ref newName, value); }
        public ushort ScoreVisual { get => scores[0]; set => this.RaiseAndSetIfChanged(ref scores[0], value); }
        public ushort ScoreProbabilityTheory { get => scores[1]; set => this.RaiseAndSetIfChanged(ref scores[1], value); }
        public ushort ScoreEES { get => scores[2]; set => this.RaiseAndSetIfChanged(ref scores[2], value); }
        public ushort ScoreCompAnalysis { get => scores[3]; set => this.RaiseAndSetIfChanged(ref scores[3], value); }
        public ushort ScoreArchitecComputer { get => scores[4]; set => this.RaiseAndSetIfChanged(ref scores[4], value); }
        public ushort ScoreMathAnalysis { get => scores[5]; set => this.RaiseAndSetIfChanged(ref scores[5], value); }
        public ushort ScoreNetworcComputer { get => scores[6]; set => this.RaiseAndSetIfChanged(ref scores[6], value); }

        public float ScoreVisualSr { get => sc_scores[0]; set => this.RaiseAndSetIfChanged(ref sc_scores[0], value); }
        public float ScoreProbabilityTheorySr { get => sc_scores[1]; set => this.RaiseAndSetIfChanged(ref sc_scores[1], value); }
        public float ScoreEESSr { get => sc_scores[2]; set => this.RaiseAndSetIfChanged(ref sc_scores[2], value); }
        public float ScoreCompAnalysisSr { get => sc_scores[3]; set => this.RaiseAndSetIfChanged(ref sc_scores[3], value); }
        public float ScoreArchitecComputerSr { get => sc_scores[4]; set => this.RaiseAndSetIfChanged(ref sc_scores[4], value); }
        public float ScoreMathAnalysisSr { get => sc_scores[5]; set => this.RaiseAndSetIfChanged(ref sc_scores[5], value); }
        public float ScoreNetworcComputerSr { get => sc_scores[6]; set => this.RaiseAndSetIfChanged(ref sc_scores[6], value); }
        public float ScoreAverageSr { get => sc_scores[7]; set => this.RaiseAndSetIfChanged(ref sc_scores[7], value); }

        public SolidColorBrush ColorVisualSr { get => colorBrush[0]; set => this.RaiseAndSetIfChanged(ref colorBrush[0], value); }
        public SolidColorBrush ColorProbabilityTheorySr { get => colorBrush[1]; set => this.RaiseAndSetIfChanged(ref colorBrush[1], value); }
        public SolidColorBrush ColorEESSr { get => colorBrush[2]; set => this.RaiseAndSetIfChanged(ref colorBrush[2], value); }
        public SolidColorBrush ColorCompAnalysisSr { get => colorBrush[3]; set => this.RaiseAndSetIfChanged(ref colorBrush[3], value); }
        public SolidColorBrush ColorArchitecComputerSr { get => colorBrush[4]; set => this.RaiseAndSetIfChanged(ref colorBrush[4], value); }
        public SolidColorBrush ColorMathAnalysisSr { get => colorBrush[5]; set => this.RaiseAndSetIfChanged(ref colorBrush[5], value); }
        public SolidColorBrush ColorNetworcComputerSr { get => colorBrush[6]; set => this.RaiseAndSetIfChanged(ref colorBrush[6], value); }
        public SolidColorBrush ColorAverageSr { get => colorBrush[7]; set => this.RaiseAndSetIfChanged(ref colorBrush[7], value); }
    }
}
