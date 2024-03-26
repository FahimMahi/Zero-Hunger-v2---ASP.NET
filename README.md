# Zero Hunger v2 --- Advanced .NET

## Description

**The Zero Hunger Project is an innovative initiative leveraging the .NET framework to bridge the gap between food surplus and scarcity. Aimed at reducing food waste and combatting hunger in urban areas, this digital platform enables restaurants in Dhaka to notify about excess food, which is then distributed to those in need. The project's .NET-based system offers a seamless interface for food establishments to register and manage food donations with real-time tracking, ensuring that every meal reaches its destination efficiently. By uniting technology with social responsibility, the Zero Hunger Project has created a sustainable model for food distribution that not only feeds the hungry but also fosters community awareness and involvement in the fight against food waste. In a world where food production is ample, it's a profound paradox that hunger still grips communities and food waste remains rampant. The Zero Hunger Project is born from the vision that aligns with the United Nations' Sustainable Development Goals—specifically, the goal to achieve Zero Hunger.**

## User

* **Admin**
* **Employee**
* **Restaurant**

## Database Design

### **1. Users**

* **UserID (PK)**
* **Username**
* **Password**
* **Email**
* **Role (Enum: Admin, Restaurant, Employee)**

### **2. Restaurants**

* **RestaurantID (PK)**
* **UserID (FK to Users.UserID)**
* **Name**
* **Address**
* **ContactNumber**
* **TINID**

### **3. Employees**

* **EmployeeID (PK)**
* **Name**
* **Nationality**
* **PhoneNumber**
* **Address**
* **nid**
* **Religion**
* **BloodGroup**
* **DOB**
* **Gender**
* **UserID (FK to Users.UserID)**

### **4. FoodCollection**

* **RID (PK)**
* **RequestTime**
* **MaxPreserve**
* **Status (Enum: Pending, Collected, Distributed)**
* **AssignedEmpID (FK to Employees.EmployeeID)**
* **CollectionTime (Nullable)**
* **CompletionTime (Nullable)**
* **RestaurantID (FK to Restaurants.RestaurantID)**

