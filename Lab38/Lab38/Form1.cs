using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

public class Form1 : Form
{
	private double points = 0;
	private double pointsPerClick = 1;
	private System.Windows.Forms.Timer passiveTimer = new System.Windows.Forms.Timer();

	private List<Upgrade> upgrades = new List<Upgrade>
	{
		new Upgrade("Усиленный клик", 50, 1.5, 0.5, 0),
		new Upgrade("Автокликер", 100, 1.4, 0, 1),
		new Upgrade("Ускоритель", 150, 1.3, 0, 2),
		new Upgrade("Бонус х2", 200, 1.6, 1, 0),
		new Upgrade("Пассивный доход", 300, 1.4, 0, 3),
        // Добавьте ещё 5–6 улучшений по аналогии
    };

	public Form1()
	{
		Text = "Кликер";
		Size = new Size(400, 600);
		StartPosition = FormStartPosition.CenterScreen;

		// Метка очков
		var lblPoints = new Label
		{
			Name = "lblPoints",
			Text = "Очки: 0",
			Location = new Point(10, 10),
			AutoSize = true
		};
		Controls.Add(lblPoints);

		// Кнопка клика
		var btnClick = new Button
		{
			Text = "КЛИК!",
			Location = new Point(10, 40),
			Size = new Size(120, 50)
		};
		btnClick.Click += (s, e) =>
		{
			points += pointsPerClick;
			lblPoints.Text = $"Очки: {(int)points}";
		};
		Controls.Add(btnClick);

		// Панель улучшений
		int y = 120;
		foreach (var upg in upgrades)
		{
			var btn = new Button
			{
				Text = $"{upg.Name} (ур. {upg.Level})\nСтоимость: {upg.GetCost()}",
				Location = new Point(10, y),
				Size = new Size(360, 60),
				Font = new Font("Arial", 9)
			};
			btn.Click += (s, e) => BuyUpgrade(upg, btn, lblPoints);
			Controls.Add(btn);
			y += 70;
		}

		// Настройка таймера пассивного дохода
		passiveTimer.Interval = 1000; // 1 секунда
		passiveTimer.Tick += (s, e) =>
		{
			double passive = 0;
			foreach (var u in upgrades) passive += u.PassivePerSecond * u.Level;
			points += passive;
			lblPoints.Text = $"Очки: {(int)points}";
		};
		passiveTimer.Start();

		LoadGame(); // Загрузить сохранение при старте
		UpdateUI(lblPoints); // Обновить интерфейс после загрузки
	}

	// Покупка улучшения
	private void BuyUpgrade(Upgrade upg, Button btn, Label lbl)
	{
		int cost = upg.GetCost();
		if (points >= cost)
		{
			points -= cost;
			upg.Level++;
			pointsPerClick += upg.ClickEffect;

			btn.Text = $"{upg.Name} (ур. {upg.Level})\nСтоимость: {upg.GetCost()}";
			lbl.Text = $"Очки: {(int)points}";
		}
		else
		{
			MessageBox.Show("Не хватает очков!");
		}
	}

	// Обновить текст всех кнопок улучшений
	private void UpdateUI(Label lblPoints)
	{
		lblPoints.Text = $"Очки: {(int)points}";
		foreach (Control ctrl in Controls)
		{
			if (ctrl is Button btn && btn.Tag is Upgrade upg)
			{
				btn.Text = $"{upg.Name} (ур. {upg.Level})\nСтоимость: {upg.GetCost()}";
			}
		}
	}

	// Сохранение игры в JSON
	private void SaveGame()
	{
		var data = new GameData
		{
			Points = points,
			PointsPerClick = pointsPerClick,
			Upgrades = upgrades
		};

		string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
		{
			WriteIndented = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		});

		File.WriteAllText("save.json", json);
	}

	// Загрузка игры из JSON
	private void LoadGame()
	{
		if (!File.Exists("save.json")) return;

		try
		{
			string json = File.ReadAllText("save.json");
			var data = JsonSerializer.Deserialize<GameData>(json);

			points = data.Points;
			pointsPerClick = data.PointsPerClick;
			upgrades = data.Upgrades ?? new List<Upgrade>();

			// Начислить очки за время оффлайн (0.1 очка за секунду)
			double offlineSeconds = (DateTime.Now - LastSaveTime()).TotalSeconds;
			points += 0.1 * offlineSeconds;
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Ошибка загрузки: {ex.Message}");
		}
	}

	// Время последнего сохранения
	private DateTime LastSaveTime()
	{
		return File.Exists("save.json") ?
			File.GetLastWriteTime("save.json") :
			DateTime.Now;
	}

	// Сохранить при закрытии
	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		SaveGame();
		base.OnFormClosing(e);
	}
}

// Класс улучшения
public class Upgrade
{
	public string Name { get; set; }
	public int BaseCost { get; set; }
	public double Multiplier { get; set; }
	public double ClickEffect { get; set; } // доп. очки за клик
	public double PassivePerSecond { get; set; } // пассивный доход в сек.
	public int Level { get; set; } = 0; // текущий уровень

	public Upgrade(string name, int baseCost, double mult, double clickEff, double passEff)
	{
		Name = name;
		BaseCost = baseCost;
		Multiplier = mult;
		ClickEffect = clickEff;
		PassivePerSecond = passEff;
	}

	public int GetCost() => (int)(BaseCost * Math.Pow(Multiplier, Level));
}

// Данные для сохранения
public class GameData
{
	public double Points { get; set; }
	public double PointsPerClick { get; set; }
	public List<Upgrade> Upgrades { get; set; }
}

