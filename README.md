# AZ Kvíz – Desktop Quiz Game

Desktop educational quiz game inspired by the Czech TV competition **AZ Kvíz**.
The application allows two players or a player against the computer to compete by answering questions and strategically capturing fields on a hexagonal game board.

The project was created as a school project for the course **Programming and Application Development**.

---

## ✨ Features

* 🎮 **Two game modes**

  * Player vs Player
  * Player vs Computer (simple AI)

* 🧠 **Quiz system**

  * Questions loaded from **JSON files**
  * Easy to extend with new categories and topics
  * Multiple-choice answers

* 🧩 **Game mechanics**

  * Turn-based gameplay
  * Hexagonal board similar to AZ Kvíz
  * Capture fields by answering correctly
  * Opponent gets a chance if the player answers incorrectly
  * Automatic win detection (connecting three sides of the board)

* ⏱ **Additional features**

  * Answer time limit (timer)
  * Light / Dark theme
  * Keyboard shortcuts
  * Game history and score tracking
  * Error handling and input validation

---

## 🛠 Technologies

* **C#**
* **WPF**
* **XAML**
* **JSON**
* **MVVM architecture**
* **Object-Oriented Programming (OOP)**

---

## 📂 Project Structure

```
AZKvizWPF/
├─ src/
│  └─ AZKvizWPF/
│     ├─ Views/
│     ├─ ViewModels/
│     ├─ Models/
│     ├─ Services/
│     ├─ Resources/
│     └─ Data/
```

Main architecture layers:

* **Views** – UI components created with WPF/XAML
* **ViewModels** – presentation logic (MVVM pattern)
* **Models** – game data structures and entities
* **Services** – game logic, AI, storage, validation, UI utilities
* **Data** – questions, scores, and game history

---

## 🚀 Running the Application

### Requirements

* **Windows OS**
* **.NET SDK / Runtime (recommended .NET 6 or newer)**
* **Visual Studio 2022 or newer**

### Steps

1. Clone the repository

```
git clone https://github.com/yourusername/AZKvizWPF.git
```

2. Open the solution

```
AZKvizWPF.sln
```

3. Build and run the project in **Visual Studio**.

---

## 📄 Question Format (JSON)

Questions are stored in JSON files to allow easy editing and reuse.

Example:

```json
{
  "category": "Geography",
  "question": "What is the capital of France?",
  "options": [
    "Berlin",
    "Madrid",
    "Paris",
    "Rome"
  ],
  "correctAnswer": 2
}
```

---

## 🎯 Project Goals

* Create a **reusable quiz system**
* Implement **turn-based game logic**
* Use **MVVM architecture**
* Practice **Clean Code principles**
* Separate **data (questions) from application logic**

---

## 👨‍💻 Author

**David Mihók**
Student – Class 4.C
Course: *Programming and Application Development*

---

## 📜 License

This project was created for educational purposes.
