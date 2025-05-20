

# Task 1: Jagged Array Instagram Posts Console App

## Features

* Uses **jagged arrays** to store user posts (caption + likes).
* Input validation for integers and strings.
* Clear grouping and display of data per user.
* Modular class structure for better code organization.

---

## Project Structure

### 1. `GetUserInput` Class

Responsible for safely handling all user input via the console.
Validates input type (integer or string) and prevents empty or invalid entries.

```csharp
public object SingleUserInput(string message, string dataType)
```

### 2. `StorePosts` Class

Handles:

* Accepting number of users.
* Collecting each user's post details.
* Storing data in a jagged array structure.
* Displaying the post data grouped by user.

```csharp
public void InsertIntoArray()
```

Internally uses `object[][][]` where:

* First index → User
* Second index → Post
* Third index → Caption (string) and Likes (int)

### 3. `Program` Class

The main entry point of the application. Calls the method to insert and display post data.

-------

### Example Console Interaction

```
Please Enter the Users count: 2
user 1 posts
Please Enter the Post count for user:1 = 2
Please Enter the Caption for Post 1: Sunset at beach
Please Enter the Likes for Post 1: 150
Please Enter the Caption for Post 2: Coffee time
Please Enter the Likes for Post 2: 89
user 2 posts
Please Enter the Post count for user:2 = 1
Please Enter the Caption for Post 1: Hiking adventure
Please Enter the Likes for Post 1: 230
```

### Output:

```
Sunset at beach,150
Coffee time,89
Hiking adventure,230
```

---

## Sample Test Case

| User | Number of Posts | Post Captions        | Likes      |
| ---- | --------------- | -------------------- | ---------- |
| 1    | 2               | "Lunch", "Road Trip" | 40, 120    |
| 2    | 1               | "Workout"            | 75         |
| 3    | 3               | "Book", "Tea", "Cat" | 30, 15, 60 |

---

Below is a sample `README.md` file for Task 2, which documents the Employee Console Application. This file explains the objectives, design decisions, classes created, and how each requirement is fulfilled by the application.

---

# Task 2: Employee Console Application

## Features

* **Employee Promotion Module**

  * **Data Collection:** Uses a `List<string>` to record employee names in the order entered.
  * **Position Search:** Uses `IndexOf` to find and display an employee’s position in the promotion list.
  * **Memory Optimization:** Applies the `TrimExcess` method to reduce the list’s capacity to its current count.
  * **Sorted Display:** Sorts and prints the list in ascending order after promotion.

* **Employee Management Module**

  * **Data Storage:** Uses a `Dictionary<int, Employee>` to guarantee unique IDs and allows fast lookup by employee ID.
  * **Default Data:** Includes a method to populate the dictionary with sample employee records.
  * **Operations:**

    * Display all employee details.
    * Retrieve an employee by ID.
    * Retrieve employees by name.
    * Update fields (name, age, and salary) for an employee.
    * Delete an employee entry.
    * Find an employee older than a given employee.
    * Sort the list of employees by salary.
  * **Error Handling:** Validates user inputs and provides informative messages when an employee is not found or invalid input is provided.

* **Additional Product Module**

  * **Product Details:** Uses a simple dictionary to store product names with prices and displays them on request.

---

## Project Structure

### 1. `GetUserInput` Class

* **Purpose:**
  Manages user input ensuring non-empty input and correct type conversion. Provides a generic method for retrieving input as either an integer or a string.

* **Key Method:**

  ```csharp
  public object SingleUserInput(string message, string dataType)
  ```

### 2. `Employee` Class

* **Purpose:**
  Models an employee with properties like ID, Age, Name, and Salary. Implements the `IComparable` interface for sorting based on salary.
* **Note:**
  (Although not shown in the snippets above, it is assumed that this class is implemented properly.)

### 3. `ManageEmployeeDetails` Class

* **Purpose:**
  Provides a complete set of methods to manage employee records using a dictionary.
* **Key Methods:**

  * `AddDefaultEmployees()` — Adds sample employee records.
  * `ShowAllEmployees()` — Displays all employee details.
  * `GetEmployeeByID()` — Retrieves an employee by their ID.
  * `SortBySalary()` — Sorts and displays employees by salary.
  * `GetEmployeesByName()` — Searches and displays employees by name.
  * `FindElder()` — Finds an employee older than a given employee.
  * `AddNweEmployee()` — Adds a new employee with user input.
  * `UpdateEmployee()` — Updates details (except ID) for an existing employee.
  * `DeleteEmployee()` — Deletes an employee from the dictionary.
  * `ManageDetails()` — Provides a menu-driven loop to perform all operations.

### 4. `EmployeePromotion` Class

* **Purpose:**
  Processes employee promotion eligibility based on the order of input.
* **Key Methods:**

  * `AddEmployee()` — Accepts employee names in the promotion order.
  * `getEmployeePosition()` — Finds and displays the position for a given employee.
  * `ReduceMemory()` — Calls `TrimExcess()` to remove unused capacity.
  * `ShowPromotedList()` — Sorts and prints the promotion list.

## Example Interaction

### Employee Promotion Example

```
Please enter the employee names in the order of their eligibility for promotion (enter blank to stop):
Ramu
Bimu
Somu
Gomu
Vimu
[blank entered]

Please enter the name of the employee to check promotion position: 
Somu

“Somu” is in position 3 for promotion.
Current Size: 8, Current Count: 5
After trimming: Size: 5, Count: 5

Promoted Employee List (sorted):
Bimu
Gomu
Ramu
Somu
Vimu
```

### Employee Management Example

```
================== MENU ==================
1. Show All Employees
2. Add New Employee
3. Update Employee
4. Get Employee by ID
5. Delete Employee
6. Get Employees by Name
7. Sort Employees by Salary
8. Find Employee Elder Than Given
0. Exit
==========================================
Enter your choice: 4

Enter the employee Id: 103
Employee Details:
ID: 103, Name: Arun, Age: 30, Salary: 58000
```

---
# Task 3: **Additional Task – Product Details:**

   * Demonstrates using a dictionary (`Dictionary<string, double>`) to store product names and their prices.
   * Simply adds sample products and displays them to the console
   
### Product Class

* **Purpose:**
  Demonstrates simple use of dictionaries by storing and displaying product details.
* **Key Method:**
  ```csharp
  public void AddProduct()
  ```