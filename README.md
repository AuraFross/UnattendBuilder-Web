# Windows Key Builder

A web application for generating customized Windows `autounattend.xml` files. This tool helps you create automated Windows installation media with ease.

**[🌐 Live Demo: https://wkey.aunixservers.com/](https://wkey.aunixservers.com/)**

## Preview

![Full Application](assets/Full%20Page.PNG)

## Features & Gallery

### 1. Basic Settings
Configure language, time zone, and computer name. Includes comprehensive support for all Windows 11 regions.
![Basic Settings](assets/basic.PNG)

### 2. Disk Partitioning
Automate disk formatting and partitioning (MBR/GPT) to ensure a clean install every time.
![Disk Partitioning](assets/Disk%20Partitioning.PNG)

### 3. Windows Edition & Key
Select your edition (Pro, Home, Enterprise, etc.) and input product keys.
![Windows Edition](assets/windows%20edition.PNG)

### 4. User Accounts
Pre-create local user accounts, set passwords, and enable auto-login.
![User Accounts](assets/User%20Accounts.PNG)

### 5. System Tweaks
Customize the experience:
- **Bypass TPM 2.0 & RAM Checks** (Install on older hardware)
- Disable UAC
- Enable RDP
- Remove Bloatware
![System Tweaks](assets/system%20tweaks.PNG)

### 6. Power Settings
Configure high-performance power plans and disable sleep/hibernate to keep the system running.
![Power Settings](assets/Power%20Settings.PNG)

### 7. Driver Injection
Automatically install drivers from a USB drive during setup.
![Driver Injection](assets/Driver%20Injection.PNG)

### 8. BitLocker Encryption
Enable BitLocker encryption automatically during the first login.
![BitLocker](assets/Bitlocker.PNG)

## Getting Started

### Prerequisites

- Docker Desktop or Docker Engine

### Deployment

1. Run the container using Docker Compose:
   ```bash
   docker-compose up -d
   ```

2. Access the application at `http://localhost:8082` or `http://<server-ip>:8082`.

## Tech Stack

- **Framework**: .NET 6.0 (ASP.NET Core / Blazor Server)
- **UI Library**: MudBlazor
- **Containerization**: Docker
