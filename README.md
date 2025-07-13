# PharmacyInventory  
(2024)

**PharmacyInventory** is a desktop-based inventory management system developed in **VB.NET** for pharmacies and medical suppliers.  
It enables real-time tracking of inventory levels, supply movement, and delivery logistics, including driver dispatch and delivery records.

## Features
- Inventory management with stock in/out tracking
- Delivery tracking and driver assignment
- Item quantity monitoring and restock indicators
- Windows Forms interface for ease of use
- MySQL database integration

## Requirements
- Visual Studio 2012 or later  
  [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)
- .NET Framework 4.8.1 or later  
  [Download .NET Framework 4.8.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net481)
- XAMPP or WAMP (for MySQL)  
  [Download XAMPP](https://www.apachefriends.org/index.html)  
  [Download WAMP](https://www.wampserver.com/en/)
- SQLYog or any MySQL client  
  [Download SQLYog](https://github.com/webyog/sqlyog-community/wiki/Downloads)
- MySQL .NET Connector (`MySql.Data.dll`)  
  [Download Connector/NET](https://dev.mysql.com/downloads/connector/net/)

## Installation
1. Download and extract the project `.zip` file.
2. Start MySQL using XAMPP, WAMP, or your preferred method.
3. Open SQLYog and import the `.sql` file located in the `sql` folder to set up the database.
4. Open `PharmacyInventory.sln` in Visual Studio.
5. Ensure the following before running:  
   - The project targets .NET Framework 4.8.1 or later.  
   - `MySql.Data.dll` is added and referenced in the project.
6. Build and run the project.

---

**Developer:** Janelle Ann Castillo ([nncast](https://github.com/nncast))
