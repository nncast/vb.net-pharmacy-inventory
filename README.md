<h1 align="center">PharmacyInventory</h1>

<p align="center">
  <img src="https://img.shields.io/badge/version-0.1.0-E5484D?style=flat-square" alt="version">
  <img src="https://img.shields.io/badge/status-complete-2772BD?style=flat-square" alt="status">
  <img src="https://img.shields.io/badge/VB.NET-Windows_Forms-512BD4?style=flat-square&logo=dotnet&logoColor=white" alt="VB.NET">
  <img src="https://img.shields.io/badge/.NET_Framework-4.8.1-5C2D91?style=flat-square&logo=dotnet&logoColor=white" alt=".NET Framework">
  <img src="https://img.shields.io/badge/MySQL-XAMPP-4479A1?style=flat-square&logo=mysql&logoColor=white" alt="MySQL">
</p>

<p align="center">
  <b>Download v0.1.0:</b>
  <a href="https://github.com/nncast/vb.net-pharmacy-inventory/archive/refs/tags/v0.1.0.zip">Source (.zip)</a> |
  <a href="https://github.com/nncast/vb.net-pharmacy-inventory/releases">All releases</a>
</p>

**PharmacyInventory** is a desktop-based inventory management system developed in **VB.NET** for pharmacies and medical suppliers. It enables real-time tracking of inventory levels, supply movement, and delivery logistics, including driver dispatch and delivery records.

> **Current version: v0.1.0** — first tagged release. See [Releases](https://github.com/nncast/vb.net-pharmacy-inventory/releases) for the project timeline.

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
| MySQL .NET Connector (`MySql.Data.dll`) | [Connector/NET](https://dev.mysql.com/downloads/connector/net/) |

## Setup and run instructions

1. Clone the repository, or download the [source .zip](https://github.com/nncast/vb.net-pharmacy-inventory/archive/refs/tags/v0.1.0.zip).
   ```bash
   git clone https://github.com/nncast/vb.net-pharmacy-inventory.git
   ```
2. Start MySQL using XAMPP, WAMP, or another server stack.
3. Import `sql/dbpharmacy.sql` with SQLYog or another MySQL client.
4. Open `PharmacyInventory/PharmacyInventory.sln` in Visual Studio.
5. Make sure the project targets .NET Framework 4.8.1 or later and that `MySql.Data.dll` is referenced.
6. Build and run the project.

## Developer

Janelle Ann Castillo ([nncast](https://github.com/nncast))

---

*PharmacyInventory · 2025 · VB.NET · Windows Forms · .NET Framework 4.8.1 · MySQL*
