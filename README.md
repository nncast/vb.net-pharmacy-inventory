<p align="center">
  <img src="assets/logo.png" alt="Pharmease Medical Store" width="320"/>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/version-0.1.1-E5484D?style=flat-square" alt="version">
  <img src="https://img.shields.io/badge/status-complete-2772BD?style=flat-square" alt="status">
  <img src="https://img.shields.io/badge/VB.NET-Windows_Forms-512BD4?style=flat-square&logo=dotnet&logoColor=white" alt="VB.NET">
  <img src="https://img.shields.io/badge/.NET_Framework-4.8.1-5C2D91?style=flat-square&logo=dotnet&logoColor=white" alt=".NET Framework">
  <img src="https://img.shields.io/badge/MySQL-XAMPP-4479A1?style=flat-square&logo=mysql&logoColor=white" alt="MySQL">
</p>

<p align="center">
  <b>Download v0.1.1:</b>
  <a href="https://github.com/nncast/vb.net-pharmacy-inventory/releases/download/v0.1.1/PharmacyInventory-v0.1.1-Windows.zip">Windows (.zip)</a> ·
  <a href="https://github.com/nncast/vb.net-pharmacy-inventory/archive/refs/tags/v0.1.1.zip">Source (.zip)</a> |
  <a href="https://github.com/nncast/vb.net-pharmacy-inventory/releases">All releases</a>
</p>

# Pharmease Medical Store

**Pharmease Medical Store** is a desktop-based inventory management system developed in **VB.NET** for pharmacies and medical suppliers. It enables real-time tracking of inventory levels, supply movement, and delivery logistics, including driver dispatch and delivery records.

> **Current version: v0.1.1** — bug-fix and security release: stock now moves correctly for orders, deliveries and stock-ins, every query is parameterized, the connection settings live in a config file, and there is a ready-to-run Windows build. See [Releases](https://github.com/nncast/vb.net-pharmacy-inventory/releases) for the release notes.

<p align="center">
  <img src="assets/screenshots/home.png" width="400" alt="Home dashboard"/>
  <img src="assets/screenshots/product.png" width="400" alt="Product management"/>
  <img src="assets/screenshots/order.png" width="400" alt="Order form with cart"/>
  <img src="assets/screenshots/delivery.png" width="400" alt="Delivery tracking"/>
</p>

## Features

- Inventory management with stock in/out tracking
- Delivery tracking and driver assignment
- Windows Forms interface for ease of use
- MySQL database integration

## Development environment

| Category | Details |
| --- | --- |
| Language | Visual Basic .NET |
| UI | Windows Forms |
| Framework | .NET Framework 4.8.1 |
| Database | MySQL / MariaDB (XAMPP or WAMP) — database `dbpharmacy` |
| Driver | MySql.Data (MySQL Connector/NET) |
| IDE | Visual Studio 2012 or later |

## Requirements

| Tool | Download |
| --- | --- |
| Visual Studio 2012 or later | [visualstudio.microsoft.com](https://visualstudio.microsoft.com/downloads/) |
| .NET Framework 4.8.1 or later | [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net481) |
| XAMPP or WAMP (for MySQL) | [XAMPP](https://www.apachefriends.org/index.html) · [WAMP](https://www.wampserver.com/en/) |
| SQLYog or any MySQL client | [SQLYog](https://github.com/webyog/sqlyog-community/wiki/Downloads) |
| MySQL .NET Connector (`MySql.Data.dll`) | Included in `lib/` (from [Connector/NET](https://dev.mysql.com/downloads/connector/net/)) |

## Setup and run instructions

**Windows build (no Visual Studio needed)**

1. Download [`PharmacyInventory-v0.1.1-Windows.zip`](https://github.com/nncast/vb.net-pharmacy-inventory/releases/download/v0.1.1/PharmacyInventory-v0.1.1-Windows.zip) from the [v0.1.1 release](https://github.com/nncast/vb.net-pharmacy-inventory/releases/tag/v0.1.1) and extract it.
2. Start MySQL (XAMPP, WAMP, or another server) and import `database/dbpharmacy.sql` from the extracted folder.
3. If your MySQL server, port, user or password differ from `localhost:3306` / `root` / no password, open `PharmacyInventory.exe.config` in Notepad and edit the `PharmacyDb` connection string.
4. Run `PharmacyInventory.exe`.

**From source**

1. Clone the repository, or download the [source .zip](https://github.com/nncast/vb.net-pharmacy-inventory/archive/refs/tags/v0.1.1.zip).
   ```bash
   git clone https://github.com/nncast/vb.net-pharmacy-inventory.git
   ```
2. Start MySQL using XAMPP, WAMP, or another server stack.
3. Import `database/dbpharmacy.sql` with SQLYog or another MySQL client.
4. Open `PharmacyInventory/PharmacyInventory.sln` in Visual Studio.
5. If your MySQL settings differ from the defaults, edit the `PharmacyDb` connection string in `PharmacyInventory/PharmacyInventory/App.config`. `MySql.Data.dll` ships in the repository's `lib` folder, so nothing else needs to be installed for the reference.
6. Build and run the project.

---

*Pharmease Medical Store · Pharmacy Inventory System · 2025 · VB.NET · Windows Forms · .NET Framework 4.8.1 · MySQL*
