using System;
using System.IO;
using System.Windows.Forms;
using System.Timers;
using System.Drawing;

namespace ClickerGame
{
    public partial class Form1 : Form
    {
        private int score = 0;
        private DateTime lastCloseTime;
        private System.Timers.Timer autoIncomeTimer;

        // Список улучшений (10 штук)
        private Upgrade[] upgrades = new Upgrade[]
        {
            new Upgrade("Ускоритель клика", 10, 1, 1.5),
            new Upgrade("Автокликер", 50, 0.1, 2),
            new Upgrade("Супер-Бонуска", 100, 2, 1.8),
            new Upgrade("Мега додеп", 200, 5, 2.1),
            new Upgrade("Бонуска в Собаках", 50000, 1000, 1000)
        };

        public Form1()
        {
            InitializeComponent();
            LoadGame();
            InitializeTimer();
            UpdateUI();
        }

        private void InitializeTimer()
        {
            autoIncomeTimer = new System.Timers.Timer(1000); // 1 секунда
            autoIncomeTimer.Elapsed += AutoIncomeTick;
            autoIncomeTimer.Start();
        }

        private void AutoIncomeTick(object source, ElapsedEventArgs e)
        {
            int income = 0;
            foreach (var upgrade in upgrades)
            {
                if (upgrade.Level > 0)
                {
                    income += (int)(upgrade.AutoIncome * upgrade.Level);
                }
            }

            // Потокобезопасное обновление UI
            this.Invoke((MethodInvoker)delegate
            {
                score += income;
                UpdateUI();
            });
        }

        private void buttonClick_Click(object sender, EventArgs e)
        {
            score++;
            UpdateUI();
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            if (listBoxUpgrades.SelectedIndex == -1) return;

            var selectedUpgrade = upgrades[listBoxUpgrades.SelectedIndex];
            int cost;

            try
            {
                cost = (int)(selectedUpgrade.BaseCost * Math.Pow(selectedUpgrade.CostMultiplier, selectedUpgrade.Level));
            }
            catch
            {
                MessageBox.Show("Стоимость слишком большая!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (score >= cost)
            {
                score -= cost;
                selectedUpgrade.Level++;
                UpdateUI();
                SaveGame();
            }
            else
            {
                MessageBox.Show("Недостаточно очков!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUI()
        {
            labelScore.Text = $"Очки: {score:N0}";

            listBoxUpgrades.Items.Clear();
            foreach (var upgrade in upgrades)
            {
                int cost;
                try
                {
                    cost = (int)(upgrade.BaseCost * Math.Pow(upgrade.CostMultiplier, upgrade.Level));
                }
                catch
                {
                    cost = int.MaxValue; // Если переполнение — показываем как "очень дорого"
                }
                listBoxUpgrades.Items.Add($"{upgrade.Name} (Lvl {upgrade.Level}) — {cost} очков");
            }

        }

        private void SaveGame()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("save.txt"))
                {
                    writer.WriteLine(score);
                    writer.WriteLine(DateTime.Now.ToString("o")); // ISO формат
                    foreach (var upgrade in upgrades)
                    {
                        writer.WriteLine(upgrade.Level);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGame()
        {
            if (!File.Exists("save.txt"))
            {
                return; // Новый игрок
            }

            try
            {
                using (StreamReader reader = new StreamReader("save.txt"))
                {
                    score = int.Parse(reader.ReadLine());
                    string closeTimeStr = reader.ReadLine();
                    lastCloseTime = DateTime.Parse(closeTimeStr);

                    TimeSpan offlineTime = DateTime.Now - lastCloseTime;
                    int offlineSeconds = (int)offlineTime.TotalSeconds;

                    // Начисляем оффлайн-доход
                    for (int i = 0; i < upgrades.Length; i++)
                    {
                        int levels = int.Parse(reader.ReadLine());
                        upgrades[i].Level = levels;

                        if (levels > 0)
                        {
                            score += (int)(upgrades[i].AutoIncome * levels * offlineSeconds);
                        }
                    }

                    labelTime.Text = $"Оффлайн-доход за {offlineTime.Days} д. {offlineTime.Hours} ч. {offlineTime.Minutes} м.";
                    labelTime.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveGame();
        }
    }

    // Класс улучшения
    public class Upgrade
    {
        public string Name { get; set; }
        public int BaseCost { get; set; }
        public double AutoIncome { get; set; } // очков в секунду
        public double CostMultiplier { get; set; } // множитель стоимости
        public int Level { get; set; } = 0;

        public Upgrade(string name, int baseCost, double autoIncome, double costMultiplier)
        {
            Name = name;
            BaseCost = baseCost;
            AutoIncome = autoIncome;
            CostMultiplier = costMultiplier;
        }
    }
}
