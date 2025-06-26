#  MiniAccount - ASP.NET Core MVC Accounting App

MiniAccount is a lightweight accounting management system built with **ASP.NET Core Razor Pages**, **Entity Framework Core**, and **SQL Server**. It allows users to manage accounts, vouchers (journal, payment, receipt), and generate Excel reports.

---

##  Features

-  **Chart of Accounts** (create, edit, delete)
-  **Voucher Management**
  - Journal, Payment, Receipt entries
-  **Voucher Entry with Debit/Credit system**
- **Export Voucher Details to Excel** using ClosedXML
-  **Stored Procedure Integration** (No LINQ) for better performance

---

##  Screenshots

###  Home Page
![Home Page](screenshots/HomePage.png)

###  Accountant List Page
![Accountant List Page](screenshots/AccountantListPage.png)

###  Accountant Crate Page
![Accountant Crate Page](screenshots/AccountantCreatePage.png)

###  Accountant Edit Page
![Accountant Edit Page](screenshots/AccountantEditPage.png)

###  Accountant Delete Page
![Accountant Delete Page](screenshots/AccountantDeletePage.png)

###  Voucher List Page
![Voucher List Page](screenshots/VoucherListPage.png)

###  Voucher Create Page
![Voucher Create Page](screenshots/VoucherCreatePage.png)

###  Voucher Details Page
![Voucher Details Page](screenshots/VoucherDetailsPage.png)

###  Manage Account Page
![Manage Account Page](screenshots/ManageAccountPage.png)

###  Excel Download
![Excel Output](screenshots/ExcelOutput.png)

>  Replace `/screenshots/*.png` with actual screenshot files in the `screenshots` folder.

---

##  Technologies Used

- ASP.NET Core Razor Pages
- Entity Framework Core
- SQL Server
- Stored Procedures (for all CRUD operations)
- ClosedXML (for Excel generation)
- Bootstrap 5 (UI)

---

##  Project Structure

MiniAccount/
├── Pages/
│ ├── Accountant/ # Account CRUD pages
│ └── Vouchers/ # Voucher pages
├── Models/ # Data models
├── Data/ # DbContext + SP logic
├── wwwroot/ # Static files (CSS, JS)
└── README.md

---

##  Setup Instructions

1. **Clone the Repo**
   ```bash
   git clone https://github.com/yourusername/MiniAccount.git
   cd MiniAccount